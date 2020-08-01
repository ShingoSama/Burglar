using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="new Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Items
{
    public EquipmentType equipmentSlot;
    public float MaxHealth;
    public float Damage;
    public float CriticalChance;
    public float CriticalDamage;
    public float Defense;
    public float MovementSpeed;
    public float AttackSpeed;
    public float Luck;
    [Space]
    public float MaxHealthPercent;
    public float DamagePercent;
    public float CriticalChancePercent;
    public float CriticalDamagePercent;
    public float DefensePercent;
    public float MovementSpeedPercent;
    public float AttackSpeedPercent;
    public float LuckPercent;
    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }
    public void Equip(PlayerStats playerStats)
    {
        if(MaxHealth != 0)
        {
            playerStats.MaxHealth.AddModifier(new StatsModifier(MaxHealth, StatsModType.Flat, this));
        }
        if (Damage != 0)
        {
            playerStats.Damage.AddModifier(new StatsModifier(Damage, StatsModType.Flat, this));
        }
        if (CriticalChance != 0)
        {
            playerStats.CriticalChance.AddModifier(new StatsModifier(CriticalChance, StatsModType.Flat, this));
        }
        if (CriticalDamage != 0)
        {
            playerStats.CriticalDamage.AddModifier(new StatsModifier(CriticalDamage, StatsModType.Flat, this));
        }
        if (Defense != 0)
        {
            playerStats.Defense.AddModifier(new StatsModifier(Defense, StatsModType.Flat, this));
        }
        if (MovementSpeed != 0)
        {
            playerStats.MovementSpeed.AddModifier(new StatsModifier(MovementSpeed, StatsModType.Flat, this));
        }
        if (AttackSpeed != 0)
        {
            playerStats.AttackSpeed.AddModifier(new StatsModifier(AttackSpeed, StatsModType.Flat, this));
        }
        if (Luck != 0)
        {
            playerStats.Luck.AddModifier(new StatsModifier(Luck, StatsModType.Flat, this));
        }
        if (MaxHealthPercent != 0)
        {
            playerStats.MaxHealth.AddModifier(new StatsModifier(MaxHealthPercent, StatsModType.PercentMult, this));
        }
        if (DamagePercent != 0)
        {
            playerStats.Damage.AddModifier(new StatsModifier(DamagePercent, StatsModType.PercentMult, this));
        }
        if (CriticalChancePercent != 0)
        {
            playerStats.CriticalChance.AddModifier(new StatsModifier(CriticalChancePercent, StatsModType.PercentMult, this));
        }
        if (CriticalDamagePercent != 0)
        {
            playerStats.CriticalDamage.AddModifier(new StatsModifier(CriticalDamagePercent, StatsModType.PercentMult, this));
        }
        if (DefensePercent != 0)
        {
            playerStats.Defense.AddModifier(new StatsModifier(DefensePercent, StatsModType.PercentMult, this));
        }
        if (MovementSpeedPercent != 0)
        {
            playerStats.MovementSpeed.AddModifier(new StatsModifier(MovementSpeedPercent, StatsModType.PercentMult, this));
        }
        if (AttackSpeedPercent != 0)
        {
            playerStats.AttackSpeed.AddModifier(new StatsModifier(AttackSpeedPercent, StatsModType.PercentMult, this));
        }
        if (LuckPercent != 0)
        {
            playerStats.Luck.AddModifier(new StatsModifier(LuckPercent, StatsModType.PercentMult, this));
        }
    }
    public void UnEquip(PlayerStats playerStats)
    {
        playerStats.MaxHealth.RemoveAllModifierFromSource(this);
        playerStats.Damage.RemoveAllModifierFromSource(this);
        playerStats.CriticalChance.RemoveAllModifierFromSource(this);
        playerStats.CriticalDamage.RemoveAllModifierFromSource(this);
        playerStats.Defense.RemoveAllModifierFromSource(this);
        playerStats.MovementSpeed.RemoveAllModifierFromSource(this);
        playerStats.AttackSpeed.RemoveAllModifierFromSource(this);
        playerStats.Luck.RemoveAllModifierFromSource(this);
}
}
public enum EquipmentType
{
    Head,
    Chest,
    Legs,
    Feet,
    RHand,
    LHand
}
