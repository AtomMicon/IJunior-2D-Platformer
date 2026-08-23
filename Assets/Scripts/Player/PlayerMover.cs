using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _fastFallSpeed = 15f;

    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private float _moveInput;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        MoveCheck();
        JumpCheck();
        FastFallCheck();
        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        _rigidBody.linearVelocity = new Vector2(_moveInput * _moveSpeed, _rigidBody.linearVelocity.y);
    }

    private void MoveCheck()
    {
        _moveInput = Input.GetAxisRaw("Horizontal");

        if (_moveInput > 0)
            Move(1);
        else if (_moveInput < 0)
            Move(-1);
    }

    private void Move(float vector)
    {
        transform.localScale = new Vector3(vector, 1, 1);
    }

    private void JumpCheck()
    {
        if (Input.GetKeyDown(KeyCode.W) && Mathf.Abs(_rigidBody.linearVelocity.y) < 0.001f)
        {
            _rigidBody.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Impulse);
        }
    }

    private void FastFallCheck()
    {
        if (Input.GetKeyDown(KeyCode.S) && Mathf.Abs(_rigidBody.linearVelocity.y) > 0.001f)
        {
            _rigidBody.linearVelocity = new Vector2(_rigidBody.linearVelocity.x, -_fastFallSpeed);
        }
    }

    private void UpdateAnimations()
    {
        _animator.SetFloat("speed", Mathf.Abs(_moveInput));

        bool isGrounded = Mathf.Abs(_rigidBody.linearVelocity.y) < 0.001f;

        _animator.SetFloat("velocityY", _rigidBody.linearVelocity.y);
    }
}