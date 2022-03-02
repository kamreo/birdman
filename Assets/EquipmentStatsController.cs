using UnityEngine;

public class EquipmentStatsController : MonoBehaviour
{
	//[SerializeField]
	//EquipmentSlot rightArm;
	//[SerializeField]
	//EquipmentSlot leftArm;

	//[SerializeField]
	//List<EquipmentSlot> armor;

	PlayerStats playerStats;
	PlayerCombatStats playerCombatStats;
	int changeSign;

	private void Start()
	{
		playerStats = FindObjectOfType<PlayerStats>();
		playerCombatStats = FindObjectOfType<PlayerCombatStats>();
	}


	public void PickUpItemApplyStatsChange(ItemData itemData)
	{
		changeSign = -1;
		ItemDataEquipable equipable = (ItemDataEquipable)itemData;

		foreach (var stat in equipable.BaseStats)
		{
			ApplyStatChange(stat);
		}

		foreach (var stat in equipable.AdditionalStats)
		{
			ApplyStatChange(stat);
		}
	}

	public void PlacedItemApplyStatsChange(ItemData itemData)
	{
		changeSign = 1;
		ItemDataEquipable equipable = (ItemDataEquipable)itemData;

		foreach (var stat in equipable.BaseStats)
		{
			ApplyStatChange(stat);
		}

		foreach (var stat in equipable.AdditionalStats)
		{
			ApplyStatChange(stat);
		}
	}

	private void ApplyStatChange(ItemStatSO stat)
	{
		switch (stat.Name)
		{
			case "Health":
				{
					playerStats.MaxHealth.FlatIncome += changeSign * stat.Value;
					break;
				}
			case "Mana":
				{
					playerStats.MaxMana.FlatIncome += changeSign * stat.Value;
					break;
				}
			case "Dexterity":
				{
					playerStats.Dexterity.FlatIncome += changeSign * stat.Value;
					break;
				}
			case "Intelligence":
				{
					playerStats.Intelligence.FlatIncome += changeSign * stat.Value;
					break;
				}
			case "Strength":
				{
					playerStats.Strength.FlatIncome += changeSign * stat.Value;
					break;
				}
			case "Block Damage":
				{
					playerCombatStats.BlockDamage.FlatIncome += changeSign * stat.Value;
					break;
				}
			case "Block Chance":
				{
					playerCombatStats.BlockChance.FlatIncome += changeSign * stat.Value;
					break;
				}
			case "Armor":
				{
					playerCombatStats.Armor.FlatIncome += changeSign * stat.Value;
					break;
				}
			case "Magic Defense":
				{
					playerCombatStats.MagicDefense.FlatIncome += changeSign * stat.Value;
					break;
				}
			case "Evasion":
				{
					playerCombatStats.Evasion.FlatIncome += changeSign * stat.Value;
					break;
				}
			case "Attack Speed":
				{
					playerCombatStats.AttackSpeed.FlatIncome += changeSign * stat.Value;
					break;
				}
			case "Crit Multiplier":
				{
					playerCombatStats.CritMultiplier.FlatIncome += changeSign * stat.Value;
					break;
				}
			case "Crit Chance":
				{
					playerCombatStats.CritChance.FlatIncome += changeSign * stat.Value;
					break;
				}
			case "Minimal Physical Damage":
				{
					playerCombatStats.MinDamage.FlatIncome += changeSign * stat.Value;
					break;
				}
			case "Maximal Physical Damage":
				{
					playerCombatStats.MaxDamage.FlatIncome += changeSign * stat.Value;
					break;
				}


			default:
				{
					Debug.LogError($"Stat: {stat.Name} is not implemented in switch statemant!!!");
					break;
				}
		}
	}
}
