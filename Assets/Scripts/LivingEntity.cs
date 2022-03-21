using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    [SerializeField] private Healthbar statusBar;
    [SerializeField] private float _startingHealth;
    [SerializeField] protected float Health;
    protected bool Dead;
    
    public event System.Action OnDeath;

    private void Awake()
    {
        statusBar = GetComponent<Healthbar>();
    }

    protected virtual void Start()
    {
        Health = _startingHealth;
        statusBar.UpdateHealthBar(_startingHealth, Health);
    }

    public void TakeHit(float damage, Collider collision)
    {
        Health -= damage;
        statusBar.UpdateHealthBar(_startingHealth, Health);

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
