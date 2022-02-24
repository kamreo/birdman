using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Util/Rarity")]

public class ItemRarityData : ScriptableObject
{
	public ItemRarity rarity;
	public Color mainColor;
	public Color borderColor;
	public Color inventoryColor;
}