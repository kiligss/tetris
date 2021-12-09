﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    private float previousTime;
    public float fallTime = 0.8f;
    public static int height = 20;
    public static int width = 10;
    // static is added as the dimensions of the grid can't be changed.
    public Vector3 rotationPoint;
    private static Transform[,] grid = new Transform[width, height];    
    //a new variable is added and this allows us to see if the 


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0);
            //moves the tetromino to the left when "A" Key is pressed
            if (!ValidMove())
                transform.position -= new Vector3(-1, 0, 0);
            //if  not a valid move it will reverse the move
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);
            //moves the tetromino to the right when "D" Key is pressed
            if (!ValidMove())
                transform.position -= new Vector3(1, 0, 0);
            //if not a valid move it will reverse the move

            //error which has been ammended
            //this.enabled = false;
            //FindObjectOfType<SpawnTetromino>().NewTetromino();

        }

        else if (Input.GetKeyDown(KeyCode.W))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            //rotates the tetromino when "W" key is pressed
            if (!ValidMove())
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);

        }

        if (Time.time - previousTime > (Input.GetKey(KeyCode.S) ? fallTime / 10 : fallTime))
        //if "S" Key is pressed than it will be moved down faster if not pressed it will inherit from "fallTime" and fall at the normal speed
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckForLines();

                this.enabled = false;
                FindObjectOfType<SpawnTetromino>().NewTetromino();
                //has accessed spawn script and has called 'SpawnTetromino'
            }
            previousTime = Time.time;
        }
    }

    bool ValidMove()    //function checks each square in the grid and rounds each movement to an int
    {
        foreach (Transform children in transform)
        //it will check each "child" or each space on grid
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            //will round the position to a whole number

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
            //will see if the tetromino is withinthe grid 
            {
                return false;
            }

            else
            {
                //left blank to minimise error
            }

            // will check if the space on the grid is free to put a tetromino in or not
            if (grid[roundedX, roundedY] != null)
            {
                return false;
            }

            else
            {
                //left blank to minimise error
            }
        }

        return true;
    }

    void AddToGrid()    //adds the tetrominoes permanantly to the grid, allowing them to detect if a tetromino is occupying the a space on the grid
    //used to see if tetrominoes are on the grid.
    {
        foreach (Transform children in transform)
        // will check every space in the grid and then add the values 
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;    //after checking the rounded values will be added into the grid array and children will be updated
       
        }

    }

    void CheckForLines()
    //will perform the line clear
    {
        for(int i = height-1; i >= 0; i--)
        // will check every position in the grid to see if a full line is there
        {
            if(HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
                //if line is found the line will be deleted then the rest of the elementsa above will move down
            }
        }
    }

    bool HasLine (int i)
    {
        for(int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
                return false;
            //checks to see if the line is there
        }

         return true;
    }

    void DeleteLine (int i)
    {
        for (int j = 0; j< width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
            //will destroy the line
        }
    }

    void RowDown (int i)
    // will move the row down
    {
        for (int y=i; y< height; y++)
        {
            for(int j = 0; j< width; j++)
            //both for loops go down and along checking each line
                if(grid[j,y]!= null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                    //moves the whole grid above the line down
                }
        }
    }
}

 