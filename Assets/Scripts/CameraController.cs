using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    public Transform target;
    public Transform camTransform;
    public Vector3 offset;
    public float smoothTime = 0.3f;
    
    private Vector3 _velocity = Vector3.zero;
 
    private void Start()
    {
        offset = camTransform.position - target.position;
    }
 
    private void LateUpdate()
    {
        if (target == null) return;

        // update position
        Vector3 targetPosition = target.position + offset;
        camTransform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
 
        // update rotation
        transform.LookAt(target);
    }
}
