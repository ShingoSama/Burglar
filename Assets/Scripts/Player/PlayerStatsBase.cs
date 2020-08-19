using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Player", menuName = "Entity/Player")]
public class PlayerStatsBase : ScriptableObject
{
    public enum State
    {
        Idle,
        Moving,
        TakeDamage,
        Attaking,
        ClimbLadder,
        Dead
    }

    public string Name;
    public int Level;
    public float Experience;
    public float MaxHealth;
    public float Damage;
    public float CriticalChance;
    public float CriticalDamage;
    public float Defense;
    public float MovementSpeed;
    public float AttackSpeed;
    public float Luck;
    public float JumpForce;
    public State state;
}

