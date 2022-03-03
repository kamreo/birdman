using System;
using UnityEngine;

[RequireComponent(typeof(EntityMovement))]
public class PlayerMovement : MonoBehaviour
{
	private EntityMovement entityMovement;
	private bool sprint = false;
	private Vector2 move = Vector2.zero;
	[SerializeField] Transform hand;

	private void OnEnable()
	{
		entityMovement = GetComponent<EntityMovement>();
	}

	private void Update()
	{
		UpdateInput();
		UpdateSprite();
		UpdateEntityMovement();
		RotateHand();
	}

    private void RotateHand()
    {
		float angle = Utility.AngleTowardsMouse(hand.position);
		hand.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
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
			transform.localScale = new Vector3(-1, 1, 0);
		if (move.x < 0)
			transform.localScale = new Vector3(1, 1, 0);
	}
}
