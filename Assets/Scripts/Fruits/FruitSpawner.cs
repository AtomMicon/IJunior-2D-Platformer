using System.Collections;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] private FruitCoin[] _fruitPrefabs;
    [SerializeField] private float _respawnTime = 3f;

    private FruitCoin _currentFruit;
    private bool _isRespawning;

    private void Start()
    {
        SpawnFruit();
    }

    private void Update()
    {
        if (_currentFruit == null && !_isRespawning)
        {
            StartCoroutine(RespawnRoutine());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }

    private IEnumerator RespawnRoutine()
    {
        _isRespawning = true;
        
        yield return new WaitForSeconds(_respawnTime);
        SpawnFruit();
        
        _isRespawning = false;
    }

    private void SpawnFruit()
    {
        if (_fruitPrefabs.Length == 0) 
            return;

        int randomIndex = Random.Range(0, _fruitPrefabs.Length);
        _currentFruit = Instantiate(_fruitPrefabs[randomIndex], transform.position, Quaternion.identity);
    }
}