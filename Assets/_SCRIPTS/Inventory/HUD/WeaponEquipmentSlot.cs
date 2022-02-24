using System.Collections.Generic;
using UnityEngine;

public class WeaponEquipmentSlot : MonoBehaviour
{
	[SerializeField]
	EquipmentSlot rightArm;

	[SerializeField]
	EquipmentSlot leftArm;

	// RIGHT HAND ITEMS
	[SerializeField]
	List<EquipmentSlotType> typesWhenOneHandedMeleeWeapon;
	[SerializeField]
	List<EquipmentSlotType> typesWhenRangeWeapon;


	// LEFT HAND ITEMS
	[SerializeField]
	List<EquipmentSlotType> typesWhenOneHandedMeleeWeaponInLeftHand;
	[SerializeField]
	List<EquipmentSlotType> typesWhenQuiver;
	[SerializeField]
	List<EquipmentSlotType> typesWhenShield;

	public void SetAvailableTypesInArms()
	{
		if (rightArm.InventoryItemSlot == null && leftArm.InventoryItemSlot == null)
		{
			rightArm.ResetAvailableSlotType();
			leftArm.ResetAvailableSlotType();
			return;
		}

		if (rightArm.InventoryItemSlot == null)
		{
			UpdateRightArmAvailableTypes();
		}
		else
		{
			UpdateLeftArmAvailableTypes();
		}
	}


	private void UpdateRightArmAvailableTypes()
	{
		EquipmentSlotType leftType = (leftArm.InventoryItemSlot.itemData as ItemDataEquipable).SlotType;

		switch (leftType)
		{
			case EquipmentSlotType.OneHandMeleeWeapon:
				{
					rightArm.AvailableItemTypes = typesWhenOneHandedMeleeWeaponInLeftHand;
					break;
				}

			case EquipmentSlotType.Shield:
				{
					rightArm.AvailableItemTypes = typesWhenShield;
					break;
				}

			case EquipmentSlotType.Quiver:
				{
					rightArm.AvailableItemTypes = typesWhenQuiver;
					break;
				}

			default:
				break;
		}


	}
	private void UpdateLeftArmAvailableTypes()
	{
		EquipmentSlotType rightType = (rightArm.InventoryItemSlot.itemData as ItemDataEquipable).SlotType;

		switch (rightType)
		{
			case EquipmentSlotType.OneHandMeleeWeapon:
				{
					leftArm.AvailableItemTypes = typesWhenOneHandedMeleeWeapon;
					break;
				}

			case EquipmentSlotType.RangeWeapon:
				{
					leftArm.AvailableItemTypes = typesWhenRangeWeapon;
					break;
				}

			case EquipmentSlotType.TwoHandMeleeWeapon:
				{
					leftArm.AvailableItemTypes = new List<EquipmentSlotType>();
					break;
				}

			default:
				break;
		}

	}
}
