using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pathfinding
{
    private const int straightCost = 10;
    private const int diagonalCost = 14;

    public static List<PathfindingNode> Path(Vector2Int from, Vector2Int to, Vector2Int offset, PathfindingNode[,] grid, int gridSize)
    {
        List<PathfindingNode> openPathfindingNodes = new List<PathfindingNode>();
        List<PathfindingNode> closedPathfindingNodes = new List<PathfindingNode>();

        for (int y = 0; y < grid.GetLength(1); y++)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                grid[x, y] = new PathfindingNode(new Vector2Int(x, y));
                grid[x, y].gCost = int.MaxValue;
                grid[x, y].CalculateFCost();
            }
        }

        int toY = to.y + offset.y - gridSize;
        int toX = to.x + offset.x - gridSize;

        Debug.Log($"{toX} x {toY}");

        if (toX < 0 || toY < 0 || toX >= grid.GetLength(0) || toY >= grid.GetLength(1))
            return null;

        PathfindingNode fromNode = grid[from.x - offset.x, from.y - offset.y];
        PathfindingNode toNode = grid[toX, toY];


        fromNode.gCost = 0;
        fromNode.hCost = CalculateDistanceCost(fromNode, toNode);
        fromNode.CalculateFCost();

        while (openPathfindingNodes.Count > 0)
        {
            PathfindingNode currentNode = GetLowestFCost(openPathfindingNodes);
            if (currentNode == toNode)
                return CalculatePath(toNode);

            openPathfindingNodes.Remove(currentNode);
            closedPathfindingNodes.Add(currentNode);

            foreach (PathfindingNode neighbourNode in GetNeighbours(currentNode, grid, offset))
            {
                if (closedPathfindingNodes.Contains(neighbourNode))
                    continue;

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.fromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, toNode);
                    neighbourNode.CalculateFCost();

                    if (!openPathfindingNodes.Contains(neighbourNode))
                    {
                        openPathfindingNodes.Add(neighbourNode);
                    }
                }
            }
        }

        return null;
    }

    private static List<PathfindingNode> CalculatePath(PathfindingNode toNode)
    {
        List<PathfindingNode> path = new List<PathfindingNode>();
        path.Add(toNode);
        PathfindingNode currentNode = toNode;

        while (currentNode.fromNode != null)
        {
            path.Add(currentNode.fromNode);
            currentNode = currentNode.fromNode;
        }

        path.Reverse();

        return path;
    }

    private static PathfindingNode GetLowestFCost(List<PathfindingNode> pathfindingNodes)
    {
        PathfindingNode pathfindingNode = pathfindingNodes[0];
        for (int index = 0; index < pathfindingNodes.Count; index++)
            if (pathfindingNodes[index].fCost < pathfindingNode.fCost)
                pathfindingNode = pathfindingNodes[index];
        return pathfindingNode;
    }

    private static List<PathfindingNode> GetNeighbours(PathfindingNode pathfindingNode, PathfindingNode[,] grid, Vector2Int offset)
    {
        List<PathfindingNode> neighboursNodes = new List<PathfindingNode>();
        for (int y = pathfindingNode.position.y - 1 - offset.y; y <= pathfindingNode.position.y + 1 - offset.y; y++)
        {
            if (y < 0 || y >= grid.GetLength(1))
                continue;
            for (int x = pathfindingNode.position.x - 1 - offset.x; x <= pathfindingNode.position.x + 1 - offset.x; x++)
            {
                if (x < 0 || x >= grid.GetLength(0))
                    continue;
                neighboursNodes.Add(grid[x - offset.x, y - offset.y]);
            }
        }
        return neighboursNodes;
    }

    private static int CalculateDistanceCost(PathfindingNode fromNode, PathfindingNode toNode)
    {
        int x = Mathf.Abs(toNode.position.x - fromNode.position.x);
        int y = Mathf.Abs(toNode.position.y - fromNode.position.y);
        int remaining = Mathf.Abs(x - y);
        return diagonalCost * Mathf.Min(x, y) + straightCost * remaining;
    }

}