using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Health))]

public class DamageHandler : MonoBehaviour
{
    [SerializeField] private float _knockbackForce = 5f;
    [SerializeField] private float _invulnerabilityDuration = 1.5f;
    [SerializeField] private float _blinkInterval = 0.1f;

    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;
    private Health _health;
    private bool _isInvulnerable;

    private void Awake()
    {
        TryGetComponent<Rigidbody2D>(out _rigidBody);
        TryGetComponent<SpriteRenderer>(out _spriteRenderer);
        TryGetComponent<Health>(out _health);
    }

    public void TakeDamage(int damage, Vector2 damageSourcePosition)
    {
        if (_isInvulnerable)
            return;

        _health.TakeDamage(damage);
        ApplyKnockback(damageSourcePosition);
        StartCoroutine(InvulnerabilityRoutine());
    }

    private void ApplyKnockback(Vector2 damageSourcePosition)
    {
        _rigidBody.linearVelocity = Vector2.zero;

        Vector2 direction = ((Vector2)transform.position - damageSourcePosition).normalized;
        direction = new Vector2(direction.x, 0.5f).normalized;

        _rigidBody.AddForce(direction * _knockbackForce, ForceMode2D.Impulse);
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
