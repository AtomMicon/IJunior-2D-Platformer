using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _fastFallSpeed = 15f;

    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Rigidbody2D _rb;
    private float _moveInput;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
        _rb.linearVelocity = new Vector2(_moveInput * _moveSpeed, _rb.linearVelocity.y);
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
        if (Input.GetKeyDown(KeyCode.W) && Mathf.Abs(_rb.linearVelocity.y) < 0.001f)
        {
            _rb.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Impulse);
        }
    }

    private void FastFallCheck()
    {
        if (Input.GetKeyDown(KeyCode.S) && Mathf.Abs(_rb.linearVelocity.y) > 0.001f)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, -_fastFallSpeed);
        }
    }

    private void UpdateAnimations()
    {
        _animator.SetFloat("speed", Mathf.Abs(_moveInput));

        bool isGrounded = Mathf.Abs(_rb.linearVelocity.y) < 0.001f;

        _animator.SetFloat("velocityY", _rb.linearVelocity.y);
    }
}