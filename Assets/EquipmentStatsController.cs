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
                    playerStats.MaxHealth += new Stat(0, 0, changeSign * stat.Value, 0);
                    break;
                }
            case "Mana":
                {
                    playerStats.MaxMana += new Stat(0, 0, changeSign * stat.Value, 0);
                    break;
                }
            case "Dexterity":
                {
                    playerStats.Dexterity += new Stat(0, 0, changeSign * stat.Value, 0);
                    break;
                }
            case "Intelligence":
                {
                    playerStats.Intelligence += new Stat(0, 0, changeSign * stat.Value, 0);
                    break;
                }
            case "Strength":
                {
                    playerStats.Strength += new Stat(0, 0, changeSign * stat.Value, 0);
                    break;
                }
            case "Block Damage":
                {
                    playerCombatStats.BlockDamage += new Stat(0, 0, changeSign * stat.Value, 0);
                    break;
                }
            case "Block Chance":
                {
                    playerCombatStats.BlockChance += new Stat(0, 0, changeSign * stat.Value, 0);
                    break;
                }
            case "Armor":
                {
                    playerCombatStats.Armor += new Stat(0, 0, changeSign * stat.Value, 0);
                    break;
                }
            case "Magic Defense":
                {
                    playerCombatStats.MagicDefense += new Stat(0, 0, changeSign * stat.Value, 0);
                    break;
                }
            case "Evasion":
                {
                    playerCombatStats.Evasion += new Stat(0, 0, changeSign * stat.Value, 0);
                    break;
                }
            case "Attack Speed":
                {
                    playerCombatStats.AttackSpeed += new Stat(0, 0, changeSign * stat.Value, 0);
                    break;
                }
            case "Crit Multiplier":
                {
                    playerCombatStats.CritMultiplier += new Stat(0, 0, changeSign * stat.Value, 0);
                    break;
                }
            case "Crit Chance":
                {
                    playerCombatStats.CritChance += new Stat(0, 0, changeSign * stat.Value, 0);
                    break;
                }
            case "Minimal Physical Damage":
                {
                    playerCombatStats.MinDamage += new Stat(0, 0, changeSign * stat.Value, 0);
                    break;
                }
            case "Maximal Physical Damage":
                {
                    playerCombatStats.MaxDamage += new Stat(0, 0, changeSign * stat.Value, 0);
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
