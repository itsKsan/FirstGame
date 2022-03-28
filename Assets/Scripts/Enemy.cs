using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEntity
{
    private enum State
    {
        Idle,
        Chasing,
        Attacking
    }
    private State _currentState;
    
    private Animator _animator;
    private Player _player;

    [SerializeField] private Transform[] _attackBox;

    [Header("Settings")]
    [SerializeField] private float attackDistanceThreshold;
    [SerializeField] private float pathRefreshTime = 0.25f;

    private NavMeshAgent _pathfinder; 

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _pathfinder = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
    }

    protected override void Start()
    {
        base.Start();

        _currentState = State.Chasing;
        StartCoroutine(UpdatePath());
    }

    private void Update()
    {
        if(_player == null) return;
        
        attackDistanceThreshold = Vector3.Distance(_player.transform.position, transform.position);

        if (attackDistanceThreshold <= _pathfinder.stoppingDistance && !Dead)
        {
            Attack();
        }
        else
        {
            _pathfinder.enabled = true;
            _currentState = State.Chasing;
            _animator.SetBool("Attack", false);
        }
    }

    private IEnumerator UpdatePath()
    {
        while (_player != null)
        {
            var targetPosition = new Vector3(_player.transform.position.x, 0, _player.transform.position.z);
            if (!Dead && _currentState == State.Chasing && _pathfinder.enabled)
            {
                _pathfinder.SetDestination(targetPosition);
            }
            yield return new WaitForSeconds(pathRefreshTime);
        }
    }

    private void Attack()
    {
        _currentState = State.Attacking;
        _pathfinder.enabled = false;
        _animator.SetBool("Attack", true);
    }
}