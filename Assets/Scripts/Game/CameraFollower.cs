using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _smoothTime = 0.3f;
    [SerializeField] private float _deadZone = 2f;

    private Vector3 _velocity = Vector3.zero;

    private void LateUpdate()
    {        
        if (_player == null)
            return;

        CalculateDistance();
    }

    private void CalculateDistance()
    {
        Vector3 currentPos = transform.position;
        float distanceX = _player.position.x - currentPos.x;
        float moveZone = _deadZone;
        
        if (Mathf.Abs(distanceX) > _deadZone)
        {
            if (distanceX > 0)
            {
                moveZone = -_deadZone;
            }

            Move(_player.position.x + moveZone);
        }
    }

    private void Move(float targetX)
    {
        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
    }
}