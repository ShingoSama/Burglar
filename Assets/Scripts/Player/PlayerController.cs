using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameOverMenu gameOverMenu;
    public MainGameMenu mainGameMenu;
    public InventoryUI inventoryUI;
    public HealthBarController healthBarController;
    public PlayerStatsBase playerStats;

    public Interactable focus;

    float currentHealth;
    //Movilidad del jugador

    public float speedPlayer;
    public float jumpforcePlayer;
    public float groundCheckRadius;
    public Transform groundCheckPlayer;
    public LayerMask groundLayer;

    // Ataque del jugador

    public Transform attackPointUnEquipPlayer;
    public float attackRangeUnEquipPlayer;
    public LayerMask targetLayer;
    public Vector2 movementDamage;
    private Animator _animatorPlayer;
    private Rigidbody2D _rigidbody2DPlayer;
    private Vector2 _movementPlayer;




    private float longIdleTimePlayer = 6f;
    private float _longIdleTimer;

    private bool isTakeDamage;
    private bool isDead;
    private bool inLadder;
    private bool inLadderMoving;
    private bool facingRight = true;
    private bool isGrounded;
    private bool isAttacking;
    private bool isInMenuUI;
    private bool isInInventaryUI;

    private float MaxHealth;

    void Start()
    {

    }
    void Awake()
    {
        mainGameMenu.PlayerDead(isDead);
        MaxHealth = playerStats.MaxHealth;
        playerStats.state = PlayerStatsBase.State.Idle;
        currentHealth = MaxHealth;
        healthBarController.SetMaxHealth((float)MaxHealth);
        _animatorPlayer = GetComponent<Animator>();
        _rigidbody2DPlayer = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (!isDead)
        {
            isInMenuUI = mainGameMenu.GetMenuUIActive();
            isInInventaryUI = inventoryUI.GetInventoryUIActive();
            if (!isTakeDamage)
            {
                isGrounded = Physics2D.OverlapCircle(groundCheckPlayer.position, groundCheckRadius, groundLayer);
                //Movimiento
                if (!isAttacking && !isInMenuUI && !isInInventaryUI)
                {
                    float horizontalInput = Input.GetAxisRaw("Horizontal");
                    float verticalInput = Input.GetAxisRaw("Vertical");
                    if (inLadder && verticalInput != 0)
                    {
                        ClimbLadderMove(verticalInput);
                    }
                    if (inLadder && !isGrounded)
                    {
                        _movementPlayer = Vector2.zero;
                    }
                    else
                    {
                        _movementPlayer = new Vector2(horizontalInput, 0f);
                    }

                    if (horizontalInput < 0f && facingRight == true)
                    {
                        FlipPlayer();
                    }
                    else if (horizontalInput > 0f && facingRight == false)
                    {
                        FlipPlayer();
                    }
                    //Salto
                    
                    if (Input.GetButtonDown("Jump") && Input.GetAxisRaw("Vertical") == 0f && isGrounded == true && isAttacking == false)
                    {
                        _rigidbody2DPlayer.AddForce(Vector2.up * jumpforcePlayer, ForceMode2D.Impulse);
                    }
                    //Ataque
                    if (Input.GetButtonDown("Fire1") && isGrounded == true && isAttacking == false)
                    {
                        PlayerUnEquipAttack();
                    }
                    //Interactuar
                    if (Input.GetButtonDown("Interact") && isGrounded == true && isAttacking == false)
                    {
                        InteractWithEnviroment();
                    }
                }
            }
        }
    }
    void FixedUpdate()
    {
        if ((!isAttacking && !inLadder)||(inLadder && isGrounded))
        {
            float horizontalVelocity = _movementPlayer.normalized.x * speedPlayer;
            _rigidbody2DPlayer.velocity = new Vector2(horizontalVelocity, _rigidbody2DPlayer.velocity.y);
        }
    }
    private void LateUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPlayer.position, groundCheckRadius, groundLayer);
        if (inLadder && !isGrounded)
        {
            _animatorPlayer.SetBool("Idle", false);
        }
        else
        {
            _animatorPlayer.SetBool("Idle", _movementPlayer == Vector2.zero);
        }
        _animatorPlayer.SetBool("IsGrounded", isGrounded);
        _animatorPlayer.SetFloat("VerticalVelocity", _rigidbody2DPlayer.velocity.y);
        //Animacion de ataque
        if (_animatorPlayer.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
        if (_animatorPlayer.GetCurrentAnimatorStateInfo(0).IsTag("TakeDamage"))
        {
            isTakeDamage = true;
        }
        else
        {
            isTakeDamage = false;
        }
        //LongIdle
        if (_animatorPlayer.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
        {
            _longIdleTimer += Time.deltaTime;
            if (_longIdleTimer >= longIdleTimePlayer)
            {
                _animatorPlayer.SetTrigger("LongIdle");
            }
        }
        else
        {
            _longIdleTimer = 0f;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {

        }

        //if (Time.time >= nextAttackTime)
        //{
        //    if (isAttacking == false && collision.CompareTag("Player") && isTakeDamage == false)
        //    {
        //        DontMove();
        //        enemyAnimator.SetTrigger("IsAttacking");
        //        nextAttackTime = Time.time + attackRate;
        //    }
        //}
    }
    private void InteractWithEnviroment()
    {
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attackPointUnEquipPlayer.position, attackRangeUnEquipPlayer);
        foreach (Collider2D targets in hitTargets)
        {
            if (targets.GetComponent<Interactable>() != null)
            {
                Interactable interactable = targets.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }

        }
    }
    void SetFocus(Interactable newfocus)
    {
        focus = newfocus;
        newfocus.OnFocus(transform);
    }
    private void PlayerUnEquipAttack()
    {
        _movementPlayer = Vector2.zero;
        _rigidbody2DPlayer.velocity = Vector2.zero;
        _animatorPlayer.SetTrigger("Attacking");

        float damage = playerStats.Damage;
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attackPointUnEquipPlayer.position, attackRangeUnEquipPlayer, targetLayer);

        foreach (Collider2D targets in hitTargets)
        {
            if (targets.GetComponent<Enemy>() != null)
            {
                targets.GetComponent<Enemy>().TakeDamage(damage, transform.position.x);
            }
            if (targets.GetComponent<BreakableEnviroment>() != null)
            {
                targets.GetComponent<BreakableEnviroment>().TakeDamage(damage, transform.position.x);
            }

        }
    }
    private void FlipPlayer()
    {
        facingRight = !facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    public void DamageKnockBack()
    {
        _rigidbody2DPlayer.velocity = new Vector2(movementDamage.x, movementDamage.y); ;
        _animatorPlayer.SetTrigger("Hurt");
    }
    public void TakeDamage(float damage, float positionDamage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
            healthBarController.SetHealth(currentHealth);
            if (positionDamage < gameObject.GetComponent<Transform>().position.x && facingRight == true)
            {
                if (movementDamage.x < 0)
                {
                    movementDamage.x *= -1;
                }
                FlipPlayer();
            }
            if (positionDamage > gameObject.GetComponent<Transform>().position.x && facingRight == false)
            {
                if (movementDamage.x > 0)
                {
                    movementDamage.x *= -1;
                }
                FlipPlayer();
            }
            isTakeDamage = true;
            DamageKnockBack();
            if (currentHealth <= 0)
            {
                PlayerDie();
            }
        }
    }
    public void ClimbLadder()
    {
        inLadder = true;
        _animatorPlayer.SetBool("InLadder", inLadder);
    }
    public void ClimbLadderMove(float direccion)
    {
        inLadder = true;
        _animatorPlayer.SetBool("InLadder", inLadder);
        if (direccion == 0f)
        {
            inLadderMoving = false;
        }
        else
        {
            inLadderMoving = true;
        }
        _animatorPlayer.SetBool("InLadderMoving", inLadderMoving);
        _rigidbody2DPlayer.velocity = new Vector2(_rigidbody2DPlayer.velocity.x, direccion * speedPlayer);
        _rigidbody2DPlayer.gravityScale = 0;
    }
    public void EndClimbLadder()
    {
        _rigidbody2DPlayer.gravityScale = 3;
        inLadder = false;
        inLadderMoving = false;
        _animatorPlayer.SetBool("InLadderMoving", inLadderMoving);
        _animatorPlayer.SetBool("InLadder", inLadder);
    }
    public bool GetInLadder()
    {
        return inLadder;
    }
    public void PlayerDie()
    {
        _animatorPlayer.SetTrigger("IsDead");
        StartCoroutine(DieAnimation());
        isDead = true;
    }
    IEnumerator DieAnimation()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        gameOverMenu.EndGame();
        mainGameMenu.PlayerDead(isDead);
    }
    public bool PlayerIsDead()
    {
        return isDead;
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPointUnEquipPlayer == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPointUnEquipPlayer.position, attackRangeUnEquipPlayer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheckPlayer.position, groundCheckRadius);
        Gizmos.DrawLine(groundCheckPlayer.position, new Vector2(groundCheckPlayer.position.x, groundCheckPlayer.position.y - groundCheckRadius));
    }
}
