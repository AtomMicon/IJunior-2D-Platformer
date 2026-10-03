using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxValue = 5;
    [SerializeField] private AudioClip _damageSound;

    private int _currentValue;
    public event Action Died;
    public event Action<int> HealthChanged;

    private void Start()
    {
        _currentValue = _maxValue;
    }

    public void ApplyDamage(int damage)
    {
        if (_currentValue <= 0) return;
        
        if (_damageSound != null)
        {
            AudioSource.PlayClipAtPoint(_damageSound, transform.position);
        }

        _currentValue -= damage;
        HealthChanged?.Invoke(_currentValue);

        if (_currentValue <= 0)
        {
            Died?.Invoke();
        }
    }

    public void Heal(int amount)
    {
        if (_currentValue <= 0) return;

        _currentValue = Mathf.Min(_currentValue + amount, _maxValue);
        HealthChanged?.Invoke(_currentValue);
    }
}