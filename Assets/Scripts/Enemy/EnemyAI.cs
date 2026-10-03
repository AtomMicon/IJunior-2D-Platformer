using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Ссылки")]
    public Animator EnemyAnimator;
    [HideInInspector] public Transform Player;

    [Header("Настройки зрения и атаки")]
    public float VisionRange = 5f;
    public float AttackRange = 1.2f;
    public int Damage = 1;

    private Patrol _patrolState;
    private EnemyChase _chaseState;

    private void Awake()
    {
        _patrolState = GetComponent<Patrol>();
        _chaseState = GetComponent<EnemyChase>();

        PlayerMover playerMover = FindFirstObjectByType<PlayerMover>();
        if (playerMover != null)
        {
            Player = playerMover.transform;
        }
    }

    private void Update()
    {
        if (Player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, Player.position);

        if (distanceToPlayer <= VisionRange)
        {
            SwitchState(_chaseState, _patrolState);
        }
        else
        {
            SwitchState(_patrolState, _chaseState);
        }
    }

    private void SwitchState(MonoBehaviour activeState, MonoBehaviour inactiveState)
    {
        if (!activeState.enabled) activeState.enabled = true;
        if (inactiveState.enabled) inactiveState.enabled = false;
    }

    public void UpdateFacingDirection(Vector3 targetPosition)
    {
        Vector3 scaler = transform.localScale;

        if (targetPosition.x > transform.position.x)
            scaler.x = Mathf.Abs(scaler.x);
        else if (targetPosition.x < transform.position.x)
            scaler.x = -Mathf.Abs(scaler.x);

        transform.localScale = scaler;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, VisionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}