using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
public class EnemyChase : MonoBehaviour
{
    [SerializeField] private float _chaseSpeed = 4f;
    [SerializeField] private float _attackCooldown = 1.5f;

    private EnemyAI _brain;
    private float _lastAttackTime;
    private readonly int _attackAnimTrigger = Animator.StringToHash("Attack1");

    private void Awake()
    {
        _brain = GetComponent<EnemyAI>();
    }

    private void Update()
    {
        if (_brain.Player == null) return;

        float distance = Vector2.Distance(transform.position, _brain.Player.position);

        if (distance <= _brain.AttackRange)
        {
            Attack();
        }
        else
        {
            Chase();
        }
    }

    private void Chase()
    {
        Vector3 target = new Vector3(_brain.Player.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, target, _chaseSpeed * Time.deltaTime);

        _brain.UpdateFacingDirection(target);
    }

    private void Attack()
    {
        if (Time.time >= _lastAttackTime + _attackCooldown)
        {
            _lastAttackTime = Time.time;

            if (_brain.EnemyAnimator != null)
            {
                _brain.EnemyAnimator.SetTrigger(_attackAnimTrigger);
            }

            if (_brain.Player.TryGetComponent(out DamageHandler playerDamageHandler))
            {
                playerDamageHandler.TakeDamage(_brain.Damage, transform.position);
            }
        }
    }
}