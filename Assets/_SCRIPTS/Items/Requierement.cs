using UnityEngine;

[System.Serializable]
public struct Requierement
{
	[SerializeField]
	private string name;
	public string Name { get => name; }

	[SerializeField]
	private int minValue;
	public int MinValue { get => minValue; }
	[SerializeField]
	private int maxValue;
	public int MaxValue { get => maxValue; }

	[SerializeField]
	private int value;
	public int Value { get => value; private set => this.value = value; }
	public Requierement(Requierement requierement) : this()
	{
		name = requierement.name;
		minValue = requierement.minValue;
		maxValue = requierement.maxValue;
		Init();
	}

	private void Init()
	{
		value = Random.Range(minValue, maxValue);
	}
}