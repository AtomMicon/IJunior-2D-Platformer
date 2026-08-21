using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    [SerializeField] private AudioClip _collectSound;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out FruitCoin fruitCoin))
        {
            ScoreUpdater.Instance.AddScore(fruitCoin.ScoreValue);

            if (_collectSound != null)
            {
                AudioSource.PlayClipAtPoint(_collectSound, transform.position);
            }

            Destroy(fruitCoin.gameObject);
        }
    }
}
