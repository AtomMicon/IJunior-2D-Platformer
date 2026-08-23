using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int _damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out DamageHandler damageHandler))
        {
            damageHandler.TakeDamage(_damage, collision.transform.position);
        }
    }
}