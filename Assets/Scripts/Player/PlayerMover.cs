using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Health), typeof(CapsuleCollider2D))]
[RequireComponent (typeof(PlayerInput))]
public class PlayerMover : MonoBehaviour
{
    public const string Horizontal = nameof(Horizontal);

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private float _currentRotation = 0;
    private bool _isGrounded = false;
    private Health _playerHealth;
    private CapsuleCollider2D _colliderPlayer;
    private Rigidbody2D _rigidbody;
    private PlayerInput _playerInput;

    public event Action Run;
    public event Action StoppedRun;
    public event Action Jumped;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerHealth = GetComponent<Health>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _colliderPlayer = GetComponent<CapsuleCollider2D>();
    }

    private void OnEnable()
    {
        _playerHealth.Died += DisableComponentsAtDeath;
    }

    private void OnDisable()
    {
        _playerHealth.Died -= DisableComponentsAtDeath;
    }

    private void FixedUpdate()
    {
        if (_playerInput.IsTryingJump)
        {
            Jump();

            _isGrounded = false;
        }

        _playerInput.DeActivateJumpTrying();
    }

    private void Update()
    {
        if(_playerInput.IsMoving)
        {
            Move();
            Run?.Invoke();
        }
        else
        {
            StoppedRun?.Invoke();
        }
    }

    private void Move()
    {
        Run?.Invoke();

        Vector3 direction = Vector3.right * _playerInput.HorizontalDirection;

        transform.position += direction * _speed * Time.deltaTime;

        if (direction.x != 0)
        {
            _currentRotation = direction.x < 0.0f ? 180 : 0;
            transform.rotation = Quaternion.Euler(0, _currentRotation, 0);
        }
    }

    private void Jump()
    {
        if(_isGrounded)
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            Jumped?.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out Ground ground))
        {
            _isGrounded = true;
        }
    }

    private void DisableComponentsAtDeath()
    {
        _rigidbody.simulated = false;
        _colliderPlayer.enabled = false;
        enabled = false;
    }
}