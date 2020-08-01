using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Estado Enemigo
    private enum StateEnemy
    {
        Idle,
        Moving,
        Knockback,
        Attaking,
        Dead
    }
    private StateEnemy currentState;
    #endregion Estado Enemigo
    public Entity entity;
    #region Propiedades Enemy Privadas
    private bool wallDetected;
    private bool groundDetected;
    private bool isWaiting;
    private bool isAttacking;
    private bool isTakeDamage;
    private bool isDead;
    private float currentHealth { get; set; }
    private int facingDirection { get; set; }
    private float nextAttackTime { get; set; }
    private float attackRate { get; set; }
    private float timeTakeDamage { get; set; }
    private float movementSpeed { get; set; }
    private float movementNormalSpeed { get; set; }
    private float movementAgroSpeed { get; set; }
    private float agroRange { get; set; }
    private GameObject enemyGO { get; set; }
    private Rigidbody2D enemyRB2D { get; set; }
    private Animator enemyAnimator { get; set; }
    private Vector2 movement { get; set; }
    #endregion Propiedades Privadas
    #region Propiedades Publicas
    private float MaxHealth;
    private float attackDamage;
    public float groundCheckDistance;
    public float wallCheckDistance;
    public Transform groundCheck;
    public Transform wallCheck;
    public Transform attackMelee;
    public Transform attackRange;
    public Transform positionPlayer;
    public LayerMask wallLayer;
    public LayerMask targetLayer;
    public LayerMask groundLayer;
    public Vector2 movementDamage;
    #endregion Propiedades Publicas
    #region Funciones Privadas
    private void Awake()
    {
        MaxHealth = entity.MaxHealth;
        currentHealth = MaxHealth;
        attackDamage = entity.Damage;
        enemyGO = gameObject;
        enemyRB2D = gameObject.GetComponent<Rigidbody2D>();
        enemyAnimator = gameObject.GetComponent<Animator>();
        movementNormalSpeed = entity.MovementNormalSpeed;
        movementSpeed = movementNormalSpeed;
        movementAgroSpeed = entity.MovementAgroSpeed;
        agroRange = entity.AgroRange;
        facingDirection = 1;
        nextAttackTime = 0f;
        attackRate = 1f;
    }
    void Start()
    {
        
    }
    void Update()
    {
        if (!isDead)
        {
            if (Time.time >= timeTakeDamage)
            {
                if (!isAttacking && !isWaiting && !isTakeDamage)
                {
                    GroundDetected();
                    WallDetected();
                    if (!groundDetected || wallDetected)
                    {
                        Flip();
                    }
                    if (!positionPlayer.GetComponent<PlayerController>().PlayerIsDead())
                    {
                        AgroRangeDeterminate();
                    }
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (!isDead)
        {
            if (!isWaiting && !isAttacking && !isTakeDamage)
            {
                Moving();
            }
        }
    }
    private void LateUpdate()
    {
        enemyAnimator.SetBool("Idle", enemyRB2D.velocity == Vector2.zero);
        if (enemyAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            FreezeEnemy();
            isAttacking = true;
        }
        else if(isAttacking)
        {
            UnFreezeEnemy();
            isAttacking = false;
        }
        if (enemyAnimator.GetCurrentAnimatorStateInfo(0).IsTag("TakeDamage"))
        {
            isTakeDamage = true;
        }
        else
        {
            isTakeDamage = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isDead)
        {
            if (Time.time >= nextAttackTime)
            {
                if (isAttacking == false && collision.CompareTag("Player") && isTakeDamage == false)
                {
                    if (attackMelee.GetComponent<Collider2D>().IsTouching(positionPlayer.GetComponent<Collider2D>()))
                    {
                        DontMove();
                        enemyAnimator.SetTrigger("IsAttacking1");
                        nextAttackTime = Time.time + attackRate;
                    }
                    else if (attackRange.GetComponent<Collider2D>().IsTouching(positionPlayer.GetComponent<Collider2D>()))
                    {
                        DontMove();
                        enemyAnimator.SetTrigger("IsAttacking2");
                        GetComponent<Projectile>().Shoot();
                        nextAttackTime = Time.time + attackRate;
                    }
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isAttacking && !isTakeDamage)
        {

        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {

    }
    private void OnCollisionExit2D(Collision2D collision)
    {

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + (facingDirection * wallCheckDistance), wallCheck.position.y));
    }
    #endregion Funciones Privadas
    #region Funciones Basicas
    private void Flip()
    {
        facingDirection *= -1;
        float localScaleX = enemyGO.transform.localScale.x;
        localScaleX = localScaleX * -1f;
        enemyGO.transform.localScale = new Vector3(localScaleX, enemyGO.transform.localScale.y, enemyGO.transform.localScale.z);
    }
    public int GetFacingDirection()
    {
        return facingDirection;
    }
    public float GetDamage()
    {
        return entity.Damage;
    }
    public void Moving()
    {
        movement = new Vector2(movementSpeed * facingDirection, enemyRB2D.velocity.y);
        enemyRB2D.velocity = movement;
    }
    public void DontMove()
    {
        movement = new Vector2(0, 0);
        enemyRB2D.velocity = movement;
    }
    private void WallDetected()
    {
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, (facingDirection * wallCheckDistance), wallLayer);
    }
    private void GroundDetected()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
    }
    private void AgroRangeDeterminate()
    {
        float distToPlayer = Vector2.Distance(enemyGO.transform.position, positionPlayer.position);
        if (distToPlayer < agroRange)
        {
            movementSpeed = movementAgroSpeed;
            ChasePlayer();
        }
        else
        {
            movementSpeed = movementNormalSpeed;
        }
    }
    private void ChasePlayer()
    {
        if (enemyGO.transform.position.x < positionPlayer.position.x)
        {
            if (facingDirection == -1)
            {
                Flip();
            }
        }
        else
        {
            if (facingDirection == 1)
            {
                Flip();
            }
        }
    }
    public void TakeDamage(float damage, float positionPlayer)
    {
        if (currentHealth > 0)
        {
            currentHealth -= (damage - entity.Defense);
            if (positionPlayer < enemyGO.transform.position.x)
            {
                if (movementDamage.x < 0)
                {
                    movementDamage.x *= -1;
                }
                if (facingDirection == 1)
                {
                    Flip();
                }
            }
            else
            {
                if (movementDamage.x > 0)
                {
                    movementDamage.x *= -1;
                }
                if (facingDirection == -1)
                {
                    Flip();
                }
            }
            UnFreezeEnemy();
            DamageKnockBack();
            timeTakeDamage = Time.time + 2f;
            if (currentHealth <= 0)
            {
                EnemyDie();
            }
        }
    }
    public void EnemyDie()
    {
        enemyAnimator.SetBool("IsDead", true);
        isDead = true;
        Destroy(gameObject, 3f);
    }
    public void DamageKnockBack()
    {
        enemyRB2D.velocity = new Vector2(movementDamage.x, movementDamage.y); ;
        enemyAnimator.SetTrigger("Hurt");
    }
    public void FreezeEnemy()
    {
        enemyRB2D.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    public void UnFreezeEnemy()
    {
        if (enemyRB2D.constraints == RigidbodyConstraints2D.FreezeAll)
        {
            enemyRB2D.constraints = RigidbodyConstraints2D.None;
            enemyRB2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    #endregion Funciones Basicas
}
