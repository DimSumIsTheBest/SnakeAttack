﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridNode : MonoBehaviour, IEnumerable<GridNode>
{
    public GridNode left, right, top, bottom;

    private List<GridTransform> connectedTransforms = new List<GridTransform>();

    /// <summary>
    /// Create a new gameobject, and add a grid node component to it.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static GridNode Create(Vector3 position, string name = "GridNode")
    {
        return new GameObject(name).AddComponent<GridNode>().Init(position);
    }

    private GridNode Init(Vector3 position)
    {
        transform.position = position;
        return this;
    }

    public GridNode GetFromDirection(Direction direction)
    {
        switch(direction)
        {
            case Direction.left:
                return left;
            case Direction.right:
                return right;
            case Direction.up:
                return top;
            case Direction.down:
                return bottom;
            default:
                return null;
        }
    }

    /// <summary>
    /// Gets a grid space from a direction, up, down, left of right
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="recoverFromError">if true, and the direction can't be recognized, then it will return null</param>
    /// <returns></returns>
    public GridNode GetFromDirection(Vector2 direction, bool recoverFromError = false)
    {
        if(direction == Vector2.left)
            return GetFromDirection(Direction.left);
        else if(direction == Vector2.right)
            return GetFromDirection(Direction.right);
        else if(direction == Vector2.up)
            return GetFromDirection(Direction.up);
        else if(direction == Vector2.down)
            return GetFromDirection(Direction.down);
        else if(recoverFromError)
            return null;
        else
            throw new System.Exception("Unrecognized direction. Directions can only be, left, right, up, or down");
    }

    /// <summary>
    /// Check if this node is adjacent to the other node
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool IsAdjacent(GridNode other)
    {
        return other != null && (left == other || right == other || top == other || bottom == other);
    }

    /// <summary>
    /// Connect a grid transform to this node, and execute it's events
    /// </summary>
    /// <param name="transform"></param>
    public void AddTransform(GridTransform transform)
    {
        foreach(var t in connectedTransforms)
        {
            t.Events.OnCollision(transform);
        }
        connectedTransforms.Add(transform);
    }

    /// <summary>
    /// Disconnect the grid trasnform from this node, and execute any events
    /// </summary>
    /// <param name="transform"></param>
    public void RemoveTransform(GridTransform transform)
    {
        connectedTransforms.Remove(transform);
    }

    /// <summary>
    /// Returns a list of transforms connected to this one
    /// </summary>
    /// <returns></returns>
    public IEnumerator<GridNode> GetEnumerator()
    {
        if(left != null)
            yield return left;
        if(right != null)
            yield return right;
        if(top != null)
            yield return top;
        if(bottom != null)
            yield return bottom;
    }

    /// <summary>
    /// Returns a list of transforms connected to this one
    /// </summary>
    /// <returns></returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        if(left != null)
            yield return left;
        if(right != null)
            yield return right;
        if(top != null)
            yield return top;
        if(bottom != null)
            yield return bottom;
    }
    
    /// <summary>
    /// Pass in an array of nodes to automatically link them
    /// </summary>
    /// <param name="nodes"></param>
    public static void AutoLink(GridNode[,] nodes)
    {
        for(int x = 0; x < nodes.GetLength(0); x++)
        {
            for(int y = 0; y < nodes.GetLength(1); y++)
            {
                GridNode node = nodes[x, y];
                if(node == null)
                    continue;
                try
                {
                    nodes[x - 1, y].right = node;
                    node.left = nodes[x - 1, y];
                }
                catch
                {

                }
                try
                {
                    nodes[x + 1, y].left = node;
                    node.right= nodes[x + 1, y];
                }
                catch
                {

                }
                try
                {
                    nodes[x, y - 1].top = node;
                    node.bottom = nodes[x, y - 1];
                }
                catch
                {

                }
                try
                {
                    nodes[x, y + 1].bottom = node;
                    node.top = nodes[x, y + 1];
                }
                catch
                {

                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 ll = transform.position - (Vector3)Vector2.one * 0.5f;
        Vector3 lr = ll + Vector3.right;
        Vector3 ul = ll + Vector3.up;
        Vector3 ur = lr + Vector3.up;
        Gizmos.DrawLine(ll, lr);
        Gizmos.DrawLine(ll, ul);
        Gizmos.DrawLine(ul, ur);
        Gizmos.DrawLine(lr, ur);
    }
}