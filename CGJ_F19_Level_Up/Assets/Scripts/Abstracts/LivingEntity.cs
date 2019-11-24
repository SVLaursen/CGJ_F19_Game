using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, ITakeDamage
{
    [SerializeField] protected int health;
    public int Health { get;  set; }

    public event Action OnDeath;
    protected bool _isDead;

    protected virtual void OnEnable()
    {
        Health = health;
        _isDead = false;
    }

    protected virtual void Start()
    {
        
    }
    
    public virtual void TakeHit(int damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        TakeDamage(damage);
    }

    public virtual void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0 && !_isDead)
            Die();
    }

    protected virtual void Die()
    {
        _isDead = true;

        if (OnDeath != null)
            OnDeath();

        gameObject.SetActive(false);
    }
}
