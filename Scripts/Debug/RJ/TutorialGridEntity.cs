﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TutorialGridEntity : MonoBehaviour
{
    private GridTransform gridTransform;
    private List<GridNode> grid;

    private void Start()
    {
        //Create a grid using the method created in part 1
        //This will be available on github

        //Add a grid transform to the object for grid movement
        gridTransform = gameObject.AddComponent<GridTransform>();

        //Warp the grid transform to the first space on the grid (lower left corner)
        gridTransform.Warp(GridSystem.GetNode(0, 0));
    }

    private void Update()
    {
        //Get the node this object is currently on
        GridNode currentNode = gridTransform.CurrentNode;

        //Get the input, and move the object if an input is pressed
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            //move right
            gridTransform.Move(Direction.right);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //move left
            gridTransform.Move(Direction.left);
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            //move up
            gridTransform.Move(Direction.up);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            //move down
            gridTransform.Move(Direction.down);
        }

        //Move the 3d position to the grid positions
        transform.position = gridTransform.Target;
    }
}