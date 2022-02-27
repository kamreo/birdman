using UnityEngine;

public class PlayerCombatStats : MonoBehaviour, ICombatStats, ICombatMethods
{
	[SerializeField]
	private Stat armor;
	public Stat Armor { get => armor; set => armor = value; }

	[SerializeField]
	private Stat magicDefense;
	public Stat MagicDefense { get => magicDefense; set => magicDefense = value; }

	[SerializeField]
	private Stat evasion;
	public Stat Evasion { get => evasion; set => evasion = value; }

	[SerializeField]
	private Stat minDamage;
	public Stat MinDamage
	{
		get => minDamage;
		set
		{
			if (value.CalculatedValue >= MaxDamage.CalculatedValue)
			{
				minDamage.CalculatedValue = MaxDamage.CalculatedValue;
			}
			else
			{
				minDamage = value;
			}
		}
	}

	[SerializeField]
	private Stat maxDamage;
	public Stat MaxDamage
	{
		get => maxDamage;
		set
		{
			if (value.CalculatedValue <= MinDamage.CalculatedValue)
			{
				maxDamage.CalculatedValue = MinDamage.CalculatedValue;
			}
			else
			{
				maxDamage = value;
			}
		}
	}

	[SerializeField]
	private Stat attackSpeed;
	public Stat AttackSpeed { get => attackSpeed; set => attackSpeed = value; }

	[SerializeField]
	private Stat critChance;
	public Stat CritChance { get => critChance; set => critChance = value; }

	[SerializeField]
	private Stat critMultiplier;
	public Stat CritMultiplier { get => critMultiplier; set => critMultiplier = value; }
	[SerializeField]
	private Stat blockChance;
	public Stat BlockChance { get => blockChance; set => blockChance = value; }

	[SerializeField]
	private Stat blockDamage;
	public Stat BlockDamage { get => blockDamage; set => blockDamage = value; }

	public void TakeDamage(float damageTaken)
	{
		throw new System.NotImplementedException();
	}

	public void TakeDamage(float minVal, float maxVal)
	{

	}

	public void TakeDamage(float damageTaken, string damageType)
	{
		throw new System.NotImplementedException();
	}
}