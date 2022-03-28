using UnityEngine;

public class RotateOverTime : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 2f;
    private void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
    }
}
