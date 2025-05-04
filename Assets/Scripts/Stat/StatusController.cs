using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusController : MonoBehaviour, IDamagable
{
    protected EntityFX fx;
    public bool isDead = false;

    protected virtual void Awake()
    {
        fx = GetComponent<EntityFX>();
    }

    public virtual void TakeDamage(float amount, bool isHitting = false)
    {
        
    }
}
