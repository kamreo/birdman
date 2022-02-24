public interface IPlayerStats
{
	public Stat Dexterity { get; set; }
	public Stat Intelligence { get; set; }
	public Stat Strength { get; set; }

	public void ModifyStatValue(ItemStatSOInt itemStat);
	public void ModifyStatValue(string statName, int value);
}