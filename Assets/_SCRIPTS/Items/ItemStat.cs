using UnityEngine;

[System.Serializable]
public class ItemStat
{
	[SerializeField]
	private int minValue;
	public int MinValue { get => minValue; }
	[SerializeField]
	private int maxValue;
	public int MaxValue { get => maxValue; }
}
