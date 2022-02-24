using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemDescription : MonoBehaviour
{
	[SerializeField]
	RectTransform description;
	Vector2 originalDescriptionSize;

	[SerializeField]
	GameObject baseStatText;

	[SerializeField]
	GameObject additionalStatText;

	TMP_Text itemName;
	RectTransform itemNameRect;
	float originalItemNameHeight;


	TMP_Text requierementsText;
	Transform baseStats;
	Transform additionalStats;

	PlayerStats playerStats;

	float itemDescriptionHeight;
	float baseStatsHeight;
	float additionalStatsHeight;

	[SerializeField]
	Vector2 baseStatsPosition = new Vector2();

	ItemData currentItem;

	private void Awake()
	{
		itemName = description.Find("ItemNameBackground").Find("ItemNameText").GetComponent<TMP_Text>();
		baseStats = description.Find("Base Stats");
		additionalStats = baseStats.Find("Requierements").Find("Additional Stats");
		requierementsText = baseStats.Find("Requierements").Find("RequierementsText").GetComponent<TMP_Text>();

		originalDescriptionSize = description.sizeDelta;
		itemNameRect = itemName.transform.parent.GetComponent<RectTransform>();
		originalItemNameHeight = itemNameRect.sizeDelta.y;

		playerStats = FindObjectOfType<PlayerStats>();
	}

	public void SetPosition(InventoryGridItem inventoryGridItem)
	{
		description.position = inventoryGridItem.transform.position;

		CorrectBorders();

		if (!inventoryGridItem.itemData.GetType().Equals(typeof(ItemDataEquipable)))
		{
			Vector2 newPosition = new Vector2(description.position.x, description.position.y);
			newPosition.y -= description.sizeDelta.y - itemNameRect.sizeDelta.y;

			description.position = newPosition;
		}

		description.SetAsLastSibling();
	}

	public void SetDescription(ItemData itemData)
	{
		if (currentItem == itemData)
			return;

		currentItem = itemData;


		TrySetText(itemName, itemData.name);
		itemName.fontSize = 14;

		itemDescriptionHeight = itemNameRect.sizeDelta.y;
		baseStatsHeight = 0f;

		Image itemNameBackground = itemNameRect.GetComponent<Image>();
		itemNameBackground.color = itemData.rarityData.inventoryColor;

		itemNameRect.sizeDelta = new Vector2(itemNameRect.sizeDelta.x, originalItemNameHeight);
		if (!itemData.GetType().Equals(typeof(ItemDataEquipable)))
		{
			baseStats.gameObject.SetActive(false);
			description.GetComponent<Image>().enabled = false;
			description.sizeDelta = new Vector2(originalDescriptionSize.x / 2f, originalDescriptionSize.y / 2f);
			itemName.fontSize = 10;

			itemNameRect.sizeDelta = new Vector2(itemNameRect.sizeDelta.x, itemNameRect.sizeDelta.y / 2);

			return;
		}


		description.sizeDelta = originalDescriptionSize;

		baseStats.gameObject.SetActive(true);
		description.GetComponent<Image>().enabled = true;

		ClearBaseStats();

		ItemDataEquipable equipable = (ItemDataEquipable)itemData;

		SetBaseStats(equipable.BaseStats);
		baseStatText.gameObject.SetActive(false);

		SetRequierements(equipable);
		baseStatsHeight += requierementsText.transform.parent.GetComponent<RectTransform>().sizeDelta.y;
		itemDescriptionHeight += requierementsText.transform.parent.GetComponent<RectTransform>().sizeDelta.y;

		RectTransform baseStatsRect = baseStats.GetComponent<RectTransform>();
		baseStatsRect.sizeDelta = new Vector2(baseStatsRect.sizeDelta.x, baseStatsHeight);

		baseStatsPosition = new Vector2();
		baseStatsPosition.x = 0;
		baseStatsPosition.y = -itemName.transform.parent.GetComponent<RectTransform>().sizeDelta.y - baseStatsHeight / 2;
		baseStatsRect.anchoredPosition = baseStatsPosition;

		ClearAdditionalStats();
		SetAdditionalStats(equipable.AdditionalStats);
		additionalStatText.gameObject.SetActive(false);


		RectTransform additionalRect = additionalStats.GetComponent<RectTransform>();
		additionalRect.sizeDelta = new Vector2(additionalRect.sizeDelta.x, additionalStatsHeight);

		Vector2 additionalNewPosition = new Vector2();
		additionalNewPosition.x = 0;
		additionalNewPosition.y = -additionalRect.sizeDelta.y / 2;
		additionalRect.anchoredPosition = additionalNewPosition;

		float paddingBot = 2f;
		description.sizeDelta = new Vector2(description.sizeDelta.x, itemDescriptionHeight + paddingBot);
	}

	private void ClearBaseStats()
	{
		int reqIndex = baseStats.childCount;
		for (int i = 0; i < reqIndex - 2; i++)
		{
			Transform child = baseStats.GetChild(i);
			Destroy(child.gameObject);
		}
	}

	private void ClearAdditionalStats()
	{
		int additionalStatsCount = additionalStats.childCount;

		for (int i = 0; i < additionalStatsCount - 1; i++)
		{
			Transform child = additionalStats.GetChild(i);

			Destroy(child.gameObject);
		}
	}

	private void SetRequierements(ItemDataEquipable itemData)
	{
		// requiers level <color=#EA0000>x</color>, a dex, b int, <color=#EA0000> c </color> str
		int reqLevel = (int)itemData.Requierements.Where(i => i.Name.Equals("Level")).First().Value;
		string level = reqLevel <= playerStats.Level ? reqLevel.ToString() : $"<color=#EA0000>{reqLevel}</color>";

		int reqDex = (int)itemData.Requierements.Where(i => i.Name.Equals("Dex")).First().Value;
		string dex = "";
		if (reqDex > 0)
			dex = reqDex <= playerStats.Dexterity.CalculatedValue ? reqDex.ToString() : $", <color=#EA0000>{reqDex}</color> dex";


		int reqInt = (int)itemData.Requierements.Where(i => i.Name.Equals("Int")).First().Value;
		string intel = "";
		if (reqInt > 0)
			intel = reqInt <= playerStats.Intelligence.CalculatedValue ? reqInt.ToString() : $", <color=#EA0000>{reqInt}</color> int";

		int reqStr = (int)itemData.Requierements.Where(i => i.Name.Equals("Str")).First().Value;
		string str = "";
		if (reqStr > 0)
			str = reqStr <= playerStats.Strength.CalculatedValue ? reqStr.ToString() : $", <color=#EA0000>{reqStr}</color> str";

		string requierements = $"requiers level {level}{dex}{intel}{str}";

		TrySetText(requierementsText, requierements);
	}

	private void SetBaseStats(ItemStatSO[] itemStats)
	{
		float padding = 0;
		itemDescriptionHeight += padding;
		baseStatsHeight += padding;

		int statsCount = itemStats.Length;
		for (int i = 0; i < statsCount; i++)
		{
			GameObject currentStatObject = Instantiate(baseStatText, baseStats);
			currentStatObject.SetActive(true);

			currentStatObject.transform.SetSiblingIndex(i);
			RectTransform rect = currentStatObject.GetComponent<RectTransform>();

			Vector2 newPosition = new Vector2();
			newPosition.x = currentStatObject.transform.position.x;
			newPosition.y = currentStatObject.transform.position.y - i * rect.sizeDelta.y - padding;
			currentStatObject.transform.position = newPosition;

			TMP_Text currentStatText = currentStatObject.GetComponent<TMP_Text>();


			string textToSet = $"{itemStats[i].Name}: <color=#4281FF> {CreateStatValueText(itemStats[i])} </color>";
			TrySetText(currentStatText, textToSet);

			itemDescriptionHeight += rect.sizeDelta.y;
			baseStatsHeight += rect.sizeDelta.y;
		}
	}

	private string CreateStatValueText(ItemStatSO itemStat)
	{

		string value = "ERR";
		Type statType = itemStat.GetType();

		if (statType.Equals(typeof(ItemStatSOInt)) || statType.Equals(typeof(ItemStatSOFloat)))
		{
			value = itemStat.GetIntValue().ToString();
		}
		if (statType.Equals(typeof(ItemStatSORange)))
		{
			value = $"{itemStat.GetRangeValue().Item1 - itemStat.GetRangeValue().Item2}";
		}

		if (itemStat.Name.Contains("Chance"))
		{
			value += "%";
		}

		return value;
	}

	private void SetAdditionalStats(ItemStatSO[] itemStats)
	{
		int statsCount = itemStats.Length;
		for (int i = 0; i < statsCount; i++)
		{
			GameObject currentStatObject = Instantiate(additionalStatText, additionalStats);
			currentStatObject.SetActive(true);

			currentStatObject.transform.SetSiblingIndex(i);
			RectTransform rect = currentStatObject.GetComponent<RectTransform>();

			Vector2 newPosition = new Vector2();
			newPosition.x = currentStatObject.transform.position.x;
			newPosition.y = currentStatObject.transform.position.y - i * rect.sizeDelta.y;
			currentStatObject.transform.position = newPosition;

			TMP_Text currentStatText = currentStatObject.GetComponent<TMP_Text>();

			string textToSet = $"+ {CreateStatValueText(itemStats[i])} to {itemStats[i].Name}";
			TrySetText(currentStatText, textToSet);

			itemDescriptionHeight += rect.sizeDelta.y;
			additionalStatsHeight += rect.sizeDelta.y;
		}
	}

	public void TrySetText(TMP_Text textObject, string text)
	{
		if (textObject == null)
			return;

		textObject.SetText(text);
	}

	private void CorrectBorders()
	{
		Vector2 newPosition = description.position;
		RectTransform currentBorder;

		currentBorder = description.Find("Down").GetComponent<RectTransform>();
		if (currentBorder.position.y < 0)
		{
			float over = -currentBorder.position.y;
			float moveDistance = over + 50f;

			newPosition.y += moveDistance;
		}

		currentBorder = description.Find("Up").GetComponent<RectTransform>();
		if (currentBorder.position.y > Screen.height)
		{
			float over = currentBorder.position.y - Screen.height;
			float moveDistance = over + 50f;

			newPosition.y -= moveDistance;
		}

		currentBorder = description.Find("Right").GetComponent<RectTransform>();
		if (currentBorder.position.x > Screen.width)
		{
			float moveDistance = description.sizeDelta.x;

			newPosition.x -= moveDistance;
		}

		currentBorder = description.Find("Left").GetComponent<RectTransform>();
		if (currentBorder.position.x < 0)
		{
			float moveDistance = description.sizeDelta.x;

			newPosition.x += moveDistance;
		}

		description.position = newPosition;
	}

	public void Show(bool toDisplay)
	{
		description.gameObject.SetActive(toDisplay);
	}
}
