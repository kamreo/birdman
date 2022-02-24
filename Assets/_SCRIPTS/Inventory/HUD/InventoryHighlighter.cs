using UnityEngine;

public class InventoryHighlighter : MonoBehaviour
{
	[SerializeField]
	RectTransform highligther;

	[SerializeField]
	GameObject parent;

	public void SetSize(Vector2Int itemSize)
	{
		Vector2 size = new Vector2();
		size.x = itemSize.x * ItemGrid.tileSize.x;
		size.y = itemSize.y * ItemGrid.tileSize.y;
		highligther.sizeDelta = size;
	}

	public void SetPosition(ItemGrid targetGrid, InventoryGridItem targetItem)
	{
		SetParent(targetGrid);
		Vector2 position = new Vector2();
		if (targetGrid.GetType().Equals(typeof(EquipmentSlot)))
		{
			EquipmentSlot slot = (EquipmentSlot)targetGrid;
			position = slot.CalculatePositionOnGrid();
		}
		else
		{
			position = targetGrid.CalculatePositionOnGrid(targetItem, targetItem.onGridPosition);
		}
		highligther.localPosition = new Vector3(position.x, position.y, -1);
		highligther.SetAsFirstSibling();
	}

	public void SetPosition(ItemGrid targetGrid, InventoryGridItem targetItem, Vector2Int position)
	{
		SetParent(targetGrid);
		Vector2 newPosition = new Vector2();

		if (targetGrid.GetType().Equals(typeof(EquipmentSlot)))
		{
			EquipmentSlot slot = (EquipmentSlot)targetGrid;
			newPosition = slot.CalculatePositionOnGrid();
		}
		else
		{
			newPosition = targetGrid.CalculatePositionOnGrid(targetItem, position);
		}
		highligther.localPosition = new Vector3(newPosition.x, newPosition.y, -1);
		highligther.SetAsFirstSibling();
	}

	public void Show(bool toDisplay)
	{
		highligther.gameObject.SetActive(toDisplay);
	}

	public void SetParent(ItemGrid parent)
	{
		if (parent == null)
			return;
		highligther.SetParent(parent.GetComponent<RectTransform>());
	}
}
