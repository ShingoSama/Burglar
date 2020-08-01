using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private float nextAttackTime { get; set; }
    public float attackDamage;
    public float speedArrow = 50f;
    public Rigidbody2D arrowRB2D;
    public Transform arrowTransform;
    void Start()
    {
        if (arrowTransform.localScale.x == -1)
        {
            arrowRB2D.velocity = new Vector2(speedArrow * arrowTransform.localScale.x, arrowRB2D.velocity.y);
            arrowRB2D.AddForce(Vector2.left * speedArrow, ForceMode2D.Impulse);
        }
        else
        {
            arrowRB2D.velocity = new Vector2(speedArrow * arrowTransform.localScale.x, arrowRB2D.velocity.y);
            arrowRB2D.AddForce(Vector2.right * speedArrow, ForceMode2D.Impulse);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(attackDamage, gameObject.transform.position.x);
            nextAttackTime = Time.time + 2f;
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Escenario"))
        {
            Destroy(gameObject);
        }
    }
    public void SetAttackDamage(float attackdamage)
    {
        attackDamage = attackdamage;
    }
}
