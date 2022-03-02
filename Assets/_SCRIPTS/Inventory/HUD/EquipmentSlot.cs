using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : ItemGrid
{
    InventoryGridItem inventoryItemSlot;

    public InventoryGridItem InventoryItemSlot { get => inventoryItemSlot; }

    bool isWeaponSlot = false;

    [SerializeField]
    List<EquipmentSlotType> availableItemTypes;
    public List<EquipmentSlotType> AvailableItemTypes
    {
        get => availableItemTypes;
        set => availableItemTypes = value;
    }
    List<EquipmentSlotType> originalAvailableItemTypes;

    private EquipmentStatsController equipmentStatsController;
    private PlayerStats playerStats;

    private void Start()
    {
        originalAvailableItemTypes = new List<EquipmentSlotType>(availableItemTypes);
        rectTransform = GetComponent<RectTransform>();
        if (inventoryItemSlot == null)
            InitInvetoryItemSlot(gridSize);

        if (GetComponent<WeaponEquipmentSlot>() != null)
            isWeaponSlot = true;

        equipmentStatsController = FindObjectOfType<EquipmentStatsController>();
        playerStats = FindObjectOfType<PlayerStats>();
    }

    protected override void InitInvetoryItemSlot(Vector2Int gridSize)
    {
        Vector2 size = new Vector2(gridSize.x * tileSize.x, gridSize.y * tileSize.y);
        rectTransform.sizeDelta = size;
    }

    public override Vector2Int GetTileGridPosition(Vector2 mousePosition)
    {
        return Vector2Int.zero;
    }
    public override InventoryGridItem PickUpItem(Vector2Int positionOnGrid)
    {
        if (positionOnGrid != Vector2Int.zero)
            return null;

        InventoryGridItem returnItem = inventoryItemSlot;

        if (returnItem == null)
        {
            return null;
        }

        equipmentStatsController.PickUpItemApplyStatsChange(returnItem.itemData);

        CleanGridReference(returnItem);

        return returnItem;
    }

    public void ResetAvailableSlotType()
    {
        availableItemTypes = new List<EquipmentSlotType>(originalAvailableItemTypes);
    }

    public override InventoryGridItem GetItem(Vector2Int positionOnGrid)
    {
        if (positionOnGrid != Vector2Int.zero)
            return null;

        return inventoryItemSlot;
    }

    protected override void CleanGridReference(InventoryGridItem item)
    {
        inventoryItemSlot = null;
        if (isWeaponSlot)
            GetComponent<WeaponEquipmentSlot>().SetAvailableTypesInArms();
    }

    public override bool PlaceItem(InventoryGridItem item, Vector2Int itemPosition, ref InventoryGridItem overlapItem)
    {
        if (item.itemData.GetType().Equals(typeof(ItemDataEquipable)))
        {
            ItemDataEquipable equipable = (ItemDataEquipable)item.itemData;
            if (!availableItemTypes.Contains(equipable.SlotType))
            {
                if (inventoryItemSlot == null || equipable.SlotType != (inventoryItemSlot.itemData as ItemDataEquipable).SlotType)
                {
                    return false;
                }
            }
        }

        if (!CheckRequierements(item.itemData))
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
            equipmentStatsController.PickUpItemApplyStatsChange(overlapItem.itemData);
            CleanGridReference(overlapItem);
        }

        PlaceItemInInventorySlot(item, itemPosition);
        if (isWeaponSlot)
            GetComponent<WeaponEquipmentSlot>().SetAvailableTypesInArms();

        equipmentStatsController.PlacedItemApplyStatsChange(item.itemData);

        return true;
    }

    private bool CheckRequierements(ItemData itemData)
    {
        if (!itemData.GetType().Equals(typeof(ItemDataEquipable)))
            return false;

        ItemDataEquipable equipable = (ItemDataEquipable)itemData;
        List<Requierement> requierementList = equipable.Requierements;

        int minLvl = requierementList.Find(r => r.Name == "Level").Value;
        int minDex = requierementList.Find(r => r.Name == "Dex").Value;
        int minInt = requierementList.Find(r => r.Name == "Int").Value;
        int minStr = requierementList.Find(r => r.Name == "Str").Value;

        if (playerStats.Level < minLvl)
            return false;

        if (playerStats.Dexterity.CalculatedValue < minDex)
            return false;

        if (playerStats.Intelligence.CalculatedValue < minInt)
            return false;

        if (playerStats.Strength.CalculatedValue < minStr)
            return false;

        return true;
    }

    public override void PlaceItemInInventorySlot(InventoryGridItem item, Vector2Int itemPosition)
    {
        RectTransform rectTransform = item.GetComponent<RectTransform>();
        rectTransform.SetParent(this.rectTransform);
        rectTransform.localScale = Vector3.one;

        inventoryItemSlot = item;

        item.onGridPosition = itemPosition;
        Vector2 position = CalculatePositionOnGrid();
        rectTransform.localPosition = position;
    }

    public Vector2 CalculatePositionOnGrid()
    {
        Vector2 position = new Vector2();
        position.x = (tileSize.x * gridSize.x) / 2;
        position.y = -(tileSize.y * gridSize.y) / 2;
        return position;
    }

    public override Vector2Int? FindSpaceForObject(ItemData itemData)
    {
        return Vector2Int.zero;
    }

    protected override bool CheckAvailableSpace(Vector2Int position, Vector2Int size)
    {
        if (inventoryItemSlot == null)
        {
            InitInvetoryItemSlot(gridSize);
        }

        if (inventoryItemSlot != null)
        {
            return false;
        }

        return true;
    }

    protected override bool CheckOverlap(Vector2Int position, Vector2Int size, ref InventoryGridItem overlapItem)
    {
        if (inventoryItemSlot != null)
        {
            if (overlapItem == null)
            {
                overlapItem = inventoryItemSlot;
            }
            else
            {
                if (overlapItem != inventoryItemSlot)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public override bool RemoveItem(InventoryGridItem item)
    {
        if (item.itemData.canBeDropped)
        {
            CleanGridReference(item);
            Destroy(item.gameObject);
            return true;
        }

        return false;
    }
}
