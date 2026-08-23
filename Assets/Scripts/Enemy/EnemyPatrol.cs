using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Vector3[] _waypoints;
    [SerializeField] private float _speed = 3f;
    private int _currentPointIndex = 0;

    private void Start()
    {
        if (_waypoints.Length > 0)
        {
            UpdateFacingDirection(_waypoints[0]);
        }
    }

    private void Update()
    {
        if (_waypoints != null && _waypoints.Length != 0)
        {
            Vector3 target = _waypoints[_currentPointIndex];

            transform.position = Vector2.MoveTowards(transform.position, target, _speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, target) < 0.1f)
            {
                _currentPointIndex = (_currentPointIndex + 1) % _waypoints.Length;
                UpdateFacingDirection(_waypoints[_currentPointIndex]);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_waypoints.Length > 0)
        {
            Gizmos.color = Color.red;
            
            for (int i = 0; i < _waypoints.Length; i++)
            {
                Gizmos.DrawSphere(_waypoints[i], 0.1f);
                
                if (i < _waypoints.Length - 1)
                {
                    Gizmos.DrawLine(_waypoints[i], _waypoints[i + 1]);
                }
                else
                {
                    Gizmos.DrawLine(_waypoints[i], _waypoints[0]);
                }
            }
        }
    }

    private void UpdateFacingDirection(Vector3 targetPosition)
    {
        Vector3 scaler = transform.localScale;

        if (targetPosition.x > transform.position.x)
        {
            scaler.x = Mathf.Abs(scaler.x);
        }
        else if (targetPosition.x < transform.position.x)
        {
            scaler.x = -Mathf.Abs(scaler.x);
        }

        transform.localScale = scaler;
    }
}