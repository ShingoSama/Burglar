using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingAxe : MonoBehaviour
{
    private Rigidbody2D RB2D;
    public float Speed;
    public float leftLimit;
    public float rightLimit;
    private float nextAttackTime { get; set; }
    private float attackDamage { get; set; }
    void Start()
    {
        RB2D = gameObject.GetComponent<Rigidbody2D>();
        RB2D.angularVelocity = 500;
    }
    void Update()
    {
        SwingMove();
    }
    public void SwingMove()
    {
        if (transform.rotation.z < rightLimit && RB2D.angularVelocity > 0 && RB2D.angularVelocity < Speed)
        {
            RB2D.angularVelocity = Speed;
        }
        else if (transform.rotation.z > leftLimit && RB2D.angularVelocity < 0 && RB2D.angularVelocity > -Speed)
        {
            RB2D.angularVelocity = -Speed;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Time.time > nextAttackTime)
            {
                float maxhealt = collision.gameObject.GetComponent<PlayerController>().playerStatsBase.MaxHealth;
                attackDamage = (25f * maxhealt) / 100f;
                collision.gameObject.GetComponent<PlayerController>().TakeDamage(attackDamage, gameObject.transform.position.x);
                nextAttackTime = Time.time + 2f;
            }
        }
    }
}
