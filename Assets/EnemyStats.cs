using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour, IBaseStats, ICombatStats, ICombatMethods
{
	private const int fixedUpdateRate = 50;         // Value needed to correctly apply regeneration

    public Stat Armor
    {
        get => throw new System.NotImplementedException();
        set => throw new System.NotImplementedException();
    }
    public Stat MagicDefense
    {
        get => throw new System.NotImplementedException();
        set => throw new System.NotImplementedException();
    }
    public Stat Evasion
    {
        get => throw new System.NotImplementedException();
        set => throw new System.NotImplementedException();
    }
    public Stat MinDamage
    {
        get => throw new System.NotImplementedException();
        set => throw new System.NotImplementedException();
    }
    public Stat MaxDamage
    {
        get => throw new System.NotImplementedException();
        set => throw new System.NotImplementedException();
    }
    public Stat AttackSpeed
    {
        get => throw new System.NotImplementedException();
        set => throw new System.NotImplementedException();
    }
    public Stat CritChance
    {
        get => throw new System.NotImplementedException();
        set => throw new System.NotImplementedException();
    }
    public Stat CritMultiplier
    {
        get => throw new System.NotImplementedException();
        set => throw new System.NotImplementedException();
    }
    public Stat Health
    {
        get => throw new System.NotImplementedException();
        set => throw new System.NotImplementedException();
    }
    public float CurrentHealth
    {
        get => throw new System.NotImplementedException();
        set => throw new System.NotImplementedException();
    }
    public Stat RegenHealth
    {
        get => throw new System.NotImplementedException();
        set => throw new System.NotImplementedException();
    }
    public Stat Mana
    {
        get => throw new System.NotImplementedException();
        set => throw new System.NotImplementedException();
    }
    public float CurrentMana
    {
        get => throw new System.NotImplementedException();
        set => throw new System.NotImplementedException();
    }
    public Stat RegenMana
    {
        get => throw new System.NotImplementedException();
        set => throw new System.NotImplementedException();
    }
    public float Speed
    {
        get => throw new System.NotImplementedException();
        set => throw new System.NotImplementedException();
    }
    public float SprintMultiplier
    {
        get => throw new System.NotImplementedException();
        set => throw new System.NotImplementedException();
    }

    public void ModifyDamageStat(ItemStatSORange itemStat)
    {
        throw new System.NotImplementedException();
    }

    public void ModifyDamageStat(float minDamage, float maxDamage)
    {
        throw new System.NotImplementedException();
    }

    public void ModifyFloatStat(string statName, float value)
    {
        throw new System.NotImplementedException();
    }

    public void ModifyIntStat(string statName, int value)
    {
        throw new System.NotImplementedException();
    }

    public void ModifyStat(ItemStatSO itemStat)
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float damageTaken)
    {
        Debug.Log("Damage taken");
    }

    public void TakeDamage(float damageTaken, string damageType)
    {
        throw new System.NotImplementedException();
    }
}