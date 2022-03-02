using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private ItemGrid selectedItemGrid;
    public ItemGrid SelectedItemGrid
    {
        get => selectedItemGrid;
        set
        {
            selectedItemGrid = value;
            inventoryHighlighter.SetParent(value);
        }
    }

    [SerializeField]
    private ItemGrid playerItemGrid;
    public ItemGrid PlayerItemGrid
    {
        get => playerItemGrid;
    }


    private ItemGrid lastUsedItemGrid;
    public ItemGrid LastUsedItemGrid
    {
        get => lastUsedItemGrid;
        set
        {
            if (value != null)
            {
                lastUsedItemGrid = value;
                // TEST
                inventoryHighlighter.SetParent(value);
            }
        }
    }
    private InventoryGridItem selectedItem;
    private InventoryGridItem overlapItem;

    RectTransform rectTransform;

    [SerializeField]
    List<ItemData> items;
    [SerializeField]
    GameObject itemPrefab;
    [SerializeField]
    Transform canvasTransform;

    [SerializeField]
    private GameObject onDropPrefabItem;
    [SerializeField]
    private Transform dropParent;

    InventoryHighlighter inventoryHighlighter;
    InventoryItemDescription inventoryItemDescription;

    private void Awake()
    {
        inventoryHighlighter = GetComponent<InventoryHighlighter>();
        inventoryItemDescription = GetComponent<InventoryItemDescription>();
    }

    private void Update()
    {
        ItemIconDrag();


        if (Input.GetKeyDown(KeyCode.Q))
        {
            CreateRandomItem();
        }


        if (Input.GetKeyDown(KeyCode.V))
        {
            InsertRandomItem();
        }

        // Throw away
        if (selectedItemGrid == null)
        {
            inventoryItemDescription.Show(false);
            inventoryHighlighter.Show(false);

            if (selectedItem == null)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                DropItemFromInventory(selectedItem.itemData);
            }
            inventoryHighlighter.Show(false);
            return;
        }

        HandleHighlight();

        if (Input.GetMouseButtonDown(0))
        {
            PressedLeftButton();
        }
    }

    public bool IsHoldingItem()
    {
        if (selectedItem == null)
            return false;

        return true;
    }

    private void InsertRandomItem()
    {
        CreateRandomItem();
        InventoryGridItem item = selectedItem;
        selectedItem = null;
        InsertItemToInventory(item);
    }

    Vector2Int oldPosition;
    InventoryGridItem itemToHighlight;
    private void HandleHighlight()
    {
        Vector2Int positionOnGrid = GetTileGridPosition();
        System.Type selectedGridType = selectedItemGrid.GetType();

        if (oldPosition == positionOnGrid)
        {
            if (!selectedGridType.Equals(typeof(EquipmentSlot)))
            {
                return;
            }
        }

        oldPosition = positionOnGrid;

        if (selectedItem != null)
        {
            bool canShow = selectedItemGrid.CheckItemBoundary(positionOnGrid, selectedItem.itemData.size);
            inventoryHighlighter.Show(canShow);
            inventoryItemDescription.Show(false);

            if (selectedGridType.Equals(typeof(EquipmentSlot)))
            {
                inventoryHighlighter.SetSize(selectedItemGrid.GridSize);

                if (!itemToHighlight.itemData.GetType().Equals(typeof(ItemDataEquipable)))
                {
                    inventoryHighlighter.Show(false);
                    return;
                }

                if (!(selectedItemGrid as EquipmentSlot).AvailableItemTypes.Contains((itemToHighlight.itemData as ItemDataEquipable).SlotType))
                {
                    inventoryHighlighter.Show(false);
                }
            }
            else
            {
                inventoryHighlighter.SetSize(selectedItem.itemData.size);
            }

            inventoryHighlighter.SetPosition(selectedItemGrid, selectedItem, positionOnGrid);
            return;
        }

        itemToHighlight = selectedItemGrid.GetItem(positionOnGrid);

        if (itemToHighlight == null)
        {
            inventoryHighlighter.Show(false);
            inventoryItemDescription.Show(false);
            return;
        }

        inventoryHighlighter.Show(true);
        inventoryItemDescription.Show(true);
        inventoryHighlighter.SetPosition(selectedItemGrid, itemToHighlight);
        inventoryItemDescription.SetDescription(itemToHighlight.itemData);
        inventoryItemDescription.SetPosition(itemToHighlight);


        if (!selectedGridType.Equals(typeof(EquipmentSlot)))
        {
            inventoryHighlighter.SetSize(itemToHighlight.itemData.size);
            return;
        }


        if (!itemToHighlight.itemData.GetType().Equals(typeof(ItemDataEquipable)))
        {
            inventoryHighlighter.Show(false);
            return;
        }


        if ((selectedItemGrid as EquipmentSlot).AvailableItemTypes.Contains((itemToHighlight.itemData as ItemDataEquipable).SlotType))
        {
            inventoryHighlighter.SetSize(selectedItemGrid.GridSize);
        }
        else
        {
            inventoryHighlighter.Show(false);
        }
    }

    private void CreateRandomItem()
    {
        InventoryGridItem inventoryGridItem = Instantiate(itemPrefab).GetComponent<InventoryGridItem>();
        selectedItem = inventoryGridItem;

        rectTransform = inventoryGridItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);

        int selectedItemId = Random.Range(0, items.Count);
        inventoryGridItem.SetItemData(items[selectedItemId]);
    }

    public void CreateItemOnItemFrameClicked(ItemData itemData, Transform parent)
    {
        InventoryGridItem inventoryGridItem = Instantiate(itemPrefab).GetComponent<InventoryGridItem>();
        selectedItem = inventoryGridItem;

        rectTransform = inventoryGridItem.GetComponent<RectTransform>();
        rectTransform.SetParent(parent);

        inventoryGridItem.SetItemData(itemData);
    }

    private void DropItemFromInventory(ItemData itemData)
    {
        if (lastUsedItemGrid == null)
            return;

        bool dropped = lastUsedItemGrid.RemoveItem(selectedItem);
        if (dropped)
        {
            rectTransform = null;
            Transform player = FindObjectOfType<PlayerStats>().transform;
            Pickable droppedItem = Instantiate(onDropPrefabItem, player.position, Quaternion.identity, dropParent).GetComponent<Pickable>();
            droppedItem.SetItemData(itemData);
        }
    }

    public void InsertItemToInventory(InventoryGridItem item)
    {
        if (selectedItemGrid == null)
            selectedItemGrid = playerItemGrid;

        Vector2Int? positionOnGrid = selectedItemGrid.FindSpaceForObject(item.itemData);


        if (positionOnGrid == null)
            return;

        selectedItemGrid.PlaceItemInInventorySlot(item, (Vector2Int)positionOnGrid);
    }

    public bool PickItemToPlayerInventory(InventoryGridItem item)
    {
        Vector2Int? positionOnGrid = playerItemGrid.FindSpaceForObject(item.itemData);


        if (positionOnGrid == null)
            return false;

        playerItemGrid.PlaceItemInInventorySlot(item, (Vector2Int)positionOnGrid);
        return true;
    }

    public bool FindSpaceForObjectInPlayerInventory(ItemData itemData)
    {
        Vector2Int? positionOnGrid = playerItemGrid.FindSpaceForObject(itemData);

        if (positionOnGrid == null)
            return false;

        return true;
    }


    private void PlaceItemInInventory(Vector2Int positionOnGrid)
    {
        bool placed = selectedItemGrid.PlaceItem(selectedItem, positionOnGrid, ref overlapItem);
        if (placed)
        {
            selectedItem = null;
            if (overlapItem != null)
            {
                selectedItem = overlapItem;
                overlapItem = null;
                rectTransform = selectedItem.GetComponent<RectTransform>();
            }
        }
    }

    private void PickItemFromInventory(Vector2Int positionOnGrid)
    {
        selectedItem = selectedItemGrid.PickUpItem(positionOnGrid);
        if (selectedItem != null)
        {
            rectTransform = selectedItem.GetComponent<RectTransform>();
        }

        if (rectTransform != null)
            rectTransform.SetAsLastSibling();

        lastUsedItemGrid = selectedItemGrid;
    }

    private void PressedLeftButton()
    {
        Vector2Int tileGridPosition = GetTileGridPosition();

        if (selectedItem != null)
        {
            PlaceItemInInventory(tileGridPosition);
        }
        else
        {
            PickItemFromInventory(tileGridPosition);
        }

    }

    private Vector2Int GetTileGridPosition()
    {
        Vector2 position = Input.mousePosition;

        if (selectedItem != null)
        {
            position.x -= (selectedItem.itemData.size.x - 1) * ItemGrid.tileSize.x / 2;
            position.y += (selectedItem.itemData.size.y - 1) * ItemGrid.tileSize.y / 2;
        }

        return selectedItemGrid.GetTileGridPosition(position);
    }

    private void ItemIconDrag()
    {
        if (selectedItem == null)
            return;

        if (rectTransform == null)
            return;

        rectTransform.position = Input.mousePosition;
    }
}
