using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    [SerializeField] private float _startingHealth;

    [SerializeField] protected float Health;
    protected bool Dead;

    public event System.Action OnDeath;
    
    protected virtual void Start()
    {
        Health = _startingHealth;
    }

    public void TakeHit(float damage, Collider collision)
    {
        Health -= damage;

        if (Health <= 0 && !Dead)
        {
            Die();
        }
    }

    protected void Die()
    {
        Dead = true;
        OnDeath?.Invoke(); // null-propagating
        Destroy(gameObject);
    }
}
