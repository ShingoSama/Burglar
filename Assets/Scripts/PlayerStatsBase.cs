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
    public float MaxHealth;
    public float Damage;
    public float CriticalChance;
    public float Defense;
    public float Movement;
    public float JumpForce;
    public float Experience;
    public State state;
}

