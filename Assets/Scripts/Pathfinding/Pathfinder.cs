using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField, Range(1, 10)]
    private int gridSize = 5;
    private PathfindingNode[,] grid = new PathfindingNode[0, 0];
    private Vector2Int offset = Vector2Int.zero;

    [SerializeField]
    private List<PathfindingNode> path = new List<PathfindingNode>();

    [SerializeField]
    Transform target;

    private void OnEnable()
    {
        grid = new PathfindingNode[gridSize * 2 + 1, gridSize * 2 + 1];
    }

    private void FixedUpdate()
    {
        offset.x = Mathf.RoundToInt(transform.position.x);
        offset.y = Mathf.RoundToInt(transform.position.y);
        if (target)
        {
            Vector2Int pos = new Vector2Int(Mathf.RoundToInt(target.position.x), Mathf.RoundToInt(target.position.y));
            path = Pathfinding.Path(new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)), pos, offset, grid, gridSize);
        }
    }

    private void OnDrawGizmos()
    {
        if (grid != null)
        {
            Gizmos.color = Color.red;
            for (int y = 0; y < grid.GetLength(1); y++)
                for (int x = 0; x < grid.GetLength(0); x++)
                    Gizmos.DrawWireCube(new Vector3(x - gridSize + offset.x, y - gridSize + offset.y, 0), Vector3.one);
                    
        }
    }

}