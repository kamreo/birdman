using UnityEngine;

public class EnemyMovementV2 : MonoBehaviour
{
    void Update()
    {
        if (GetComponent<Rigidbody2D>().velocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 0);
        }
        else if (GetComponent<Rigidbody2D>().velocity.x <= 0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 0);
        }
    }
}
