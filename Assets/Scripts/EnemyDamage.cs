using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int _damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerHealth health))
        {
            health.TakeDamage(_damage, transform.position);
        }
    }
}