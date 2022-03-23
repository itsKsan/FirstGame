using System;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField] private GameObject explosionParticle;
    
    [Header("Settings")]
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float explosiveForce = 500;
    [SerializeField] private float damage = 3f;
    
    private void OnCollisionEnter(Collision hit)
    {
        GameObject explosion = Instantiate(explosionParticle, this.transform.position, Quaternion.identity);

        Collider[] hitColliders = Physics.OverlapSphere(explosion.transform.position, explosionRadius);
        foreach (var obj in hitColliders)
        {
            if (obj.CompareTag("Player"))
            {
                var damageableObject = obj.GetComponent<IDamageable>();
                damageableObject?.TakeHit(damage,null);
            }
            
            var rb = obj.GetComponent<Rigidbody>();
            if (rb == null) continue;
            
            rb.AddExplosionForce(explosiveForce, transform.position, explosionRadius, 1);
        }
        
        
        Destroy(explosion, 0.5f);
        Destroy(this.gameObject);
    }
} 
