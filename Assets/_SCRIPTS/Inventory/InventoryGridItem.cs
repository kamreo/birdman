using UnityEngine;
using UnityEngine.UI;

public class InventoryGridItem : MonoBehaviour
{
	public ItemData itemData;

	public Vector2Int onGridPosition;

	public void SetItemData(ItemData itemData)
	{
		this.itemData = itemData;

		GetComponent<Image>().sprite = itemData.graphics;

		Vector2 inventorySize = new Vector2();
		inventorySize.x = itemData.size.x * ItemGrid.tileSize.x;
		inventorySize.y = itemData.size.y * ItemGrid.tileSize.y;
		GetComponent<RectTransform>().sizeDelta = inventorySize;
	}
}
