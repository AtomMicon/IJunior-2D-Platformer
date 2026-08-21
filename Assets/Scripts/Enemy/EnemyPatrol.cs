using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Vector3[] waypoints;
    [SerializeField] private float speed = 3f;
    private int currentPointIndex = 0;

    private void Start()
    {
        if (waypoints.Length > 0)
        {
            UpdateFacingDirection(waypoints[0]);
        }
    }

    private void Update()
    {
        if (waypoints == null || waypoints.Length == 0)
        { 
            return; 
        }
        
        Vector3 target = waypoints[currentPointIndex];
        
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            currentPointIndex = (currentPointIndex + 1) % waypoints.Length;
            UpdateFacingDirection(waypoints[currentPointIndex]);
        }
    }

    private void OnDrawGizmos()
    {
        if (waypoints.Length > 0)
        {
            Gizmos.color = Color.red;
            
            for (int i = 0; i < waypoints.Length; i++)
            {
                Gizmos.DrawSphere(waypoints[i], 0.1f);
                
                if (i < waypoints.Length - 1)
                {
                    Gizmos.DrawLine(waypoints[i], waypoints[i + 1]);
                }
                else
                {
                    Gizmos.DrawLine(waypoints[i], waypoints[0]);
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