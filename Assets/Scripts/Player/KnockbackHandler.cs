using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class KnockbackHandler : MonoBehaviour
{
    [SerializeField] private float _knockbackForce = 5f;

    private Rigidbody2D _rigidBody;
    
    void Start()
    {
        TryGetComponent<Rigidbody2D>(out _rigidBody);
    }

    public void ApplyKnockback(Vector2 damageSourcePosition)
    {
        _rigidBody.linearVelocity = Vector2.zero;

        Vector2 direction = ((Vector2)transform.position - damageSourcePosition).normalized;
        direction = new Vector2(direction.x, 0.5f).normalized;

        _rigidBody.AddForce(direction * _knockbackForce, ForceMode2D.Impulse);
    }
}
