using UnityEngine;


public class Functionalities : MonoBehaviour //inventoryTab
{
	[Header("Inventory Openning")]
	public GameObject inventoryTab;

	float timeUntilClose = 0.25f;
	float startTime = 0;
	float currentTime;
	bool open;

	private void Start()
	{
		open = false;
		inventoryTab.SetActive(open);
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		//open inventory using key I, and dont let open it so fast
		if (Input.GetButton("Inventory") && currentTime >= timeUntilClose)
		{
			currentTime = startTime;
			open = !open;
			inventoryTab.SetActive(open);
		}
		else
		{
			currentTime += Time.deltaTime;
		}
	}
}


