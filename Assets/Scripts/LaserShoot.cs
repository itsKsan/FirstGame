using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class LaserShoot : MonoBehaviour
{
    private NavMeshAgent _pathfinder; 
    private GameObject target;
    private Animator _animator;
    
    [SerializeField] private GameObject laserGun;
    [SerializeField] private LineRenderer lineRenderer;
    
    [Header("Laser Settings")] 
    private float _attackDistanceThreshold;

    [SerializeField] private float fireRate;
    [SerializeField] private float turnSpeed = 2f;

    private float _nextShootTime;
    private Vector3 _offset;

    private void Awake()
    {
        _pathfinder = GetComponentInParent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player");
        lineRenderer = GetComponentInChildren<LineRenderer>();
        _animator = GetComponentInParent<Animator>();
    }

    private void Start()
    {
        _offset = new Vector3(0, 1, 0);
    }

    private void Update()
    {
        if(target == null) return;

        _attackDistanceThreshold = Vector3.Distance(target.transform.position, transform.position);

        LockTarget();

        if (_attackDistanceThreshold <= _pathfinder.stoppingDistance + 4f && Time.time > _nextShootTime)
        {
            _animator.SetBool("LookTarget", true);
            _pathfinder.enabled = false;
            
            _nextShootTime = Time.time + fireRate;
            StartCoroutine(nameof(Fire));
        }
        else
        {
            _animator.SetBool("LookTarget", false);
            lineRenderer.enabled = false;
        }
    }

    private void LockTarget()
    {
        Vector3 direction = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        laserGun.transform.rotation = Quaternion.Slerp(laserGun.transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private IEnumerator Fire()
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, laserGun.transform.position);
        lineRenderer.SetPosition(1, target.transform.position + _offset);

        yield return new WaitForSeconds(1);

        _pathfinder.enabled = true;
    }
}
