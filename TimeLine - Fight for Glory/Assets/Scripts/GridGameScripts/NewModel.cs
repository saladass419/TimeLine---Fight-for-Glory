using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardinalDirection { NORTH, SOUTH, WEST, EAST }
public enum RotateDirection { LEFT, RIGHT }


public class NewModel : MonoBehaviour
{
    [SerializeField] private string characterName;

    [SerializeField] private Position position;
    [SerializeField] private bool onBoard = false;
    [SerializeField] private CardinalDirection direction;
    [SerializeField] private bool isEnemy;


    public Position Position { get => position; set => position = value; }
    public bool OnBoard { get => onBoard; set => onBoard = value; }
    public CardinalDirection Direction { get => direction; set => direction = value; }
    public bool IsEnemy { get => isEnemy; set => isEnemy = value; }

    public void RotateModel(RotateDirection rotateDirection, float angel)
    {

        if (rotateDirection == RotateDirection.LEFT)
        {
            gameObject.transform.Rotate(Vector3.up, -angel);
        }
        else
        {
            gameObject.transform.Rotate(Vector3.up, angel);
        }

        if (angel == 180)
        {
            if (direction == CardinalDirection.NORTH)
            {
                direction = CardinalDirection.SOUTH;
            }
            else if (direction == CardinalDirection.SOUTH)
            {
                direction = CardinalDirection.NORTH;
            }
            else if (direction == CardinalDirection.WEST)
            {
                direction = CardinalDirection.EAST;
            }
            else
            {
                direction = CardinalDirection.WEST;
            }
        }
        else
        {
            if (rotateDirection == RotateDirection.LEFT)
            {
                if (direction == CardinalDirection.NORTH)
                {
                    direction = CardinalDirection.WEST;
                }
                else if (direction == CardinalDirection.WEST)
                {
                    direction = CardinalDirection.SOUTH;
                }
                else if (direction == CardinalDirection.SOUTH)
                {
                    direction = CardinalDirection.EAST;
                }
                else
                {
                    direction = CardinalDirection.NORTH;
                }
            }
            else
            {
                if (direction == CardinalDirection.NORTH)
                {
                    direction = CardinalDirection.EAST;
                }
                else if (direction == CardinalDirection.EAST)
                {
                    direction = CardinalDirection.SOUTH;
                }
                else if (direction == CardinalDirection.SOUTH)
                {
                    direction = CardinalDirection.WEST;
                }
                else
                {
                    direction = CardinalDirection.NORTH;
                }
            }
        }
    }



}



public struct Position {

    private int posX;
    private int posY;

    public Position(int x, int y)
    {
        posX = x;
        posY = y;
    }

    public int PosX { get => posX; set => posX = value; }
    public int PosY { get => posY; set => posY = value; }
}