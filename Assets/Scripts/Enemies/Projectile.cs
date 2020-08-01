using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform Firepoint;
    private float damage;
    public GameObject arrowPrefab;
    public void Shoot()
    {
        arrowPrefab.transform.localScale = new Vector3((float)GetComponent<Enemy>().GetFacingDirection(), arrowPrefab.transform.localScale.y, arrowPrefab.transform.localScale.z);
        damage = GetComponent<Enemy>().GetDamage();
        arrowPrefab.GetComponentInChildren<Arrow>().SetAttackDamage(damage);
        Instantiate(arrowPrefab, Firepoint.position, Firepoint.rotation);
        
    }
}
