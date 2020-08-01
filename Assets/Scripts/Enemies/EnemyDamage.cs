using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private float nextAttackTime { get; set; }
    private float attackDamage { get; set; }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Time.time > nextAttackTime)
            {
                attackDamage = GetComponentInParent<Enemy>().entity.Damage;
                collision.gameObject.GetComponent<PlayerController>().TakeDamage(attackDamage, gameObject.transform.position.x);
                nextAttackTime = Time.time + 1f;
            }
        }
    }
}
