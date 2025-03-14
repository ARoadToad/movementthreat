using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage;
    public float tickCooldownCount;
    bool cooldown;
    private void OnCollisionEnter(Collision collision)
    {
        if (!cooldown)
        {
            Damagable damagable = collision.gameObject.GetComponentInChildren<Damagable>();
            if(damagable != null)
            {
                damagable.TookDamage(damage);
                Invoke(nameof(CooldownFalse), tickCooldownCount);
                cooldown = true;
            }
            
        }
    }

    void CooldownFalse()
    {
        cooldown = false;
    }
}
