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
        if(_currentTIme < 0) return;

        _currentTIme -= Time.deltaTime;

        Debug.Log(_currentTIme);
        // _timerText.text = _currentTIme.ToString();
    }

    public void Interact()
    {
                
    }
}
