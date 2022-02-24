using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Item Stat", menuName = "Inventory/Item Stat/Range")]
public class ItemStatSORange : ItemStatSO
{
	[SerializeField]
	private ItemStatRange[] rangeTierValues;

	private int minStatValue;
	public int MinStatValue { get => minStatValue; }
	private int maxStatValue;
	public int MaxStatValue { get => maxStatValue; }

	public ItemStatSORange(ItemStatSORange itemStatSO) : base(itemStatSO)
	{
		name = itemStatSO.Name;
		tier = itemStatSO.Tier;
		rangeTierValues = itemStatSO.rangeTierValues;
		minStatValue = itemStatSO.MinStatValue;
		maxStatValue = itemStatSO.MaxStatValue;
	}

	public override void RandValue(int itemLevel)
	{
		Tier = Random.Range(itemLevel / 10 - 3, itemLevel / 10 + 1);

		minStatValue = Random.Range(rangeTierValues[tier].MinMinimalValue, rangeTierValues[tier].MaxMininalValue);
		maxStatValue = Random.Range(rangeTierValues[tier].MinMaximalValue, rangeTierValues[tier].MaxMaximalValue);
	}

	public override (int, int) GetRangeValue()
	{
		return (minStatValue, maxStatValue);
	}
}

