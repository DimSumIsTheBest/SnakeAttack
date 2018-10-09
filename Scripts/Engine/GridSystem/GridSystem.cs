﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviourSingleton
{
    static GridSystem Instance
    {
        get
        {
            return GetInstance<GridSystem>();
        }
    }

    const int width = 100;
    GridNode[,] gridNodes = new GridNode[width, width];

    private void Awake()
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < width; y++)
            {
                GridNode node = new GameObject("GridNode(" + x + "," + y + ")").AddComponent<GridNode>();
                node.transform.position = new Vector3(x, y);
                gridNodes[x, y] = node;
            }
        }
        GridNode.AutoLink(gridNodes);
    }

    public static GridNode GetNode(int x, int y)
    {
        try
        {
            return Instance.gridNodes[x, y];
        }
        catch
        {
            return null;
        }
    }
}