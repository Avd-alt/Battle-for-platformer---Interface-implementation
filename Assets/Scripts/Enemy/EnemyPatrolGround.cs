using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D), typeof(Health))]
public class EnemyPatrolGround : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private PlayerDetecter _detecterPlayer;

    private float _currentRotation = 0;
    private float _originalSpeed = 1.5f;
    private float _speed;
    private int _randomIndexPoint;
    private Rigidbody2D _rigidbodyEnemy;
    private CapsuleCollider2D _colliderEnemy;
    private Health _healthEnemy;

    public event Action Stayed;
    public event Action Run;

    private void Awake()
    {
        _rigidbodyEnemy = GetComponent<Rigidbody2D>();
        _colliderEnemy = GetComponent<CapsuleCollider2D>();
        _healthEnemy = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _healthEnemy.Died += DisableComponentsAtDeath;
    }

    private void OnDisable()
    {
        _healthEnemy.Died -= DisableComponentsAtDeath;
    }

    private void Start()
    {
        _speed = _originalSpeed;
        _randomIndexPoint = GetRandomIndex();
    }

    private void Update()
    {
        if(_detecterPlayer.PlayerHealth != null)
        {
            MoveToPlayer(_detecterPlayer.PlayerHealth );
        }
        else
        {
            MoveToPoint();
        }
    }

    private void MoveToPoint()
    {
        float distanceChangePoint = 0.2f;


        Vector3 direction = _points[_randomIndexPoint].position - transform.position;
        transform.position = Vector2.MoveTowards(transform.position, _points[_randomIndexPoint].position, _speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, _points[_randomIndexPoint].position) < distanceChangePoint)
        {
            _randomIndexPoint = GetRandomIndex();
        }

        if (direction.x != 0)
        {
            _currentRotation = direction.x > 0.0f ? 180 : 0;
            transform.rotation = Quaternion.Euler(0, _currentRotation, 0);
        }
    }

    private void MoveToPlayer(Health playerHealth)
    {
        float distanceToAttack = 1f;
        float distanceToPlayer = Vector2.Distance(transform.position, playerHealth.transform.position);

        if (distanceToPlayer < distanceToAttack)
        {
            StopMove();
        }
        else
        {
            _speed = _originalSpeed;

            Run?.Invoke();

            transform.position = Vector2.MoveTowards(transform.position, playerHealth.transform.position, _speed * Time.deltaTime);
        }
    }

    private void StopMove()
    {
        _speed = 0;

        Stayed?.Invoke();
    }

    private void DisableComponentsAtDeath()
    {
        _rigidbodyEnemy.simulated = false;
        _colliderEnemy.enabled = false;
        enabled = false;
    }

    private int GetRandomIndex() => Random.Range(0, _points.Length);
}