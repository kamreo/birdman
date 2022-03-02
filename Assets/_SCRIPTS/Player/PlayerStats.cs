using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour, IBaseStats, IPlayerStats
{
    private const int fixedUpdateRate = 50;         // Value needed to correctly apply regeneration

    [Header("Bools")]
    [SerializeField] bool resetStats = false;
    [SerializeField] bool printPassvieNodesIds = false;

    #region BaseStats

    [Header("Dexterity")]
    [SerializeField]
    private int baseDexterity;
    [SerializeField]
    private Stat dexterity;
    public Stat Dexterity { get; set; }
    [SerializeField]
    private int attackSpeedPerDexteriry;
    public int AttackSpeedPerDexterity { get => attackSpeedPerDexteriry; }
    [SerializeField]
    private int evasionPerDexterity;
    public int EvasionPerDexterity { get => evasionPerDexterity; }

    [Header("Intelligence")]
    [SerializeField]
    private int baseIntelligence;
    [SerializeField]
    private Stat intelligence;
    public Stat Intelligence { get; set; }
    [SerializeField]
    private int maxManaPerIntelligence;
    public int MaxManaPerIntelligence { get => maxManaPerIntelligence; }
    [SerializeField]
    private int magicalAttackDamagePerIntelligence;
    public int MagicalAttackDamagePerIntelligence { get => MagicalAttackDamagePerIntelligence; }

    [Header("Strength")]
    [SerializeField]
    private int baseStrength;
    [SerializeField]
    private Stat strength;
    public Stat Strength { get; set; }
    [SerializeField]
    private int maxHealthPerStrength;
    public int MaxHealthPerStrength { get => maxHealthPerStrength; }
    [SerializeField]
    private int physicalAttackDamagePerStrength;
    public int PhysicalAttackDamagePerStrength { get => physicalAttackDamagePerStrength; }


    //public void ModifyPlayerStat(ItemStatSO itemStat)
    //{
    //	ModifyPlayerStat(itemStat.Name, itemStat.Value);
    //}

    //public void ModifyPlayerStat(string statName, int value)
    //{
    //	switch (statName)
    //	{
    //		case "Dexterity":
    //			{
    //				dexterity.BaseValue += value;
    //				break;
    //			}
    //		case "Intelligence":
    //			{
    //				intelligence.BaseValue += value;
    //				break;
    //			}
    //		case "Strength":
    //			{
    //				strength.BaseValue += value;
    //				break;
    //			}
    //		default:
    //			break;
    //	}
    //}

    #endregion

    #region Health
    [Header("Health")]

    // Available in Unity
    [SerializeField] Stat maxHealth;
    [SerializeField] float currentHealth = 500;
    [Space]
    [Tooltip("Health regeneration per second (applied 1/50 value per fixedUpdate()")]
    [SerializeField] Stat regenHealth;
    [Space]
    [SerializeField] Slider hpSlider;               // UI Slider that shows current HP

    // Properties
    public Stat MaxHealth
    {
        get => maxHealth;
        set
        {
            maxHealth = value;
            hpSlider.maxValue = MaxHealth.CalculatedValue;
        }
    }

    public float CurrentHealth
    {
        get => currentHealth;
        set
        {
            if (value < MaxHealth.CalculatedValue)
            {
                currentHealth = value;
            }
            else
            {
                currentHealth = MaxHealth.CalculatedValue;
            }
        }
    }

    public Stat RegenHealth
    {
        get => regenHealth;
        set => regenHealth = value;
    }


    #endregion

    #region Mana
    [Header("Mana")]

    // Available in Unity
    [SerializeField] Stat maxMana;
    [SerializeField] float currentMana = 200;
    [Tooltip("Health regeneration per second (applied 1/50 value per fixedUpdate()")]
    [SerializeField] Stat regenMana;
    [Space]
    [SerializeField] Slider mpSlider;                // UI Slider that shows current MP

    // Properties
    public Stat MaxMana
    {
        get => maxMana;
        set
        {
            maxMana = value;
            mpSlider.maxValue = MaxMana.CalculatedValue;
        }
    }
    public float CurrentMana
    {
        get => currentMana;
        set
        {
            // value = currentaMana + value
            if (value < MaxMana.CalculatedValue)
            {
                currentMana = value;
            }
            else if (value < 0)
            {
                Debug.LogWarning("New currentMana value cannot be below 0 !!!");
            }
            else
            {
                currentMana = MaxMana.CalculatedValue;
            }
        }
    }

    public Stat RegenMana
    {
        get => regenMana;
        set => regenMana = value;
    }

    #endregion

    #region Experience
    [Header("Experience")]

    // Available in Unity
    [SerializeField] Slider expSlider;
    [Space]
    [SerializeField] int expNeededToLevelUp = 600;
    [SerializeField] int currentExp = 0;
    [Space]
    [SerializeField] int level = 1;
    [SerializeField] TMP_Text levelText;
    [Space]
    [SerializeField] int spellPoints = 1;
    [SerializeField] TMP_Text spellPointsText;
    [Space]
    [SerializeField] int passivePoints = 1;
    [SerializeField] TMP_Text passivePointsText;

    public int CurrentExp
    {
        get => currentExp;
        set
        {
            if (value > 0)
            {
                currentExp = value;
            }
        }
    }
    public int ExpNeededToLevelUp
    {
        get => expNeededToLevelUp;
        set
        {
            if (value > 0)
            {
                expNeededToLevelUp = value;
            }
        }
    }

    public int Level
    {
        get => level;
        set
        {
            if (value >= 1)
            {
                level = value;
                levelText.text = spellPoints.ToString();
            }
        }
    }

    public int SpellPoints
    {
        get => spellPoints;
        set
        {
            if (value >= 0)
            {
                spellPoints = value;
                if (spellPointsText != null)
                    spellPointsText.text = spellPoints.ToString();
            }
        }
    }

    public int PassivePoints
    {
        get => passivePoints;
        set
        {
            if (value >= 0)
            {
                passivePoints = value;
                if (passivePointsText != null)
                    passivePointsText.text = passivePoints.ToString();
            }
        }
    }

    void LevelUpPlayer()
    {
        LevelUp();
        UpdateHUD();
    }

    void CalcNeededExperienceToLevelUp()
    {
        float a = 5;        // a > 0
        float b = 10;
        float c = 485;      // a+b+c = exp needed to level up at 1 level

        // The basic quadratic function to calc
        expNeededToLevelUp = (int)Mathf.Floor(a * Level * Level + b * Level + c);
    }

    #endregion

    #region Passives
    //[Header("Passives")]
    //public List<int> PassiveIds;

    #endregion

    #region Material and colors
    //[Header("Material and colors")]
    //[SerializeField] Material spriteMaterial;
    //[SerializeField] Color takeDamageTint;
    //Color currentTint;
    //[SerializeField] float tintFadeSpeed;

    #endregion

    #region Notifications

    [Header("Notifications")]
    [SerializeField]
    private GameObject passivesButton;

    #endregion

    #region Methods
    void LevelUp()
    {
        Level++;
        SpellPoints++;
        PassivePoints += 3;

        currentExp -= expNeededToLevelUp;
        CalcNeededExperienceToLevelUp();
        passivesButton.SetActive(true);
    }

    void UpdateHUD()
    {
        expSlider.maxValue = expNeededToLevelUp;
        hpSlider.maxValue = MaxHealth.CalculatedValue;
        mpSlider.maxValue = MaxMana.CalculatedValue;
        levelText.text = level.ToString();

        if (spellPointsText != null)
            spellPointsText.text = spellPoints.ToString();

        if (passivePointsText != null)
            passivePointsText.text = passivePoints.ToString();
    }

    public void SetUp()
    {
        Dexterity = new Stat(baseDexterity);
        Intelligence = new Stat(baseIntelligence);
        Strength = new Stat(baseStrength);

        maxHealth.CalculateValue();
        hpSlider.minValue = 0;
        hpSlider.maxValue = maxHealth.CalculatedValue;
        hpSlider.value = currentHealth;

        maxMana.CalculateValue();
        mpSlider.minValue = 0;
        mpSlider.maxValue = MaxMana.CalculatedValue;
        mpSlider.value = currentMana;

        CalcNeededExperienceToLevelUp();
        expSlider.minValue = 0;
        expSlider.maxValue = expNeededToLevelUp;
        expSlider.value = currentExp;

        levelText.text = level.ToString();
        if (spellPointsText != null)
            spellPointsText.text = spellPoints.ToString();
        if (passivePointsText != null)
            passivePointsText.text = passivePoints.ToString();

        //spriteMaterial.SetColor("_Tint", currentTint);
    }

    public void ResetStats()
    {
        MaxHealth = new Stat(MaxHealth.BaseValue);
        MaxMana = new Stat(MaxMana.BaseValue);
        RegenHealth = new Stat(RegenHealth.BaseValue);
        RegenMana = new Stat(RegenMana.BaseValue);

        Dexterity = new Stat(Dexterity.BaseValue);
        Intelligence = new Stat(Intelligence.BaseValue);
        Strength = new Stat(Strength.BaseValue);

        //PassiveIds = new List<int>();
        PassivePoints = Level;
    }

    //public void PrintPickedNodes()
    //{
    //	string nodes = string.Empty;
    //	foreach (var node in PassiveIds)
    //	{
    //		nodes += node + ", ";
    //	}

    //	Debug.Log("Picked nodes: " + nodes);
    //}

    private TMP_Text FindTextObjectByName(string objectName)
    {
        var tmpTexts = Resources.FindObjectsOfTypeAll<TMP_Text>();
        foreach (var text in tmpTexts)
        {
            if (text.name == objectName)
            {
                return text;
            }
        }
        return null;
    }
    #endregion

    #region Unity Methods

    void Start()
    {
        // Finds sceneChanger if loses reference
        //if (sceneChanger != null)
        //{
        //	GameObject sceneManager = GameObject.FindGameObjectsWithTag("Manager").Where(g => g.name == "GameManager").FirstOrDefault();
        //	sceneChanger = sceneManager.GetComponent<SceneChanger>();
        //}

        SetUp();
    }

    private void Update()
    {
        hpSlider.value = currentHealth;
        mpSlider.value = currentMana;
        expSlider.value = currentExp;

        // Changes hero material back to normal color through time
        //if (currentTint.a > 0)
        //{
        //	currentTint.a = Mathf.Clamp01(currentTint.a - tintFadeSpeed * Time.deltaTime);
        //	spriteMaterial.SetColor("_Tint", currentTint);
        //}

        // Check if can level up
        if (currentExp >= expNeededToLevelUp)
        {
            LevelUpPlayer();
        }

        // Check if dying
        if (currentHealth <= 0)
        {
            Debug.Log("Player died!");
        }

        if (spellPointsText == null)
            spellPointsText = FindTextObjectByName("LeftSpellPointsText");

        if (passivePointsText == null)
            passivePointsText = FindTextObjectByName("LeftPassivePointsText");


        if (resetStats)
        {
            ResetStats();
            resetStats = false;
        }

        if (printPassvieNodesIds)
        {
            //PrintPickedNodes();
            printPassvieNodesIds = false;
        }



    }

    private void FixedUpdate()
    {
        CurrentHealth += RegenHealth.CalculatedValue / fixedUpdateRate;
        CurrentMana += RegenMana.CalculatedValue / fixedUpdateRate;
    }
    #endregion
}
