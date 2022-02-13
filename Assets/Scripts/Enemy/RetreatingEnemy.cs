using UnityEngine;

public class RetreatingEnemy : MonoBehaviour
{
	public float speed;
	public float stoppingDistance;
	public float retreatDistance;

	[SerializeField]
	public Transform player;

	private void Update()
	{
		if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
		{
			transform.position = Vector2.MoveTowards(transform.position, player.position, speed);
		}
	}
}
