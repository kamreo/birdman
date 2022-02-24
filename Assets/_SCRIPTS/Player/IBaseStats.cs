public interface IBaseStats
{
	public Stat Health { get; set; }
	public float CurrentHealth { get; set; }
	public Stat RegenHealth { get; set; }

	public Stat Mana { get; set; }
	public float CurrentMana { get; set; }
	public Stat RegenMana { get; set; }

	public float Speed { get; set; }
	public float SprintMultiplier { get; set; }
}