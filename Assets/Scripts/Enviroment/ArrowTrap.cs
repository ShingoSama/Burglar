using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    private float nextAttackTime { get; set; }
    private float damage;
    private bool inTrap;
    public LayerMask targetLayer;
    public GameObject arrowPrefab;
    public Transform firepoint;
    public Animator animationTrap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inTrap && Time.time > nextAttackTime)
        {
            arrowPrefab.transform.localScale = transform.localScale;
            arrowPrefab.GetComponentInChildren<Arrow>().SetAttackDamage(damage);
            arrowPrefab.GetComponent<Rigidbody2D>().gravityScale = 0;
            Instantiate(arrowPrefab, firepoint.position, firepoint.rotation);
            nextAttackTime = Time.time + 2f;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            float maxhealt = collision.gameObject.GetComponent<PlayerController>().playerStats.MaxHealth;
            damage = (25f * maxhealt) / 100f;
            inTrap = true;
            animationTrap.SetBool("InTrap", inTrap);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inTrap = false;
            animationTrap.SetBool("InTrap", inTrap);
        }
    }
}
