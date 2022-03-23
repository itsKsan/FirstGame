using System;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField] private GameObject explosion = null;
    
    private void OnCollisionEnter(Collision collision)
    {
        return;
        GameObject exp = Instantiate(explosion, this.transform.position, Quaternion.identity);
        Destroy(exp, 0.5f);
        Destroy(this.gameObject);
    }
}
