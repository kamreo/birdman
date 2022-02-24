using UnityEngine;

[System.Serializable]
public class ItemStatFloat : ItemStat
{
	[SerializeField]
	private float minValue;
	public float MinValue { get => minValue; }
	[SerializeField]
	private float maxValue;
	public float MaxValue { get => maxValue; }
}
