using UnityEngine;

public class LaserAim : MonoBehaviour
{
    [SerializeField] private float laserSpeed = 1f;

    public Vector3 EndPos { private get; set; }


    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, EndPos, laserSpeed * Time.deltaTime);

        if (transform.position == EndPos)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetActive(bool state)
    {
        gameObject.SetActive(state);
    }
}