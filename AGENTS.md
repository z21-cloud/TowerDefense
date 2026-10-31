# AGENTS.md — заметки для AI-агентов

Файл описывает устройство проекта «Tower Defense: Костёр в лесу» для агентов, которые будут в нём работать. Основной источник правды по геймдизайну — `README.md` (на русском), по поведению — код в `Assets/Scripts`.

## Обзор проекта

2D-игра на Unity (вид сверху) в жанре tower defense. «Башня» — костёр: днём игрок рубит деревья и носит дрова, ночью отбивается от монстров и подкидывает дрова в огонь. Костёр погас — поражение. Статус — **активная разработка MVP**, список задач MVP и планы после него — в `README.md`.

Ключевые технологии: Unity **6000.3.11f1** (версия фиксирована в `ProjectSettings/ProjectVersion.txt`), C#, URP 17.3 (2D-рендерер, настройки в `Assets/Settings`), ScriptableObjects, событийная архитектура через SO event channels.

## Структура репозитория

```
Assets/
  Scripts/           весь игровой код (см. ниже)
  Tests/Editor/      edit-mode тесты (NUnit): HealthTests.cs, InventoryTests.cs
  Prefabs/           Bullet, Log, Slot, Tree
  Scenes/            SampleScene.unity — единственная сцена (в EditorBuildSettings)
  Settings/          ассеты URP (Renderer2D, UniversalRP)
  Sprites/           графика
  TextMesh Pro/      стандартный пакет TMP
  _Recovery/         резервные копии сцен — НЕ часть сборки, не трогать без необходимости
Packages/manifest.json    зависимости пакетов Unity
ProjectSettings/          настройки проекта (версия Unity, InputManager, теги и пр.)
```

Сгенерированные Unity файлы (`.csproj`, `.slnx`, `Library/`, `Temp/`, `Logs/`, `UserSettings/`) в git не коммитятся — см. `.gitignore`. **Файлы `.meta` коммитятся обязательно**: без них ссылки на ассеты в сценах ломаются.

## Кодовая база (`Assets/Scripts`)

Код разбит на папки по доменам: `Player/` (контроллер, движение, оружие `Axe/`+`Weapon/`, инвентарь `Inventory/`, взаимодействие `Pickable/`), `Tree/`, `Log/`, `Damageable/`, `Factory/`, `ObjectPooling/`, `Input/`, `GameBootstrapper/`, плюс отдельные файлы в корне (`Bullet.cs`, `Campfire.cs`, `CameraFollow.cs`).

Пространства имён (namespace): большинство кода — `TowerDefence.Core`, ввод — `TowerDefence.PlayerInput`, камера и UI здоровья — `TowerDefence.PlayerView`, точка сборки — `TowerDefence.Bootstrap`. Новые скрипты должны попадать в один из этих namespace.

### Архитектурные решения (важно сохранять, можно предлагать улучшения, если пользователь сам спрашивает)

- **Ручная сборка зависимостей через `GameBootstrapper`** (`Scripts/GameBootstrapper/GameBootstrapper.cs`): ссылки на компоненты задаются `[SerializeField]` в инспекторе, в `Awake()` вызываются методы `Initialize(...)` — это аналог DI-контейнера. Новые подсистемы подключаются сюда, а не через `FindObjectOfType` и не Singleton-ами.
- **Данные в ScriptableObject**: параметры вынесены в конфиги-ассеты (`TreeConfig`, `AxeInteractorConfig`, `PickableInteractorConfig`), создаются через `CreateAssetMenu` с menuName вида `TowerDefense/...`. Магические числа в коде не приветствуются — добавлять поля в конфиги.
- **События через SO event channels** (`TreeChoppedEventChannelSO`): издатели и подписчики не знают друг о друге напрямую. Паттерн для новых событий: SO с `event Action<T>` + метод `RaiseEvent`.
- **Интерфейсы для расширяемости**: `IDamageable`, `IHealable`, `IHittable`, `IPickable`, `IDamageDealer`, `IInteractable`, `IFactory<T>`, `IPool<T>`. Например, костёр реализует `IDamageable`, поэтому монстры могут бить и игрока, и костёр одной логикой.
- **Фабрики + пулинг**: `Factory<T>` создаёт объекты из префабов, `ObjectPooling<T>` переиспользует их (Bullet, Log, Tree). Пул не расширяется динамически — при пустом пуле логируется ошибка и возвращается null; при возврате объекта в пул физические параметры (Rigidbody velocity) сбрасываются до `SetActive(false)`.
- **Ввод изолирован**: `InputHandler` (читает Unity Input) → `IInputReader` → `InputManager` (игровые запросы). Игровой код не обращается к `Input.*` напрямую — добавлять действия в `IInputReader`/`InputHandler`/`InputManager` связкой.
- **Обработка нулевых ссылок**: компоненты проверяются в `Awake`/`Initialize`, при null — `Debug.LogError` с префиксом `[ИмяКласса]` и (для контроллеров) `enabled = false`.

