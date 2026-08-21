using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FruitCoin : MonoBehaviour
{
    [SerializeField] private int _scoreValue = 1;
    [SerializeField] private float _bobSpeed = 2f;
    [SerializeField] private float _bobHeight = 0.2f;

    public int ScoreValue => _scoreValue;

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
}