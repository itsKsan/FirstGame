using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _damage = 1f;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnHitObject(other);
        }
    }
    
    private void OnHitObject(Collider hit)
    {
        var damageableObject = hit.GetComponent<IDamageable>();
        damageableObject?.TakeHit(_damage,hit);
    }
}
