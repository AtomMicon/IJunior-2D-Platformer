using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FruitCoin : MonoBehaviour
{
    [SerializeField] private int _scoreValue = 1;
    [SerializeField] private AudioClip _collectSound;
    [SerializeField] private float _bobSpeed = 2f;
    [SerializeField] private float _bobHeight = 0.2f;

    private float _startY;

    private void Start()
    {
        _startY = transform.position.y;
    }

    private void Update()
    {
        float newY = _startY + Mathf.Sin(Time.time * _bobSpeed) * _bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(_scoreValue);

            if (_collectSound != null)
            {
                AudioSource.PlayClipAtPoint(_collectSound, transform.position);
            }

            Destroy(gameObject);
        }
    }
}