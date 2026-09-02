using UnityEngine;


public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private AudioClip _collectSound;
    [SerializeField] private ScoreUpdater _scoreUpdater;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out FruitCoin fruitCoin))
        {
            _scoreUpdater.AddScore(fruitCoin.ScoreValue);

            if (_collectSound != null)
            {
                AudioSource.PlayClipAtPoint(_collectSound, transform.position);
            }

            if(other.TryGetComponent(out HealingFruit healingFruitCoin)) 
            {

            }

            Destroy(fruitCoin.gameObject);
        }
    }
}
