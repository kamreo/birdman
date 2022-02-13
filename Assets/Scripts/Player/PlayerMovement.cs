using UnityEngine;

[RequireComponent(typeof(EntityMovement))]
public class PlayerMovement : MonoBehaviour
{
	private PlayerStats stats;
	private EntityMovement entityMovement;
	private bool sprint = false;
	private Vector2 move = Vector2.zero;

	private void OnEnable()
	{
		entityMovement = GetComponent<EntityMovement>();
		stats = GetComponent<PlayerStats>();

		entityMovement.MoveSpeed = stats.Speed;
		entityMovement.SprintMultiplier = stats.SprintMultiplier;
	}

	private void Update()
	{
		UpdateInput();
		UpdateSprite();
		UpdateEntityMovement();
	}

	private void UpdateInput()
	{
		sprint = Input.GetKey(KeyCode.LeftShift);
		move.x = Input.GetAxis("Horizontal");
		move.y = Input.GetAxis("Vertical");
	}

	private void UpdateEntityMovement()
	{
		if (!entityMovement)
			return;
		entityMovement.SetSprint(sprint);
		entityMovement.SetMove(move);
	}

	private void UpdateSprite()
	{
		if (move.x > 0)
			transform.localScale = new Vector3(1, 1, 0);
		if (move.x < 0)
			transform.localScale = new Vector3(-1, 1, 0);
	}
}