### Замечание про ввод

Пакет `com.unity.inputsystem` установлен и в `ProjectSettings` задан `activeInputHandler: 2` (Both), но игровой код использует **legacy Input Manager** (`Input.GetAxisRaw`, `Input.GetButtonDown`) с кастомными осями `Reload`, `Inventory`, `Interact`, `Primary Weapon`, `Secondary Weapon`, определёнными в `ProjectSettings/InputManager.asset`. Ассет `Assets/InputSystem_Actions.inputactions` шаблонный и кодом не используется. При добавлении новых клавиш не копируйте new Input System API без согласования.

## Сборка и запуск

- Открыть папку проекта в Unity Hub (нужна Unity **6000.3.x**, точная версия — в `ProjectSettings/ProjectVersion.txt`) и запустить сцену `Assets/Scenes/SampleScene.unity`. Командной строки/CI-сборки нет.
- Решение `TowerDefence.slnx` и `.csproj` генерирует Unity при открытии проекта; в репозиторий они не входят.
- Проверка компиляции: открыть проект в Unity (ошибки покажет консоль) или собрать через `Unity.exe -batchmode -quit -projectPath .` при наличии Unity в PATH.

## Тестирование

- Фреймворк: Unity Test Framework 1.6 (NUnit), **edit-mode** тесты в `Assets/Tests/Editor/` (`HealthTests.cs`, `InventoryTests.cs`). Отдельного `.asmdef` для тестов нет — папка `Editor` компилируется в стандартную editor-сборку, Test Runner их подхватывает.
- Запуск: в редакторе Unity **Window → Test Runner → EditMode → Run All**. Play-mode тестов пока нет.
- Стиль тестов: чистый NUnit (`[Test]`, `Assert.That(..., Is.EqualTo(...))`), имена вида `MethodName_ОжидаемоеПоведение`, тестируются чистые логические классы (например, `Inventory` — без MonoBehaviour, с поддельной реализацией `IPickable` прямо в файле теста). Пишите тесты так же: логику выносить из MonoBehaviour в тестируемые классы.
- Важно: код — источник правды. В тестах инвентарь на 16 ячеек (2 под оружие), а README описывает 10 — это расхождение документации и реализации, при противоречиях ориентируйтесь на код и помечайте расхождение в финальном ответе.

## Стиль кода

- C#, 4 пробела на отступ, фигурные скобки с новой строки, `var` не используется для примитивов.
- Приватные поля — `_camelCase` с `[SerializeField]`; публичные свойства — `PascalCase` (`public int MaxHits => _maxHits;`).
- Имена классов/файлов — PascalCase, файл на класс, папка на подсистему.
- Логи — `Debug.Log`/`LogError` с префиксом `[ИмяКласса]`.
- Комментарии и обсуждение в README — **на русском**; имена идентификаторов — английские.
- Коммиты в репозитории маркируются тегами: `[New Feature]`, `[Refactoring]`; ветки для агентов — `agents/<задача>` с PR в `main`.

## Безопасность и эксплуатация

- Секретов в репозитории нет; конфиги — обычные ассеты Unity. Сторонних сервисов, сети и деплоя нет — сборка это запуск в редакторе/standalone из Unity.
- `Assets/_Recovery/` — локальные резервные копии сцен, не удалять и не включать в сцену сборки.
