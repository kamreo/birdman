using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Item Stat", menuName = "Inventory/Item Stat/Float")]
public class ItemStatSOFloat : ItemStatSO
{
	[SerializeField]
	private ItemStatFloat[] floatTierValues;

	protected float statValue;

	public ItemStatSOFloat(ItemStatSOFloat itemStatSO) : base(itemStatSO)
	{
		name = itemStatSO.Name;
		tier = itemStatSO.Tier;
		floatTierValues = itemStatSO.floatTierValues;
		statValue = itemStatSO.statValue;
	}

	public override void RandValue(int itemLevel)
	{
		Tier = Random.Range(itemLevel / 10 - 3, itemLevel / 10 + 1);

		statValue = Random.Range(floatTierValues[tier].MinValue, floatTierValues[tier].MaxValue);
		statValue = Mathf.Round(statValue * 10f) / 10f;
	}

	public override float GetFloatValue()
	{
		return statValue;
	}
}

