using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item/Usable")]
public class ItemDataUsable : ItemData
{
	[SerializeField]
	private int count = 1;
	public int maxCount = 1;

	public int Count
	{
		get => count;
		set
		{
			if (value <= maxCount)
				count = value;
		}
	}
}
