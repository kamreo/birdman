public interface ICombatStats
{
	public Stat Armor { get; set; }
	public Stat MagicDefense { get; set; }
	public Stat Evasion { get; set; }

	public Stat MinDamage { get; set; }
	public Stat MaxDamage { get; set; }
	public Stat AttackSpeed { get; set; }
	public Stat AttackRange { get; set; }
	public Stat CritChance { get; set; }
	public Stat CritMultiplier { get; set; }

	public Stat BlockChance { get; set; }
	public Stat BlockDamage { get; set; }
}