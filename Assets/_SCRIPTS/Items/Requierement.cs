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

	private int value;
	public int Value { get => value; }

	public void Init()
	{
		value = Random.Range(minValue, maxValue);
	}
}