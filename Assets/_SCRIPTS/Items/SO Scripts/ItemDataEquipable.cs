using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item/Equipable")]
public class ItemDataEquipable : ItemData
{
	[SerializeField]
	private EquipmentSlotType slotType;
	public EquipmentSlotType SlotType { get => slotType; }

	private int itemLevel;
	public int ItemLevel { get => itemLevel; }

	[SerializeField]
	private List<Requierement> requierementsEditor;
	private List<Requierement> requierements;
	public List<Requierement> Requierements { get => requierements; private set => requierements = value; }

	[SerializeField]
	private ItemStatSO[] baseStats;
	public ItemStatSO[] BaseStats { get => baseStats; }

	public ItemStat additionalStatsCount;

	[SerializeField]
	private List<ItemStatSO> additionalStatsEditor;

	private ItemStatSO[] additionalStats;
	public ItemStatSO[] AdditionalStats { get => additionalStats; }

	private new void OnEnable()
	{
		requierements = new List<Requierement>();
		foreach (var item in requierementsEditor)
		{
			requierements.Add(new Requierement(item));
		}

		foreach (var item in baseStats)
		{
			item.RandValue(itemLevel);
		}

		int additionalStatsCountRand = Random.Range(additionalStatsCount.MinValue, additionalStatsCount.MaxValue + 1);
		if (additionalStatsCountRand > additionalStatsEditor.Count)
			additionalStatsCountRand = additionalStatsEditor.Count;

		additionalStats = additionalStatsEditor.OrderBy(x => Random.Range(float.MinValue, float.MaxValue)).Take(additionalStatsCountRand).Distinct().ToArray();
		foreach (var item in additionalStats)
		{
			item.RandValue(itemLevel);
		}
		base.OnEnable();
	}
}