using UnityEngine;

public class ItemGrid : MonoBehaviour
{
	public static readonly Vector2 tileSize = new Vector2(32, 32);
	[SerializeField]
	protected Vector2Int gridSize = new Vector2Int(9, 4);

	public Vector2Int GridSize { get { return gridSize; } }

	private InventoryGridItem[,] inventoryItemSlot;
	protected Vector2 positionOnTheGrid = new Vector2();
	protected Vector2Int tileGridPosition = new Vector2Int();

	[SerializeField]
	protected RectTransform rectTransform;

	private void Start()
	{
		rectTransform = GetComponent<RectTransform>();
		if (inventoryItemSlot == null)
			InitInvetoryItemSlot(gridSize);
	}

	protected virtual void InitInvetoryItemSlot(Vector2Int gridSize)
	{
		inventoryItemSlot = new InventoryGridItem[gridSize.x, gridSize.y];
		Vector2 size = new Vector2(gridSize.x * tileSize.x, gridSize.y * tileSize.y);
		rectTransform.sizeDelta = size;
	}

	public virtual Vector2Int GetTileGridPosition(Vector2 mousePosition)
	{
		positionOnTheGrid.x = mousePosition.x - rectTransform.position.x;
		positionOnTheGrid.y = rectTransform.position.y - mousePosition.y;

		tileGridPosition.x = (int)(positionOnTheGrid.x / tileSize.x);
		tileGridPosition.y = (int)(positionOnTheGrid.y / tileSize.y);

		return tileGridPosition;
	}
	public virtual InventoryGridItem PickUpItem(Vector2Int positionOnGrid)
	{
		if (positionOnGrid.x < 0 || positionOnGrid.y < 0)
			return null;

		if (positionOnGrid.x >= inventoryItemSlot.GetLength(0) || positionOnGrid.y >= inventoryItemSlot.GetLength(1))
			return null;

		InventoryGridItem returnItem = inventoryItemSlot[positionOnGrid.x, positionOnGrid.y];
		if (returnItem == null)
		{
			return null;
		}

		CleanGridReference(returnItem);

		return returnItem;
	}

	public virtual InventoryGridItem GetItem(Vector2Int positionOnGrid)
	{
		if (positionOnGrid.x < 0 || positionOnGrid.y < 0)
			return null;

		if (positionOnGrid.x >= inventoryItemSlot.GetLength(0) || positionOnGrid.y >= inventoryItemSlot.GetLength(1))
			return null;

		return inventoryItemSlot[positionOnGrid.x, positionOnGrid.y];
	}

	protected virtual void CleanGridReference(InventoryGridItem item)
	{
		if (inventoryItemSlot == null)
			return;

		for (int i = 0; i < item.itemData.size.x; i++)
		{
			for (int j = 0; j < item.itemData.size.y; j++)
			{
				inventoryItemSlot[item.onGridPosition.x + i, item.onGridPosition.y + j] = null;
			}
		}
	}

	public virtual bool PlaceItem(InventoryGridItem item, Vector2Int itemPosition, ref InventoryGridItem overlapItem)
	{
		if (!CheckItemBoundary(itemPosition, item.itemData.size))
		{
			return false;
		}

		if (!CheckOverlap(itemPosition, item.itemData.size, ref overlapItem))
		{
			overlapItem = null;
			return false;
		}

		if (overlapItem != null)
		{
			CleanGridReference(overlapItem);
		}

		PlaceItemInInventorySlot(item, itemPosition);
		return true;
	}

	public virtual void PlaceItemInInventorySlot(InventoryGridItem item, Vector2Int itemPosition)
	{
		RectTransform rectTransform = item.GetComponent<RectTransform>();
		rectTransform.SetParent(this.rectTransform);
		rectTransform.localScale = Vector3.one;

		for (int i = 0; i < item.itemData.size.x; i++)
		{
			for (int j = 0; j < item.itemData.size.y; j++)
			{
				inventoryItemSlot[itemPosition.x + i, itemPosition.y + j] = item;
			}
		}

		item.onGridPosition = itemPosition;
		Vector2 position = CalculatePositionOnGrid(item, itemPosition);

		rectTransform.localPosition = position;
	}

	public virtual Vector2 CalculatePositionOnGrid(InventoryGridItem item, Vector2Int itemPosition)
	{
		Vector2 position = new Vector2();
		position.x = itemPosition.x * tileSize.x + tileSize.x * item.itemData.size.x / 2;
		position.y = -(itemPosition.y * tileSize.y + tileSize.y * item.itemData.size.y / 2);
		return position;
	}

	public virtual Vector2Int? FindSpaceForObject(ItemData itemData)
	{
		int height = gridSize.y - itemData.size.y + 1;
		int width = gridSize.x - itemData.size.x + 1;
		for (int j = 0; j < height; j++)
		{
			for (int i = 0; i < width; i++)
			{
				if (CheckAvailableSpace(new Vector2Int(i, j), itemData.size))
				{
					return new Vector2Int(i, j);
				}
			}
		}

		return null;
	}

	protected virtual bool CheckAvailableSpace(Vector2Int position, Vector2Int size)
	{
		if (inventoryItemSlot == null)
		{
			InitInvetoryItemSlot(gridSize);
		}

		for (int i = 0; i < size.x; i++)
		{
			for (int j = 0; j < size.y; j++)
			{
				if (inventoryItemSlot[position.x + i, position.y + j] != null)
				{
					return false;
				}
			}
		}

		return true;
	}

	protected virtual bool CheckOverlap(Vector2Int position, Vector2Int size, ref InventoryGridItem overlapItem)
	{
		for (int i = 0; i < size.x; i++)
		{
			for (int j = 0; j < size.y; j++)
			{
				if (inventoryItemSlot[position.x + i, position.y + j] != null)
				{
					if (overlapItem == null)
					{
						overlapItem = inventoryItemSlot[position.x + i, position.y + j];
					}
					else
					{
						if (overlapItem != inventoryItemSlot[position.x + i, position.y + j])
						{
							return false;
						}
					}
				}
			}
		}

		return true;
	}

	public virtual bool RemoveItem(InventoryGridItem item)
	{
		if (item.itemData.canBeDropped)
		{
			CleanGridReference(item);
			Destroy(item.gameObject);
			return true;
		}

		return false;
	}

	protected virtual bool CheckPosition(Vector2Int position)
	{
		if (position.x < 0 || position.y < 0)
			return false;

		if (position.x >= gridSize.x || position.y >= gridSize.y)
			return false;

		return true;
	}

	public virtual bool CheckItemBoundary(Vector2Int position, Vector2Int size)
	{
		if (!CheckPosition(position))
			return false;

		position.x += size.x - 1;
		position.y += size.y - 1;


		if (!CheckPosition(position))
			return false;

		return true;
	}
}
