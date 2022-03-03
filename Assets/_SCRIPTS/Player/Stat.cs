using System;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField] private int calculatedValue;     // It is base value after implications = (baseValue + flatIncome) * (1f + percentIncome / 100f)
                                                      // Changing value in below variables runs CalculateValue() function
    [SerializeField] private int baseValue;           // Starting stat value 
    [SerializeField] private int flatIncome;          // Flat value added to base one
    [SerializeField] private int itemIncome;          // Flat value added to base one
    [SerializeField] private int percentIncome;       // Percentage modifier

    public int CalculatedValue { get => calculatedValue; set => calculatedValue = value; }
    public int BaseValue
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
    public int FlatIncome
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
    public int ItemIncome
    {
        get => itemIncome;
        set
        {
            if (value >= 0)
            {
                itemIncome = value;
                CalculateValue();
            }
        }
    }
    public int PercentIncome
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


    public Stat(int baseValue, int flatIncome, int itemIncome, int percentIncome)
    {
        BaseValue = baseValue;
        FlatIncome = flatIncome;
        PercentIncome = percentIncome;
        ItemIncome = itemIncome;
        CalculateValue();
    }

    public Stat(Stat copyStat)
    {
        BaseValue = copyStat.BaseValue;
        FlatIncome = copyStat.FlatIncome;
        PercentIncome = copyStat.PercentIncome;
        ItemIncome = copyStat.ItemIncome;
        CalculateValue();
    }

    public Stat()
    {
        CalculateValue();
    }

    public Stat(int baseValue)
    {
        BaseValue = baseValue;
        CalculateValue();
    }

    public static Stat operator +(Stat baseStat, Stat add)
    {
        baseStat.BaseValue += add.BaseValue;
        baseStat.FlatIncome += add.FlatIncome;
        baseStat.ItemIncome += add.ItemIncome;
        baseStat.PercentIncome += add.PercentIncome;

        baseStat.CalculateValue();

        return baseStat;
    }

    public static Stat operator -(Stat baseStat, Stat add)
    {
        baseStat.BaseValue -= add.BaseValue;
        baseStat.FlatIncome -= add.FlatIncome;
        baseStat.ItemIncome -= add.ItemIncome;
        baseStat.PercentIncome -= add.PercentIncome;

        baseStat.CalculateValue();

        return baseStat;
    }

    public override string ToString() => $"Calculataed value equals: {CalculatedValue}";

    public void CalculateValue()
    {
        CalculatedValue = (int)((BaseValue + FlatIncome + ItemIncome) * (1 + PercentIncome / 100f));
    }
}
