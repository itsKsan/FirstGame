using System;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Vector3 _velocity;
    
    
    [SerializeField] private Transform playerImage;
    [SerializeField] private GameObject playerHeadFace;
    [SerializeField] private GameObject playerHeadBack;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    } 

    public void Move(Vector3 velocity)
    {
        _velocity = velocity;
    }
    
 
    private void FixedUpdate() 
    { 
        _rigidbody.MovePosition(_rigidbody.position + _velocity * Time.deltaTime);
    }

    public void LookAt(Vector3 lookPoint)
    {
        /*
        var heightCorrectionPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectionPoint);
        */

        if (lookPoint.x < transform.position.x) 
        {
            playerImage.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        if (lookPoint.x > transform.position.x)
        {
            playerImage.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (lookPoint.z > transform.position.z + 1f)
        {
            playerHeadBack.SetActive(true);
            playerHeadFace.SetActive(false);
        }
        else
        {
            playerHeadBack.SetActive(false);
            playerHeadFace.SetActive(true);
        }
    }
}
