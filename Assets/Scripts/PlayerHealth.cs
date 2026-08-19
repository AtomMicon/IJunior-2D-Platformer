using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PlayerHealth : MonoBehaviour
{
    public event Action OnDied;

    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private float _invulnerabilityDuration = 1.5f;
    [SerializeField] private float _blinkInterval = 0.1f;
    [SerializeField] private float _knockbackForce = 5f;
    [SerializeField] private AudioClip _damageSound;

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;

    private int _currentHealth;
    private bool _isInvulnerable;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int amount, Vector2 damageSourcePosition)
    {
        if (_isInvulnerable || _currentHealth <= 0) return;

        _currentHealth -= amount;

        if (_damageSound != null)
        {
            AudioSource.PlayClipAtPoint(_damageSound, transform.position);
        }

        ApplyKnockback(damageSourcePosition);
        StartCoroutine(InvulnerabilityRoutine());

        if (_currentHealth <= 0)
        {
            OnDied?.Invoke();
            return;
        }
    }

    private void ApplyKnockback(Vector2 damageSourcePosition)
    {
        _rb.linearVelocity = Vector2.zero;

        Vector2 direction = ((Vector2)transform.position - damageSourcePosition).normalized;
        direction = new Vector2(direction.x, 0.5f).normalized;

        _rb.AddForce(direction * _knockbackForce, ForceMode2D.Impulse);
    }

    private IEnumerator InvulnerabilityRoutine()
    {
        _isInvulnerable = true;
        float elapsedTime = 0f;

        while (elapsedTime < _invulnerabilityDuration)
        {
            _spriteRenderer.enabled = !_spriteRenderer.enabled;
            yield return new WaitForSeconds(_blinkInterval);
            elapsedTime += _blinkInterval;
        }

        _spriteRenderer.enabled = true;
        _isInvulnerable = false;
    }
}