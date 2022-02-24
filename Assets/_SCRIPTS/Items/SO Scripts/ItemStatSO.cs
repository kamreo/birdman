using UnityEngine;

[System.Serializable]
public abstract class ItemStatSO : ScriptableObject
{
	[SerializeField]
	protected new string name;
	public string Name { get => name; }

	[SerializeField]
	protected int tier = -1;
	public int Tier
	{
		get => tier;
		protected set
		{
			tier = value;

			if (tier < 0)
				tier = 0;

			if (tier > 6)
				tier = 6;
		}
	}

	public virtual void RandValue(int itemLevel)
	{
	}

	public virtual int GetIntValue()
	{
		return -1;
	}

	public virtual float GetFloatValue()
	{
		return -1.0f;
	}

	public virtual (int, int) GetRangeValue()
	{
		return (-1, -1);
	}

	public ItemStatSO(ItemStatSO itemStatSO)
	{
		name = itemStatSO.Name;
		tier = itemStatSO.Tier;
	}
}

