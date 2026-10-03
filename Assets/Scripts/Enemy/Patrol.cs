using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
public class Patrol : MonoBehaviour
{
    [SerializeField] private Vector3[] _waypoints;
    [SerializeField] private float _patrolSpeed = 2f;

    private EnemyAI _brain;
    private int _currentPointIndex = 0;

    private void Awake()
    {
        _brain = GetComponent<EnemyAI>();
    }

    private void Start()
    {
        FindNearestWaypoint();
    }

    private void Update()
    {
        if (_waypoints == null || _waypoints.Length == 0) return;

        Vector3 target = new Vector3(_waypoints[_currentPointIndex].x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, target, _patrolSpeed * Time.deltaTime);

        _brain.UpdateFacingDirection(target);

        if (Mathf.Abs(transform.position.x - target.x) < 0.1f)
        {
            _currentPointIndex = (_currentPointIndex + 1) % _waypoints.Length;
        }
    }

    private void FindNearestWaypoint()
    {
        if (_waypoints == null || _waypoints.Length == 0) return;

        float minDistance = float.MaxValue;
        for (int i = 0; i < _waypoints.Length; i++)
        {
            float distance = Vector2.Distance(transform.position, _waypoints[i]);
            if (distance < minDistance)
            {
                minDistance = distance;
                _currentPointIndex = i;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_waypoints != null && _waypoints.Length > 0)
        {
            Gizmos.color = Color.green;

            for (int i = 0; i < _waypoints.Length; i++)
            {
                Gizmos.DrawSphere(_waypoints[i], 0.1f);

                if (i < _waypoints.Length - 1)
                    Gizmos.DrawLine(_waypoints[i], _waypoints[i + 1]);
                else
                    Gizmos.DrawLine(_waypoints[i], _waypoints[0]);
            }
        }
    }
}