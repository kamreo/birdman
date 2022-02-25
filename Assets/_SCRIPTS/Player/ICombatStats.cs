using UnityEngine;

public interface ICombatStats
{
	[SerializeField]
	public Stat Armor { get; set; }
	public Stat MagicDefense { get; set; }
	public Stat Evasion { get; set; }

	public Stat MinDamage { get; set; }
	public Stat MaxDamage { get; set; }
	public Stat AttackSpeed { get; set; }
	public Stat AttackRange { get; set; }
	public Stat CritChance { get; set; }
	public Stat CritMultiplier { get; set; }


	public void ModifyStat(ItemStatSO itemStat);
	public void ModifyFloatStat(string statName, float value);
	public void ModifyIntStat(string statName, int value);

	public void ModifyDamageStat(ItemStatSORange itemStat);
	public void ModifyDamageStat(float minDamage, float maxDamage);
}