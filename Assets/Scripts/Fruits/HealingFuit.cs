using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealingFruit : MonoBehaviour
{
    [SerializeField] private int _healValue = 1;

    public int HealValue => _healValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Health healthComponent))
        {
            healthComponent.Heal(_healValue);
            Destroy(gameObject);
        }
    }
}