using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    // Monster¿¡ 
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Monster"))
    //    {
    //        Monster monster = other.GetComponent<Monster>();

    //        if (monster != null)
    //        {
    //            int damage = player.status.damage;
    //            monster.TakeDamage(damage);
    //        }
    //    }
    //}
}
