using System.Collections;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] private FruitCoin[] _fruitPrefabs;
    [SerializeField] private FruitCoin _healingFruitPrefab;
    [SerializeField, Range(0f, 100f)] private float _healSpawnChance = 10f;
    [SerializeField] private float _respawnTime = 3f;

    private FruitCoin _currentFruit;
    private bool _isRespawning;
    private float _spawnChanceMax = 100f;
    private float _spawnChanceMin = 0f;

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

        FruitCoin prefabToSpawn;

        if (Random.Range(_spawnChanceMin, _spawnChanceMax) <= _healSpawnChance && _healingFruitPrefab != null)
        {
            prefabToSpawn = _healingFruitPrefab;
        }
        else
        {
            int randomIndex = Random.Range(0, _fruitPrefabs.Length);
            prefabToSpawn = _fruitPrefabs[randomIndex];
        }

        _currentFruit = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
    }
}