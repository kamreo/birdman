using System.Collections;
using UnityEngine;

public class PlayerHudHandler : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField]
    private GameObject inventoryTab;

    float openCooldown = 0.25f;
    bool canOpenInventory = true;

    private void Start()
    {
        inventoryTab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Inventory") && canOpenInventory)
        {
            canOpenInventory = false;
            inventoryTab.SetActive(!inventoryTab.activeSelf);
            StartCoroutine(Cooldown(openCooldown));
        }
    }

    private IEnumerator Cooldown(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        canOpenInventory = true;
    }
}
