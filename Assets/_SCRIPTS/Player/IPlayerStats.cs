public interface IPlayerStats
{
	public Stat Dexterity { get; set; }
	public int AttackSpeedPerDexterity { get; }
	public int EvasionPerDexterity { get; }

	public Stat Intelligence { get; set; }
	public int MaxManaPerIntelligence { get; }
	public int MagicalAttackDamagePerIntelligence { get; }

	public Stat Strength { get; set; }
	public int MaxHealthPerStrength { get; }
	public int PhysicalAttackDamagePerStrength { get; }
}