using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class CannonShoot : MonoBehaviour
{
    private NavMeshAgent _agent;
    
    [SerializeField] private GameObject shellPrefab;
    [SerializeField] private Transform shellSpawnPos;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject redCircle;

    [Header("Cannon Settings")]
    [SerializeField] private float attackDistanceThreshold = 10;
    [SerializeField] private float projectileSpeed = 15f;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private float fireRate = 1f;

    private float _nextShootTime;
    private Vector3 offset = new Vector3(0, 0.01f, 0);
    
    private void Awake()
    {
        target = GameObject.FindWithTag("Player");
        _agent = GetComponentInParent<NavMeshAgent>();

        attackDistanceThreshold = _agent.stoppingDistance;
    }


    private void Fire()
    {
        GameObject shell = Instantiate(shellPrefab, shellSpawnPos.transform.position, shellSpawnPos.transform.rotation);
        GameObject shellPrediction = Instantiate(redCircle, target.transform.position + offset, Quaternion.identity);
        shell.GetComponent<Rigidbody>().velocity = projectileSpeed * this.transform.forward;
    }

    private void Update()
    {
        if (target == null) return;
        
        if (attackDistanceThreshold <= _agent.stoppingDistance && Time.time > _nextShootTime)
        {
            _nextShootTime = Time.time + fireRate;
            
            Fire();
        }

        Vector3 direction = (target.transform.position - parent.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        parent.transform.rotation = Quaternion.Slerp(parent.transform.rotation, lookRotation, Time.deltaTime * turnSpeed);

        float? angle = RotateTurret();
    }

    
    float? RotateTurret()
    {
        float? angle = CalculateAngle(false);  // true for low, false for high

        if (angle != null)
        {
            this.transform.localEulerAngles = new Vector3(360f - (float)angle, 0f, 0f);
        }
        return angle;
    }
    
    private float? CalculateAngle(bool low)
    {
        Vector3 targetDir = target.transform.position - this.transform.position;
        float y = targetDir.y;
        targetDir.y = 0;
        float x = targetDir.magnitude;
        float gravity = 9.81f;
        float sSqr = projectileSpeed * projectileSpeed;
        float underTheSqrRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr);

        if (underTheSqrRoot >= 0f)
        {
            float root = Mathf.Sqrt(underTheSqrRoot);
            float highAngle = sSqr + root;
            float lowAngle = sSqr - root;

            if (low)
                return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
            else
                return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);
        }
        else
            return null;
    }
}
