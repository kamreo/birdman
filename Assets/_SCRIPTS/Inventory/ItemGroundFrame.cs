using UnityEngine;

public class ItemGroundFrame : MonoBehaviour
{
	Transform targetLocation;
	Rigidbody2D rb;
	[SerializeField]
	float speed = 0.5f;
	float originalSpeed = 0.5f;

	[SerializeField]
	float distanceToMoveSide;

	bool moved = false;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		targetLocation = transform.parent.transform;
	}

	void Update()
	{
		float distance = Vector3.Distance(transform.position, targetLocation.position);
		if (distance > 0.1f)
		{
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, targetLocation.position, step);
		}

		if (moved)
			return;

		if (distance > distanceToMoveSide)
		{
			moved = true;
			transform.position = new Vector3(transform.position.x + Random.Range(-1f, 1f), transform.position.y, transform.position.z);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("PickableItemFrame"))
		{
			SetSpeed();
		}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("PickableItemFrame"))
		{
			SetSpeed();
		}
	}

	private void SetSpeed()
	{
		speed = 0f;
		float distance = Vector3.Distance(transform.position, targetLocation.position);
		if (distance > 0.2f)
			speed = 0.01f;

		if (distance > 0.5f)
			speed = 0.03f;
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("PickableItemFrame"))
		{
			speed = originalSpeed;
			float distance = Vector3.Distance(transform.position, targetLocation.position);
			if (distance > 1f)
				speed = 5f;

			if (distance > 2f)
				speed = 10f;
		}
	}

	//void OnMouseDown()
	//{
	//	//var player = FindObjectOfType<PickingItems>().transform;
	//	//float radius = player.GetComponent<CircleCollider2D>().radius;

	//	//if (Vector2.Distance(player.position, transform.parent.position) <= radius)
	//	gameObject.transform.parent.GetComponent<Pickable>().PickItemByClick();

	//}
}
