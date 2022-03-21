using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float lifeTime  = 3f;
    
    [SerializeField] private float _speed;
    [SerializeField] private float _damage = 1;

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    private void Update()
    {
        float moveDistance = _speed * Time.deltaTime;
        transform.Translate(Vector3.forward * moveDistance);
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            OnHitObject(other);
        }

        if (other.gameObject.CompareTag("Obsticle"))
        {
            Destroy(gameObject);
        }
    }

    private void OnHitObject(Collider hit)
    {
        var damageableObject = hit.GetComponent<IDamageable>();
        damageableObject?.TakeHit(_damage,hit);

         Destroy(gameObject);
    }
}
