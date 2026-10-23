using System;
using System.Collections;
using TowerDefence.PlayerInput;
using UnityEngine;

namespace TowerDefence.Core
{
    public class Axe : MonoBehaviour
    {
        [SerializeField] private GameObject _axe;
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private float _axeUsageTime = 0.2f;
        [SerializeField] private float _axeHitDelay = 0.5f;

        private Rigidbody2D _rb;
        private float _timer;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _timer = 0f;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if(_inputManager.GetAttack() && _timer >= _axeHitDelay)
            {
                _timer = 0f;
                StartCoroutine(UseAxe());
            }
        }

        private IEnumerator UseAxe()
        {
            // Note: Change _rb.WakeUp to Physics2D.OverLapBox, 
            // because on trigger enter 2d might not be called
            _rb.WakeUp();
            _axe.SetActive(true);
            yield return new WaitForSeconds(_axeUsageTime);
            _axe.SetActive(false);
        }
    }
}
