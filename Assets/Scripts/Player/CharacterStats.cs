using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

[Serializable]
public class CharacterStats
{
    public float BaseValue;
    public virtual float Value
    {
        get
        {
            if (isDirty || BaseValue != lastBaseValue)
            {
                lastBaseValue = BaseValue;
                _value = CalculateFinalValue();
                isDirty = false;
            }
            return _value;
        }
    }
    protected bool isDirty = true;
    protected float _value;
    protected float lastBaseValue = float.MinValue;
    private readonly List<StatsModifier> statsModifiers;
    public readonly ReadOnlyCollection<StatsModifier> StatsModifiers;
    public CharacterStats()
    {
        statsModifiers = new List<StatsModifier>();
        StatsModifiers = statsModifiers.AsReadOnly();
    }
    public CharacterStats(float baseValue) : this()
    {
        BaseValue = baseValue;
    }
    public virtual void AddModifier(StatsModifier mod)
    {
        isDirty = true;
        statsModifiers.Add(mod);
        statsModifiers.Sort(CompareModifierOrder);
    }
    protected virtual int CompareModifierOrder(StatsModifier a, StatsModifier b)
    {
        if (a.Order < b.Order)
        {
            return -1;
        }
        else if (a.Order > b.Order)
        {
            return 1;
        }
        return 0;
    }
    public virtual bool RemoveAllModifierFromSource(object source)
    {
        bool didRemove = false;
        for (int i = statsModifiers.Count - 1; i >= 0 ; i--)
        {
            if(statsModifiers[i].Source == source)
            {
                isDirty = true;
                statsModifiers.RemoveAt(i);
                didRemove = true;
            }
        }
        return didRemove; 
    }
    public virtual bool RemoveModifier(StatsModifier mod)
    {
        if(statsModifiers.Remove(mod))
        {
            isDirty = true;
            return isDirty;
        }
        return false; 
    }
    protected virtual float CalculateFinalValue()
    {
        float finalValue = BaseValue;
        float sumPercentAdd = 0;
        for(int i = 0; i < statsModifiers.Count; i++)
        {
            StatsModifier mod = statsModifiers[i];
            if (mod.Type == StatsModType.Flat)
            {
                finalValue += mod.Value;
            }
            else if (mod.Type == StatsModType.PercentAdd)
            {
                sumPercentAdd += mod.Value;
                if(i +1 >= statsModifiers.Count || statsModifiers[i+1].Type != StatsModType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentAdd;
                    sumPercentAdd = 0;
                }
            }
            else if(mod.Type == StatsModType.PercentMult)
            {
                finalValue *= 1 + mod.Value;
            }
        }
        return (float)Math.Round(finalValue, 4);
    }
}
