using UnityEngine;

public interface IBaseStats
{
	[SerializeField]
	public int Level { get; set; }

	[SerializeField]
	public Stat MaxHealth { get; set; }
	[SerializeField]
	public float CurrentHealth { get; set; }
	[SerializeField]
	public Stat RegenHealth { get; set; }

	[SerializeField]
	public Stat MaxMana { get; set; }
	[SerializeField]
	public float CurrentMana { get; set; }
	[SerializeField]
	public Stat RegenMana { get; set; }
}