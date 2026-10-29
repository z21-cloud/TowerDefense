using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class HealthTests
{
    private GameObject _gameObject;
    private Health _health;
    private readonly List<int> _currentHealthValues = new List<int>();
    private readonly List<int> _maxHealthValues = new List<int>();

    [SetUp]
    public void SetUp()
    {
        _currentHealthValues.Clear();
        _maxHealthValues.Clear();

        _gameObject = new GameObject(nameof(HealthTests));
        _gameObject.SetActive(false);
        _health = _gameObject.AddComponent<Health>();
        _health.OnHealthChanged += RecordHealthChanged;
        _health.Awake();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(_gameObject);
    }

    [Test]
    public void Awake_InitializesHealthToMaximumAndRaisesEvent()
    {
        AssertHealthChangedEvents((100, 100));
    }

    [Test]
    public void TakeDamage_ReducesHealthAndClampsAtZero()
    {
        _health.TakeDamage(30);
        _health.TakeDamage(100);

        AssertHealthChangedEvents((100, 100), (70, 100), (0, 100));

        _health.TakeDamage(10);

        AssertHealthChangedEvents((100, 100), (70, 100), (0, 100));
    }

    [Test]
    public void Heal_RestoresHealthAndClampsAtMaximum()
    {
        _health.TakeDamage(40);
        _health.Heal(15);
        _health.Heal(100);
        _health.Heal(1);

        AssertHealthChangedEvents((100, 100), (60, 100), (75, 100), (100, 100));
    }

    private void RecordHealthChanged(int current, int max)
    {
        _currentHealthValues.Add(current);
        _maxHealthValues.Add(max);
    }

    private void AssertHealthChangedEvents(params (int current, int max)[] expected)
    {
        Assert.That(_currentHealthValues, Is.EqualTo(ExtractCurrentValues(expected)));
        Assert.That(_maxHealthValues, Is.EqualTo(ExtractMaxValues(expected)));
    }

    private static int[] ExtractCurrentValues((int current, int max)[] values)
    {
        int[] currentValues = new int[values.Length];

        for (int i = 0; i < values.Length; i++)
        {
            currentValues[i] = values[i].current;
        }

        return currentValues;
    }

    private static int[] ExtractMaxValues((int current, int max)[] values)
    {
        int[] maxValues = new int[values.Length];

        for (int i = 0; i < values.Length; i++)
        {
            maxValues[i] = values[i].max;
        }

        return maxValues;
    }
}
