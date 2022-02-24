using UnityEngine;

[System.Serializable]
public class ItemStatRange : ItemStat
{
	[SerializeField]
	private int minMinimalValue;
	public int MinMinimalValue { get => minMinimalValue; }
	[SerializeField]
	private int maxMininimalValue;
	public int MaxMininalValue { get => maxMininimalValue; }

	[SerializeField]
	private int minMaximalValue;
	public int MinMaximalValue { get => minMaximalValue; }
	[SerializeField]
	private int maxMaximalValue;
	public int MaxMaximalValue { get => maxMaximalValue; }
}