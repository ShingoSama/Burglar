using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public string Name;
    public int Level;
    public int Experience;
    private float currentHealth { get; set; }
    public CharacterStats MaxHealth;
    public CharacterStats Damage;
    public CharacterStats CriticalChance;
    public CharacterStats CriticalDamage;
    public CharacterStats Defense;
    public CharacterStats MovementSpeed;
    public CharacterStats AttackSpeed;
    public CharacterStats Luck;
    public int JumpForce;

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    //public PlayerStats()
    //{
    //    Name = "New Name";
    //    Level = 1;
    //    Experience = 0;
    //    MaxHealth.BaseValue = 40f;
    //    currentHealth = MaxHealth.BaseValue;
    //}
}
