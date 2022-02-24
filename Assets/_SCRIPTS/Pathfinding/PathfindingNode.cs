using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PathfindingNode
{

    public PathfindingNode(Vector2Int position)
    {
        this.position = position;
    }

    public Vector2Int position = Vector2Int.zero;

    public PathfindingNode fromNode = null;

    public int gCost = 0;
    public int hCost = 0;
    public int fCost = 0;

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

}
