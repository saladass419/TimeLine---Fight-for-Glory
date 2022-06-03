using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardinalDirection {NORTH, SOUTH, WEST, EAST}

public enum RotateDirection {LEFT, RIGHT}


public class Model : MonoBehaviour
{
    private (int PosX, int PosY) position;
    [SerializeField] private HeroCard hero = new HeroCard();
    [SerializeField] private bool placed = false;
    private CardinalDirection direction;
    private bool isEnemy;

    public (int PosX, int PosY) Position { get => position; set => position = value; }
    public HeroCard Hero { get => hero; set => hero = value; }
    public bool Placed { get => placed; set => placed = value; }
    public CardinalDirection Direction { get => direction; set => direction = value; }
    public bool IsEnemy { get => isEnemy; set => isEnemy = value; }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            hero.ModifyHealth(-10);
        }
    }

    public void RotateModel(RotateDirection rotateDirection, float angel)
    {

        if(rotateDirection == RotateDirection.LEFT)
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
