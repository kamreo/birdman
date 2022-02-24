using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ItemGrid))]
public class GridInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField]
	private InventoryController inventoryController;
	private ItemGrid itemGrid;

	public void OnPointerEnter(PointerEventData eventData)
	{
		inventoryController.SelectedItemGrid = itemGrid;
		inventoryController.LastUsedItemGrid = itemGrid;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		inventoryController.SelectedItemGrid = null;
	}

	private void Awake()
	{
		if (inventoryController == null)
			inventoryController = FindObjectOfType<InventoryController>();

		itemGrid = GetComponent<ItemGrid>();
	}
}
