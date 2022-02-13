using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EntityMovement : MonoBehaviour
{
	[SerializeField, Range(0, 5)]
	private float moveSpeed = 1;
	public float MoveSpeed
	{
		get
		{
			return moveSpeed;
		}
		set
		{
			if (value > 0)
				moveSpeed = value;
		}
	}

	private Rigidbody2D rigidbody;
	[SerializeField]
	private bool sprint = false;
	[SerializeField, Range(1.0f, 2.0f)]
	private float sprintMultiplier = 1.0f;
	public float SprintMultiplier
	{
		get
		{
			return sprintMultiplier;
		}
		set
		{
			if (value > 1)
				sprintMultiplier = value;
		}
	}
	[SerializeField]
	private Vector2 move = Vector2.zero;
	[SerializeField]
	private Vector2 velocity = Vector2.zero;

	private void OnEnable()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		rigidbody.freezeRotation = true;
		rigidbody.drag = 10;
	}

	private void Update()
	{
		UpdateInput();
	}

	private void FixedUpdate()
	{
		UpdateRigidbody();
	}

	private void UpdateInput()
	{
		if (move.magnitude > 1)
			move = move.normalized;
	}

	private void UpdateRigidbody()
	{
		if (!rigidbody) return;
		velocity = move * moveSpeed * Time.fixedDeltaTime;
		if (sprint)
			velocity = velocity * sprintMultiplier;
		rigidbody.MovePosition(rigidbody.position + velocity);
	}

	public void SetMove(Vector2 move) => this.move = move;
	public void SetSprint(bool sprint) => this.sprint = sprint;
}
