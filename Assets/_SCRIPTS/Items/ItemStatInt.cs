using UnityEngine;

[System.Serializable]
public class ItemStatInt : ItemStat
{
	[SerializeField]
	private int minValue;
	public int MinValue { get => minValue; }
	[SerializeField]
	private int maxValue;
	public int MaxValue { get => maxValue; }
}
