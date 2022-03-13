using System.Reflection;
using UnityEngine;

public class PlayerCombatStats : MonoBehaviour, ICombatStats, ICombatMethods
{
    AttributesHudHandler attributesHudHandler;

    private void Start()
    {
        attributesHudHandler = transform.parent.GetComponentInChildren<AttributesHudHandler>();

        attributesHudHandler.ChangeAttributeValueText("MinDamage", minDamage.CalculatedValue);
        attributesHudHandler.ChangeAttributeValueText("MaxDamage", maxDamage.CalculatedValue);
        attributesHudHandler.ChangeAttributeValueText("Armor", armor.CalculatedValue);
        attributesHudHandler.ChangeAttributeValueText("MagicDefense", magicDefense.CalculatedValue);
        attributesHudHandler.ChangeAttributeValueText("Evasion", evasion.CalculatedValue);
        attributesHudHandler.ChangeAttributeValueText("SpellAmplification", spellAmplification.CalculatedValue);
        attributesHudHandler.ChangeAttributeValueText("AttackSpeed", attackSpeed.CalculatedValue);
        attributesHudHandler.ChangeAttributeValueText("CritChance", critChance.CalculatedValue);
        attributesHudHandler.ChangeAttributeValueText("CritMultiplier", critMultiplier.CalculatedValue);
    }

    [Header("Attack")]
    [SerializeField]
    private Stat minDamage;
    public Stat MinDamage
    {
        get => minDamage;
        set
        {
            minDamage = value;

            var propName = MethodBase.GetCurrentMethod().Name.Substring(4);
            attributesHudHandler.ChangeAttributeValueText(propName, minDamage.CalculatedValue);
        }
    }

    [SerializeField]
    private Stat maxDamage;
    public Stat MaxDamage
    {
        get => maxDamage;
        set
        {
            maxDamage = value;

            var propName = MethodBase.GetCurrentMethod().Name.Substring(4);
            attributesHudHandler.ChangeAttributeValueText(propName, maxDamage.CalculatedValue);
        }
    }

    [SerializeField]
    private Stat additiveDamage;
    public Stat AdditiveDamage
    {
        get => additiveDamage;
        set => additiveDamage = value;
    }

    [SerializeField]
    private Stat attackSpeed;
    public Stat AttackSpeed
    {
        get => attackSpeed;
        set
        {
            attackSpeed = value;
            var propName = MethodBase.GetCurrentMethod().Name.Substring(4);
            attributesHudHandler.ChangeAttributeValueText(propName, attackSpeed.CalculatedValue);
        }
    }

    [SerializeField]
    private Stat critChance;
    public Stat CritChance
    {
        get => critChance;
        set
        {
            critChance = value;
            var propName = MethodBase.GetCurrentMethod().Name.Substring(4);
            attributesHudHandler.ChangeAttributeValueText(propName, critChance.CalculatedValue);
        }
    }

    [SerializeField]
    private Stat critMultiplier;
    public Stat CritMultiplier
    {
        get => critMultiplier;
        set
        {
            critMultiplier = value;
            var propName = MethodBase.GetCurrentMethod().Name.Substring(4);
            attributesHudHandler.ChangeAttributeValueText(propName, critChance.CalculatedValue);
        }
    }

    [Header("Magic")]
    [SerializeField]
    private Stat spellAmplification;
    public Stat SpellAmplification
    {
        get => spellAmplification;
        set
        {
            spellAmplification = value;
            var propName = MethodBase.GetCurrentMethod().Name.Substring(4);
            attributesHudHandler.ChangeAttributeValueText(propName, spellAmplification.CalculatedValue);
        }
    }

    [Header("Defense")]
    [SerializeField]
    private Stat armor;
    public Stat Armor
    {
        get => armor;
        set
        {
            armor = value;
            var propName = MethodBase.GetCurrentMethod().Name.Substring(4);
            attributesHudHandler.ChangeAttributeValueText(propName, armor.CalculatedValue);
        }
    }

    [SerializeField]
    private Stat magicDefense;
    public Stat MagicDefense
    {
        get => magicDefense;
        set
        {
            magicDefense = value;
            var propName = MethodBase.GetCurrentMethod().Name.Substring(4);
            attributesHudHandler.ChangeAttributeValueText(propName, magicDefense.CalculatedValue);
        }
    }

    [SerializeField]
    private Stat evasion;
    public Stat Evasion
    {
        get => evasion;
        set
        {
            evasion = value;
            var propName = MethodBase.GetCurrentMethod().Name.Substring(4);
            attributesHudHandler.ChangeAttributeValueText(propName, evasion.CalculatedValue);
        }
    }

    [Header("Blocking")]
    [SerializeField]
    private Stat blockChance;
    public Stat BlockChance { get => blockChance; set => blockChance = value; }

    [SerializeField]
    private Stat blockDamage;
    public Stat BlockDamage { get => blockDamage; set => blockDamage = value; }
    public Stat AttackRange { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void TakeDamage(float damageTaken)
    {
        Debug.Log($"Taking {damageTaken} damage!");
    }

    public void TakeDamage(float minVal, float maxVal)
    {

    }

    public void TakeDamage(float damageTaken, string damageType)
    {
        throw new System.NotImplementedException();
    }
}
