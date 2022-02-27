using UnityEngine;

public class PlayerGodMode : MonoBehaviour
{
	PlayerStats player;

	//[SerializeField] SceneChanger sceneChanger;     // Calls "LoadGameOver" scene when player die
	[Header("God mode")]
	[SerializeField] bool unlimitedHP;              // Give player max health every update
	[SerializeField] bool unlimitedMP;              // Give player max mana every update
	[SerializeField] bool noCooldowns;              // Removes cooldowns
	[Tooltip("Player will stay alive at 1HP")]
	[SerializeField] bool cantDie;                  // Player never dies (stays at 1hp)

	public bool NoCooldowns { get => noCooldowns; }

	void Start()
	{
		player = transform.parent.GetComponentInChildren<PlayerStats>();
	}

	private void Update()
	{
		if (cantDie && player.CurrentHealth <= 0)
		{
			player.CurrentHealth = 1;
		}

		// Cheats
		if (unlimitedHP)
		{
			player.CurrentHealth = player.MaxHealth.CalculatedValue;
		}

		if (unlimitedMP)
		{
			player.CurrentMana = player.MaxMana.CalculatedValue;
		}
	}
}
