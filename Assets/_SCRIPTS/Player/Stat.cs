using System;
using UnityEngine;

[Serializable]
public class Stat
{
	[SerializeField] private bool isInteger = true;
	[SerializeField] private float calculatedValue;     // It is base value after implications = (baseValue + flatIncome) * (1f + percentIncome / 100f)
														// Changing value in below variables runs CalculateValue() function
	[SerializeField] private float baseValue;           // Starting stat value 
	[SerializeField] private float flatIncome;          // Flat value added to base one
	[SerializeField] private float percentIncome;       // Percentage modifier

	public float CalculatedValue { get => calculatedValue; set => calculatedValue = value; }
	public float BaseValue
	{
		get => baseValue;
		set
		{
			if (value > 0)
			{
				baseValue = value;
				CalculateValue();
			}
		}
	}
	public float FlatIncome
	{
		get => flatIncome;
		set
		{
			if (value >= 0)
			{
				flatIncome = value;
				CalculateValue();
			}
		}
	}
	public float PercentIncome
	{
		get => percentIncome;
		set
		{
			if (value >= 0)
			{
				percentIncome = value;
				CalculateValue();
			}
		}
	}


	public Stat(float baseValue, float flatIncome, float percentIncome)
	{
		BaseValue = baseValue;
		FlatIncome = flatIncome;
		PercentIncome = percentIncome;
		CalculateValue();
	}

	public Stat(Stat copyStat)
	{
		BaseValue = copyStat.BaseValue;
		FlatIncome = copyStat.FlatIncome;
		PercentIncome = copyStat.PercentIncome;
		CalculateValue();
	}

	public Stat()
	{
		CalculateValue();
	}

	public Stat(float baseValue)
	{
		BaseValue = baseValue;
		CalculateValue();
	}

	public static Stat CreateFlatIncomeStat(float flatIncome)
	{
		return new Stat(0f, flatIncome, 0f);
	}

	public static Stat operator +(Stat baseStat, Stat add)
	{
		baseStat.baseValue += add.baseValue;
		baseStat.flatIncome += add.flatIncome;
		baseStat.percentIncome += add.percentIncome;

		baseStat.CalculateValue();

		return baseStat;
	}

	public static Stat operator -(Stat baseStat, Stat add)
	{
		baseStat.baseValue -= add.baseValue;
		baseStat.flatIncome -= add.flatIncome;
		baseStat.percentIncome -= add.percentIncome;

		baseStat.CalculateValue();

		return baseStat;
	}

	public override string ToString() => $"Calculataed value equals: {CalculatedValue}";

	public void CalculateValue()
	{
		if (isInteger)
			CalculatedValue = (int)((BaseValue + FlatIncome) * (1 + PercentIncome / 100f));
		else
			CalculatedValue = (BaseValue + FlatIncome) * (1 + PercentIncome / 100f);
	}

	public int GetIntValue()
	{
		return (int)CalculatedValue;
	}
}
