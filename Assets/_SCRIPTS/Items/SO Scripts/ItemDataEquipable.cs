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
	private List<Requierement> requierements;
	public List<Requierement> Requierements { get => requierements; }

	[SerializeField]
	private ItemStatSO[] baseStats;
	public ItemStatSO[] BaseStats { get => baseStats; }

	public ItemStatInt additionalStatsCount;

	[SerializeField]
	private List<ItemStatSO> additionalStatsEditor;

	private ItemStatSO[] additionalStats;
	public ItemStatSO[] AdditionalStats { get => additionalStats; }

	private new void OnEnable()
	{
		foreach (var item in requierements)
		{
			item.Init();
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