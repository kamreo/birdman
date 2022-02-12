using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[Range(1f, 10f)]
	public float Speed = 5f;

	private Rigidbody2D rb;
	private Vector2 movement;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");
	}

	void FixedUpdate()
	{
		rb.MovePosition(rb.position + movement * Speed * Time.fixedDeltaTime);
	}
}
