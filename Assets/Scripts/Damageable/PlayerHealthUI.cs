using TMPro;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textUI;

    private Health _health;

    public void Initialize(Health health)
    {
        _health = health;

        _health.OnHealthChanged += HealthChanged;
    }

    public void HealthChanged(int current, int max)
    {
        _textUI.text = $"Health: {current} / {max}";
    }

    private void OnDestroy()
    {
        _health.OnHealthChanged -= HealthChanged;
    }
}
