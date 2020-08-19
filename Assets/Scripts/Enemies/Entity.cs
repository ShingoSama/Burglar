using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Entity", menuName = "Entity/Enemy")]
public class Entity : ScriptableObject
{
    public string Name;
    public float MaxHealth;
    public float Damage;
    public float Defense;
    public float AgroRange;
    public float MovementNormalSpeed;
    public float MovementAgroSpeed;
    public float Experience;
}
