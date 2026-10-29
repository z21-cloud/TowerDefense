using TMPro;
using TowerDefence.Core;
using UnityEngine;

public class Campfire : MonoBehaviour, IInteractable
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private float _maxTime = 120f;
    [SerializeField] private float _currentTIme = 120f;

    private void Update()
    {
        if (_currentTIme < 0) return;

        _currentTIme -= Time.deltaTime;

        // _timerText.text = _currentTIme.ToString();
    }

    public bool CanAccept(PickableType type)
    {
        // or is full maybe
        if (type != PickableType.Log) return false;

        return true;
    }

    public void Interact()
    {
        _currentTIme += 20f;
        _currentTIme = Mathf.Clamp(_currentTIme, 0f, _maxTime);

        Debug.Log($"[Campfire] Added log");
    }
}
