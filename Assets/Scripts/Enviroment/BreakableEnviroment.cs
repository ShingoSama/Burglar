using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BreakableEnviroment : MonoBehaviour
{
    public Animator animator;
    public Light2D light2D;
    public float MaxHealth = 1;
    private float currentHealth;
    private bool _isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
    }

    public void TakeDamage(float damage, float positionPlayer)
    {
        if (!_isDead)
        {
            currentHealth -= damage;
            if (positionPlayer > gameObject.GetComponent<Transform>().position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            if (currentHealth <= 0)
            {
                DestroyEnviroment();
            }
        }
    }
    void DestroyEnviroment()
    {
        _isDead = true;
        light2D.enabled = false;
        animator.SetTrigger("IsDead");
        Destroy(gameObject, 2f);
    }
}
