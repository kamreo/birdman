using UnityEngine;

public class ItemData : ScriptableObject
{
	// Basic information
	public new string name;
	public string description;
	public ItemRarityData rarityData;
	public Sprite graphics;

	public float chanceToDrop;

	// Inventory data
	public Vector2Int size; //x and y
	public bool canBeDropped = true;


	public void OnEnable()
	{
		if (size == Vector2Int.zero)
		{
			//int width = (int)(graphics.rect.size.x / 32);
			//int height = (int)(graphics.rect.size.y / 32);
			int width = (int)(graphics.rect.size.x / ItemGrid.tileSize.x);
			int height = (int)(graphics.rect.size.y / ItemGrid.tileSize.y);
			size = new Vector2Int(width, height);
		}
	}
}