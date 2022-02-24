using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pickable : MonoBehaviour
{
	bool picked = false;

	[SerializeField]
	ItemData itemData;
	public ItemData ItemData { get => itemData; }

	[SerializeField]
	GameObject inventoryItemPrefab;

	//Transform notificationsCanvas;
	//float notificationDuration = 1.5f;

	[SerializeField]
	Canvas autoResizeCanvas;
	[SerializeField]
	Canvas itemFrameCanvas;

	[SerializeField]
	Image background;

	[SerializeField]
	GameObject borderObject;

	[SerializeField]
	TMP_Text itemNameText;

	private Transform itemsGridObject;


	private void Start()
	{
		SetFrame(itemData);
		itemsGridObject = Resources.FindObjectsOfTypeAll<ItemGrid>().FirstOrDefault(g => g.name.Equals("ItemsGrid")).transform;
	}

	internal void PickItem()
	{
		if (picked)
			return;

		if (itemData == null)
			return;


		InventoryController inventoryController = FindObjectOfType<InventoryController>();
		InventoryGridItem inventoryItem = Instantiate(inventoryItemPrefab, itemsGridObject).GetComponent<InventoryGridItem>();

		inventoryItem.SetItemData(itemData);

		bool placed = inventoryController.PickItemToPlayerInventory(inventoryItem);

		if (!placed)
		{
			//StartCoroutine(ShowCantPickNotification(notificationDuration, inventoryItem.itemData.name));
			Destroy(inventoryItem.gameObject);
			return;
		}
		picked = true;

		Destroy(gameObject);
	}

	public void PickItemByClick()
	{
		if (picked)
			return;

		if (itemData == null)
			return;

		if (GameObject.Find("InventoryCanvas") == null)
		{
			PickItem();
		}
		else
		{
			InventoryController inventoryController = FindObjectOfType<InventoryController>();

			if (inventoryController.IsHoldingItem())
			{
				return;
			}


			inventoryController.CreateItemOnItemFrameClicked(itemData, itemsGridObject);
			Destroy(gameObject);
		}
	}


	//private IEnumerator ShowCantPickNotification(float duration, string itemName)
	//{
	//	TMP_Text text = cantPickNotification.transform.GetChild(0).GetComponentInChildren<TMP_Text>();

	//	text.SetText("Can't pick " + itemName);
	//	cantPickNotification.SetActive(true);
	//	Debug.Log(cantPickNotification.gameObject.activeSelf);

	//	yield return new WaitForSeconds(duration);

	//	cantPickNotification.SetActive(false);
	//}

	public void SetItemData(ItemData itemData)
	{
		this.itemData = itemData;

		SetFrame(itemData);
	}

	private void SetFrameSize()
	{
		Canvas.ForceUpdateCanvases();
		autoResizeCanvas.GetComponent<VerticalLayoutGroup>().enabled = false;
		autoResizeCanvas.GetComponent<VerticalLayoutGroup>().enabled = true;
		RectTransform targetRect = autoResizeCanvas.GetComponent<RectTransform>();

		float widthMargin = 0.35f;

		itemFrameCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(targetRect.sizeDelta.x + widthMargin, targetRect.sizeDelta.y);

		Vector2 newColliderSize = new Vector2();
		newColliderSize.x = (targetRect.sizeDelta.x + widthMargin) * targetRect.localScale.x;
		newColliderSize.y = targetRect.sizeDelta.y * targetRect.localScale.x;

		autoResizeCanvas.transform.parent.GetComponent<BoxCollider2D>().size = newColliderSize;
	}

	private void SetColor(ItemData itemData)
	{
		background.color = itemData.rarityData.mainColor;

		foreach (var image in borderObject.gameObject.GetComponentsInChildren<Image>())
		{
			image.color = itemData.rarityData.borderColor;
		}
	}

	private void SetTextName(ItemData itemData)
	{
		itemNameText.text = itemData.name;
	}

	private void SetFrame(ItemData itemData)
	{
		SetTextName(itemData);
		SetFrameSize();
		SetColor(itemData);
	}

}
