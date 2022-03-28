using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Laser : MonoBehaviour
{
    private NavMeshAgent _pathfinder; 
    private GameObject _target;
    private Animator _animator;

    [SerializeField] private GameObject[] laserGun;
    [SerializeField] private LineRenderer[] lineRenderer;
    [SerializeField] private GameObject laserTarget;
    
    [Header("Laser Settings")]
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float turnSpeed = 2f;

    private GameObject laserAim;
    private float _attackDistanceThreshold;
    private float _nextShootTime;
    private bool _instantiated = true;
    
    private void Awake()
    {
        _pathfinder = GetComponent<NavMeshAgent>();
        _target = GameObject.FindWithTag("Player");
        _animator = GetComponentInChildren<Animator>();
    }


    private void Update()
    {
        if(_target == null) return;

        _attackDistanceThreshold = Vector3.Distance(_target.transform.position, transform.position);

        if (_attackDistanceThreshold <= _pathfinder.stoppingDistance + 4f)
        {
            _animator.SetBool("LookTarget", true);
            _pathfinder.enabled = false;

            if (Time.time > _nextShootTime)
            {
                InstantiateLaserTarget(_instantiated); 
                StartCoroutine(nameof(Fire));
            }
            
        }
        else
        {
            _animator.SetBool("LookTarget", false);
            
            lineRenderer[0].enabled = false;
            lineRenderer[1].enabled = false;
        }
    }

    private void LockTarget()
    {
        Vector3 direction = _target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    
    private IEnumerator Fire()
    {
        LockTarget();

        for (int i = 0; i < laserGun.Length; i++)
        {
            lineRenderer[i].enabled = true;
            lineRenderer[i].SetPosition(0, laserGun[i].transform.position);
            lineRenderer[i].SetPosition(1, laserAim.transform.position);
        }
        
        yield return new WaitForSeconds(fireRate);
        lineRenderer[0].enabled = false;
        lineRenderer[1].enabled = false;
        
        _nextShootTime = Time.time + fireRate;
        _pathfinder.enabled = true;
        _instantiated = true;
    }

    private void InstantiateLaserTarget(bool spawn)
    {
        if (!spawn) return;
        
        _instantiated = false;
        laserAim = Instantiate(laserTarget, _target.GetComponent<Player>().dots[0].position,
            Quaternion.identity);
        laserAim.GetComponent<LaserAim>().SetActive(true);
        laserAim.GetComponent<LaserAim>().EndPos = _target.GetComponent<Player>().dots[1].position;
        
    }
}
