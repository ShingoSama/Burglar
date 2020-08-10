using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstaDead : MonoBehaviour
{
    private float attackDamage;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            attackDamage = collision.gameObject.GetComponent<PlayerController>().playerStatsBase.MaxHealth;
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(attackDamage, gameObject.transform.position.x);
        }
    }
}
