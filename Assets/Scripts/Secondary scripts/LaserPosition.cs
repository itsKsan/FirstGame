using System.Collections;
using UnityEngine;

public class LaserPosition : MonoBehaviour
{
    [SerializeField] private Transform dotStart;
    [SerializeField] private Transform dotEnd;
    [SerializeField] private GameObject laserDot;
    [SerializeField] private float moveSeed;

    public Vector3 dotA;
    public Vector3 dotB;

    private bool startLaser;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(nameof(LaserMoving));
        }
    }

    private IEnumerator LaserMoving()
    {
        SetDots();
        GameObject laserEndPosition = Instantiate(laserDot, dotStart.position, Quaternion.identity);
        laserEndPosition.transform.position = Vector3.MoveTowards(transform.position, dotB, moveSeed * Time.deltaTime);
        
        
        yield return new WaitForSeconds(20);
        Destroy(laserEndPosition.gameObject);
    }
    
    private void SetDots()
    {
        dotA = dotStart.position;
        dotB = dotEnd.position;
    }
}
