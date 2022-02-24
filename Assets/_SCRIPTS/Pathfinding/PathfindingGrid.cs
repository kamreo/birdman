using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingGrid
{

    private const int straightCost = 10;
    private const int diagonalCost = 14;

    private Vector2Int offset = Vector2Int.zero;

    public PathfindingNode[,] pathfindingNodes;
    private List<PathfindingNode> openPathfindingNodes;
    private List<PathfindingNode> closedPathfindingNodes;

    public PathfindingGrid(Vector2Int offset, Vector2Int size)
    {
        this.offset = offset;
        pathfindingNodes = new PathfindingNode[size.x, size.y];
        for (int y = 0; y < pathfindingNodes.GetLength(1); y++)
        {
            for (int x = 0; x < pathfindingNodes.GetLength(0); x++)
            {
                pathfindingNodes[x, y] = new PathfindingNode(new Vector2Int(x, y));
                pathfindingNodes[x, y].gCost = int.MaxValue;
                pathfindingNodes[x, y].CalculateFCost();
            }
        }

        openPathfindingNodes = new List<PathfindingNode>();
        closedPathfindingNodes = new List<PathfindingNode>();
    }

    private PathfindingNode FindPathfindingNode(Vector2Int position) => pathfindingNodes[position.x, position.y];

    public List<PathfindingNode> Path(Vector2Int from, Vector2Int to)
    {
        PathfindingNode fromNode = FindPathfindingNode(from);
        PathfindingNode toNode = FindPathfindingNode(to);

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

            foreach (PathfindingNode neighbourNode in GetNeighbours(currentNode))
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

    private List<PathfindingNode> GetNeighbours(PathfindingNode pathfindingNode)
    {
        List<PathfindingNode> neighboursNodes = new List<PathfindingNode>();
        for (int y = pathfindingNode.position.y - 1 - offset.y; y <= pathfindingNode.position.y + 1 - offset.y; y++)
        {
            if (y < 0 || y >= pathfindingNodes.GetLength(1))
                continue;
            for (int x = pathfindingNode.position.x - 1 - offset.x; x <= pathfindingNode.position.x + 1 - offset.x; x++)
            {
                if (x < 0 || x >= pathfindingNodes.GetLength(0))
                    continue;
                neighboursNodes.Add(FindPathfindingNode(new Vector2Int(x, y)));
            }
        }
        return neighboursNodes;
    }

    private List<PathfindingNode> CalculatePath(PathfindingNode toNode)
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

    private int CalculateDistanceCost(PathfindingNode fromNode, PathfindingNode toNode)
    {
        int x = Mathf.Abs(toNode.position.x - fromNode.position.x);
        int y = Mathf.Abs(toNode.position.y - fromNode.position.y);
        int remaining = Mathf.Abs(x - y);
        return diagonalCost * Mathf.Min(x, y) + straightCost * remaining;
    }

    private PathfindingNode GetLowestFCost(List<PathfindingNode> pathfindingNodes)
    {
        PathfindingNode pathfindingNode = pathfindingNodes[0];
        for (int index = 0; index < pathfindingNodes.Count; index++)
            if (pathfindingNodes[index].fCost < pathfindingNode.fCost)
                pathfindingNode = pathfindingNodes[index];
        return pathfindingNode;
    }


}