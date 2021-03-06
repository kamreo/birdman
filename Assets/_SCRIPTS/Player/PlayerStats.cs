using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour, IBaseStats, IPlayerStats
{
    private const int fixedUpdateRate = 50;         // Value needed to correctly apply regeneration

    PlayerCombatStats combatStats;
    AttributesHudHandler attributesHudHandler;

    [Header("General")]
    [SerializeField] string playerName;

    [Header("Bools")]
    [SerializeField] bool resetStats = false;
    [SerializeField] bool printPassvieNodesIds = false;

    #region BaseStats

    [Header("Dexterity")]
    [SerializeField]
    private int baseDexterity;
    [SerializeField]
    private Stat dexterity;
    public Stat Dexterity
    {
        get => dexterity;
        set
        {
            dexterity = value;
            combatStats.AttackSpeed = new Stat(dexterity.CalculatedValue * AttackSpeedPerDexterity);
            combatStats.Evasion = new Stat(dexterity.CalculatedValue * EvasionPerDexterity);
            var propName = MethodBase.GetCurrentMethod().Name.Substring(4);
            attributesHudHandler.ChangeAttributeValueText(propName, dexterity.CalculatedValue);
        }
    }
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
    public Stat Intelligence
    {
        get => intelligence;
        set
        {
            intelligence = value;
            MaxMana = new Stat(MaxMana.BaseValue, intelligence.CalculatedValue * MaxManaPerIntelligence, MaxMana.ItemIncome, MaxMana.PercentIncome);
            combatStats.SpellAmplification = new Stat(intelligence.CalculatedValue * SpellAmplificationPerIntelligence);
            var propName = MethodBase.GetCurrentMethod().Name.Substring(4);
            attributesHudHandler.ChangeAttributeValueText(propName, intelligence.CalculatedValue);
        }
    }
    [SerializeField]
    private int maxManaPerIntelligence;
    public int MaxManaPerIntelligence { get => maxManaPerIntelligence; }
    [SerializeField]
    private int spellAmplificationPerIntelligence;
    public int SpellAmplificationPerIntelligence { get => spellAmplificationPerIntelligence; }

    [Header("Strength")]
    [SerializeField]
    private int baseStrength;
    [SerializeField]
    private Stat strength;
    public Stat Strength
    {
        get => strength;
        set
        {
            strength = value;
            MaxHealth = new Stat(MaxHealth.BaseValue, strength.CalculatedValue * MaxHealthPerStrength, MaxHealth.ItemIncome, MaxHealth.PercentIncome);
            combatStats.AdditiveDamage = new Stat(strength.CalculatedValue * PhysicalAttackDamagePerStrength);
            var propName = MethodBase.GetCurrentMethod().Name.Substring(4);
            attributesHudHandler.ChangeAttributeValueText(propName, strength.CalculatedValue);
        }
    }
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

    public void ModifyPlayerStat(string statName)
    {
        if (attributesPoints <= 0)
        {
            Debug.LogWarning("THERE IS 0 ATTRIBUTES POINTS LEFT!!!");
            return;
        }

        switch (statName)
        {
            case "Dexterity":
                {
                    Debug.Log("Plus 1 dex");
                    Dexterity += new Stat(1);
                    break;
                }
            case "Intelligence":
                {
                    Intelligence += new Stat(1);
                    break;
                }
            case "Strength":
                {
                    Strength += new Stat(1);
                    break;
                }
            default:
                {
                    Debug.LogError($"Stat name:{statName} not found!!!!");
                    break;
                }
        }

        attributesPoints--;
        attributesPointsText.SetText($"{attributesPoints}");

        if (attributesPoints <= 0)
        {
            attributesPanelButton.SetActive(false);

            foreach (var button in attributesButtons)
            {
                button.SetActive(false);
            }
        }
    }

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
            bool maxHp = false;
            if (currentHealth == maxHealth.CalculatedValue)
            {
                maxHp = true;
            }
            maxHealth = value;
            hpSlider.maxValue = MaxHealth.CalculatedValue;

            if (maxHp)
            {
                currentHealth = maxHealth.CalculatedValue;
                hpSlider.value = currentHealth;
            }

            var propName = MethodBase.GetCurrentMethod().Name.Substring(4);
            attributesHudHandler.ChangeAttributeValueText(propName, maxHealth.CalculatedValue);
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
            var propName = MethodBase.GetCurrentMethod().Name.Substring(4);
            attributesHudHandler.ChangeAttributeValueText(propName, currentHealth);
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
            bool maxMp = false;
            if (currentMana == maxMana.CalculatedValue)
            {
                maxMp = true;
            }
            maxMana = value;
            mpSlider.maxValue = maxMana.CalculatedValue;
            if (maxMp)
            {
                currentMana = maxMana.CalculatedValue;
                mpSlider.value = currentMana;
            }
            var propName = MethodBase.GetCurrentMethod().Name.Substring(4);
            attributesHudHandler.ChangeAttributeValueText(propName, maxMana.CalculatedValue);
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

            var propName = MethodBase.GetCurrentMethod().Name.Substring(4);
            attributesHudHandler.ChangeAttributeValueText(propName, currentMana);
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
    [SerializeField] Slider experienceSlider;
    [Space]
    [SerializeField] int experienceNeededToLevelUp = 600;
    [SerializeField] int currentExperience = 0;
    [Space]
    [SerializeField] int level = 1;
    [SerializeField] TMP_Text levelText;
    [Space]
    [SerializeField] int spellPoints = 1;
    [SerializeField] TMP_Text spellPointsText;
    [Space]
    [SerializeField] int attributesPoints = 1;
    [SerializeField] TMP_Text attributesPointsText;

    public int CurrentExperience
    {
        get => currentExperience;
        set
        {
            if (value > 0)
            {
                currentExperience = value;
            }
            var propName = MethodBase.GetCurrentMethod().Name.Substring(4);
            attributesHudHandler.ChangeAttributeValueText(propName, currentExperience);
        }
    }
    public int ExperienceNeededToLevelUp
    {
        get => experienceNeededToLevelUp;
        set
        {
            if (value > 0)
            {
                experienceNeededToLevelUp = value;
            }
            var propName = MethodBase.GetCurrentMethod().Name.Substring(4);
            attributesHudHandler.ChangeAttributeValueText(propName, experienceNeededToLevelUp);
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
            var propName = MethodBase.GetCurrentMethod().Name.Substring(4);
            attributesHudHandler.ChangeAttributeValueText(propName, level);
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

    public int AttributesPoints
    {
        get => attributesPoints;
        set
        {
            if (value >= 0)
            {
                attributesPoints = value;
                var propName = MethodBase.GetCurrentMethod().Name.Substring(4);
                attributesHudHandler.ChangeAttributeValueText(propName, attributesPoints);
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
        experienceNeededToLevelUp = (int)Mathf.Floor(a * Level * Level + b * Level + c);
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

    [Header("AttributesPanel")]
    [SerializeField]
    private GameObject attributesPanelButton;
    [SerializeField]
    private TMP_Text dexterityText;
    [SerializeField]
    private TMP_Text intelligenceText;
    [SerializeField]
    private TMP_Text strengthText;

    [SerializeField]
    private GameObject[] attributesButtons;

    #endregion

    #region Methods
    void LevelUp()
    {
        Level++;
        SpellPoints++;
        AttributesPoints += 3;

        currentExperience -= experienceNeededToLevelUp;
        CalcNeededExperienceToLevelUp();
        attributesPanelButton.SetActive(true);
    }

    void UpdateHUD()
    {
        experienceSlider.maxValue = experienceNeededToLevelUp;
        hpSlider.maxValue = MaxHealth.CalculatedValue;
        mpSlider.maxValue = MaxMana.CalculatedValue;
        levelText.text = level.ToString();

        if (spellPointsText != null)
            spellPointsText.text = spellPoints.ToString();

        if (attributesPointsText != null)
            attributesPointsText.text = attributesPoints.ToString();
    }

    public void SetUp()
    {
        Dexterity = new Stat(baseDexterity);
        Intelligence = new Stat(baseIntelligence);
        Strength = new Stat(baseStrength);

        dexterityText.SetText($"{Dexterity.CalculatedValue}");
        intelligenceText.SetText($"{Intelligence.CalculatedValue}");
        strengthText.SetText($"{Strength.CalculatedValue}");

        //maxHealth.CalculateValue();
        MaxHealth = new Stat(maxHealth);
        hpSlider.minValue = 0;
        hpSlider.maxValue = MaxHealth.CalculatedValue;
        currentHealth = MaxHealth.CalculatedValue;
        hpSlider.value = currentHealth;

        //maxMana.CalculateValue();
        MaxMana = new Stat(maxMana);
        mpSlider.minValue = 0;
        mpSlider.maxValue = MaxMana.CalculatedValue;
        currentMana = MaxMana.CalculatedValue;
        mpSlider.value = currentMana;

        CalcNeededExperienceToLevelUp();
        experienceSlider.minValue = 0;
        experienceSlider.maxValue = experienceNeededToLevelUp;
        experienceSlider.value = currentExperience;

        levelText.text = level.ToString();
        if (spellPointsText != null)
            spellPointsText.text = spellPoints.ToString();
        if (attributesPointsText != null)
            attributesPointsText.text = attributesPoints.ToString();

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
        AttributesPoints = Level;
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
        combatStats = transform.parent.GetComponentInChildren<PlayerCombatStats>();
        attributesHudHandler = transform.parent.GetComponentInChildren<AttributesHudHandler>();

        attributesHudHandler.ChangeAttributeValueText("Name", playerName);
        attributesHudHandler.ChangeAttributeValueText("Level", level);
        attributesHudHandler.ChangeAttributeValueText("CurrentExperience", currentExperience);
        attributesHudHandler.ChangeAttributeValueText("ExperienceNeededToLevelUp", experienceNeededToLevelUp);
        attributesHudHandler.ChangeAttributeValueText("Dexterity", dexterity.CalculatedValue);
        attributesHudHandler.ChangeAttributeValueText("Intelligence", intelligence.CalculatedValue);
        attributesHudHandler.ChangeAttributeValueText("Strength", strength.CalculatedValue);
        attributesHudHandler.ChangeAttributeValueText("LeftAttributes", AttributesPoints);


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
        experienceSlider.value = currentExperience;

        // Changes hero material back to normal color through time
        //if (currentTint.a > 0)
        //{
        //	currentTint.a = Mathf.Clamp01(currentTint.a - tintFadeSpeed * Time.deltaTime);
        //	spriteMaterial.SetColor("_Tint", currentTint);
        //}

        // Check if can level up
        if (CurrentExperience >= ExperienceNeededToLevelUp)
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

        if (attributesPointsText == null)
            attributesPointsText = FindTextObjectByName("LeftAttributesPointsText");


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
