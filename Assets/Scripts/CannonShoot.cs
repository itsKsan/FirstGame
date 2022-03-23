using System;
using Unity.Mathematics;
using UnityEngine;

public class CannonShoot : MonoBehaviour
{
    [SerializeField] private GameObject shellPrefab;
    [SerializeField] private Transform shellSpawnPos;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject parent;

    [Header("Cannon Settings")]
    [SerializeField]private float speed = 15f;
    [SerializeField] private float turnSpeed = 5f;
    private void Awake()
    {
        target = GameObject.FindWithTag("Player");
    }

    private void Fire()
    {
        GameObject shell = Instantiate(shellPrefab, shellSpawnPos.transform.position, shellSpawnPos.transform.rotation);
        shell.GetComponent<Rigidbody>().velocity = speed * this.transform.forward;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
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
        float sSqr = speed * speed;
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
