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

        MoveCamera(CheckDistance());
    }

    private float CheckDistance()
    {
        Vector3 currentPos = transform.position;
        float targetX = currentPos.x;
        float distanceX = _player.position.x - currentPos.x;

        if (Mathf.Abs(distanceX) > _deadZone)
        {
            if (distanceX > 0)
                targetX = _player.position.x - _deadZone;
            else
                targetX = _player.position.x + _deadZone;
        }

        return targetX;
    }

    private void MoveCamera(float targetX)
    {
        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
    }
}