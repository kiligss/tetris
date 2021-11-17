using System.Collections;
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
            //if not a valid move it will reverse the move
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);
            //moves the tetromino to the right when "D" Key is pressed
            if (!ValidMove())
                transform.position -= new Vector3(1, 0, 0);
            //if not a valid move it will reverse the move

            this.enabled = false;
            FindObjectOfType<SpawnTetromino>().NewTetromino();

        }

        else if (Input.GetKeyDown(KeyCode.W))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            //rotates the tetromino when "W" key is pressed
            if (!ValidMove())
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);

        }

        if (Time.time - previousTime > (Input.GetKey(KeyCode.S) ? fallTime / 10 : fallTime))
        //if "S" Key is pressed than it will be moved down faster if not it will inherit from "fallTime" and fall at the normal speed
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
                transform.position -= new Vector3(0, -1, 0);
            previousTime = Time.time;
        }

        bool ValidMove()
        {
            foreach (Transform children in transform)
            //it will check each "child" or each space on grid
            {
                int roundedX = Mathf.RoundToInt(children.transform.position.x);
                int roundedY = Mathf.RoundToInt(children.transform.position.y);
                //will round the position to a whole number

                if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
                //will see if the tetromino is withing the grid 
                {
                    return false;
                }

            }

            return true;
        }

    }
}