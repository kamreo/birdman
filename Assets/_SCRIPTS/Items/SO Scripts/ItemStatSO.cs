using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Item Stat", menuName = "Inventory/Item Stat")]
public class ItemStatSO : ScriptableObject
{
	[SerializeField]
	private new string name;
	public string Name { get => name; }

	[SerializeField]
	protected int tier = -1;
	public int Tier
	{
		get => tier;
		protected set
		{
			tier = value;

			if (tier < 0)
				tier = 0;

			if (tier > 6)
				tier = 6;
		}
	}

	[SerializeField]
	private ItemStat[] tierValues;

	[SerializeField]
	private int value = -1;
	public int Value { get => value; }

	public ItemStatSO(ItemStatSO itemStatSO)
	{
		name = itemStatSO.Name;
		tier = itemStatSO.Tier;
		tierValues = itemStatSO.tierValues;
		value = itemStatSO.value;
	}

	public void RandValue(int itemLevel)
	{
		Tier = Random.Range(itemLevel / 10 - 3, itemLevel / 10 + 1);
		value = Random.Range(tierValues[tier].MinValue, tierValues[tier].MaxValue);
	}
}

