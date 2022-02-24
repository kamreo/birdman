using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingItems : MonoBehaviour
{
	bool canPick = true;
	bool clicked = false;

	[SerializeField, Range(0.1f, 1f)]
	float pickCooldown = 0.3f;

	InventoryController inventoryController;
	List<Transform> itemsInRange;

	private void Awake()
	{
		itemsInRange = new List<Transform>();
		inventoryController = FindObjectOfType<InventoryController>();
	}

	private void Update()
	{
		if (itemsInRange.Count <= 0)
			return;

		if (!canPick)
			return;

		clicked = Input.GetButton("PickItem");
	}

	private void FixedUpdate()
	{
		if (!clicked)
			return;

		clicked = false;
		canPick = false;

		itemsInRange[FindClosestItemIndex()].GetComponent<Pickable>().PickItem();

		StartCoroutine(Cooldown(pickCooldown));
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (itemsInRange.Contains(collision.transform))
			return;

		itemsInRange.Add(collision.transform);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (!itemsInRange.Contains(collision.transform))
			return;

		itemsInRange.Remove(collision.transform);
	}
	private IEnumerator Cooldown(float duration)
	{
		yield return new WaitForSeconds(duration);

		canPick = true;
	}

	private int FindClosestItemIndex()
	{
		Transform player = GetComponent<Transform>();
		int closestItemIndex = 0;


		float closest = float.MaxValue;
		for (int i = 0; i < itemsInRange.Count; i++)
		{
			// Cant pick to inventory so we won't calc distance for that item
			if (!inventoryController.FindSpaceForObjectInPlayerInventory(itemsInRange[i].transform.GetComponent<Pickable>().ItemData))
				continue;

			float distance = Vector3.Distance(player.position, itemsInRange[i].position);
			if (distance < closest)
			{
				closest = distance;
				closestItemIndex = i;
			}
		}

		return closestItemIndex;
	}
}
