using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Item Stat", menuName = "Inventory/Item Stat")]
public class ItemStatSO : ScriptableObject
{
    [SerializeField]
    private new string name;
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

    [SerializeField]
    private ItemStat[] tierValues;

    [SerializeField]
    private int value = -1;
    public int Value { get => value; }

    public void Init(ItemStatSO itemStatSO)
    {
        name = itemStatSO.Name;
        tierValues = itemStatSO.tierValues;
    }

    public void RandValue(int itemLevel)
    {
        //Tier = Random.Range(itemLevel / 10 - 3, itemLevel / 10 + 2);
        Tier = (int)(3f / 40f * itemLevel) + Random.Range(0, 2);
        value = Random.Range(tierValues[tier].MinValue, tierValues[tier].MaxValue);
    }

    public void RandValueByTier(int tier)
    {
        //Tier = Random.Range(itemLevel / 10 - 3, itemLevel / 10 + 2);
        this.tier = tier;
        value = Random.Range(tierValues[tier].MinValue, tierValues[tier].MaxValue);
    }

    //private void OnEnable()
    //{
    //    if (tierValues == null)
    //    {
    //        Debug.LogError($"NAME:{name} ItemStatSO has no tierValues array!!!");
    //    }

    //    if (tierValues.Length != 6)
    //    {
    //        Debug.LogError($"{this.name} has NOT 6 tiers values");
    //    }
    //}
}

