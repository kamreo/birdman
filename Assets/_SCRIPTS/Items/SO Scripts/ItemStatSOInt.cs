using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Item Stat", menuName = "Inventory/Item Stat/Integer")]
public class ItemStatSOInt : ItemStatSO
{
	[SerializeField]
	private ItemStatInt[] intTierValues;

	[SerializeField]
	private int statValue = -1;
	public int StatValue { get => statValue; }

	public ItemStatSOInt(ItemStatSOInt itemStatSO) : base(itemStatSO)
	{
		name = itemStatSO.Name;
		tier = itemStatSO.Tier;
		intTierValues = itemStatSO.intTierValues;
		statValue = itemStatSO.statValue;
	}

	public override void RandValue(int itemLevel)
	{
		Tier = Random.Range(itemLevel / 10 - 3, itemLevel / 10 + 1);
		statValue = Random.Range(intTierValues[tier].MinValue, intTierValues[tier].MaxValue);
	}

	public override int GetIntValue()
	{
		return statValue;
	}
}

