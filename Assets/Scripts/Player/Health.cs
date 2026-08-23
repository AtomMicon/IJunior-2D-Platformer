using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxValue = 3;
    [SerializeField] private AudioClip _damageSound;


    private int _currentValue;

    public event Action Died;

    private void Start()
    {
        _currentValue = _maxValue;
    }

    public void TakeDamage(int amount)
    {
        _currentValue -= amount;

        if (_damageSound != null)
        {
            AudioSource.PlayClipAtPoint(_damageSound, transform.position);
        }

        
        if (_currentValue <= 0)
        {
            Died?.Invoke();
            return;
        }
    }
}