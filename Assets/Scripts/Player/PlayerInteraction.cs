using UnityEngine;


public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private AudioClip _collectSound;
    [SerializeField] private AudioClip _healingSound;
    [SerializeField] private ScoreUpdater _scoreUpdater;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out FruitCoin fruitCoin))
        {
            _scoreUpdater.AddScore(fruitCoin.ScoreValue);

            if(other.TryGetComponent(out HealingFruit healingFruitCoin)) 
            {
                if (TryGetComponent(out Health health) && _healingSound != null)
                {
                    health.Heal(healingFruitCoin.HealValue);
                    AudioSource.PlayClipAtPoint(_healingSound, transform.position);
                }
            }
            
            else if (_collectSound != null)
            {
                AudioSource.PlayClipAtPoint(_collectSound, transform.position);
            }

            Destroy(fruitCoin.gameObject);
        }
    }
}
