using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer), typeof(Health), typeof(KnockbackHandler))]

public class DamageHandler : MonoBehaviour
{
    
    [SerializeField] private float _invulnerabilityDuration = 1.5f;
    [SerializeField] private float _blinkInterval = 0.1f;

    private SpriteRenderer _spriteRenderer;
    private Health _health;
    private KnockbackHandler _knockbackHandler;
    private bool _isInvulnerable;

    private void Awake()
    {
        TryGetComponent<SpriteRenderer>(out _spriteRenderer);
        TryGetComponent<Health>(out _health);
        TryGetComponent<KnockbackHandler>(out _knockbackHandler);
    }

    public void TakeDamage(int damage, Vector2 damageSourcePosition)
    {
        if (_isInvulnerable)
            return;

        _health.ApplyDamage(damage);
        _knockbackHandler.ApplyKnockback(damageSourcePosition);
        StartCoroutine(InvulnerabilityRoutine());
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
