using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private Tile[,] tileList = new Tile[8,8];

    public Tile[,] TileList { get => tileList; set => tileList = value; }
    public Material HightLightMaterial { get => hightLightMaterial; set => hightLightMaterial = value; }

    [SerializeField] private Material hightLightMaterial;
    [SerializeField] Material tile1Material;
    [SerializeField] Material tile2Material;

    private void Awake()
    {
        GameObject[] objectTiles = GameObject.FindGameObjectsWithTag("Tile");
        int n = 0;
        int m = 0;
        foreach (GameObject _tileObject in objectTiles)
        {
            Tile tTile = _tileObject.GetComponent<Tile>();
            tileList[n, m] = tTile;
            //tileList[n, m].Position.PosX = n;
            //tileList[n, m].Position.PosY = m;

            if (n % 2 == 0 && m % 2 == 0)
            {
                tileList[n, m].OwnMaterial = tile1Material;
            }
            else if (n % 2 == 1 && m % 2 == 1)
            {
                tileList[n, m].OwnMaterial = tile1Material;
            }
            else
            {
                tileList[n, m].OwnMaterial = tile2Material;
            }


            m++;

            if (m % 8 == 0)
            {
                n++;
                m = 0;
            }
        }
    }


    public void AddMonsterToTile(GameObject heroModel, int x, int y)
    {
        Tile chosenTile = FindTile(x, y);
        chosenTile.PlaceMonster(heroModel);
    }


    public void HighlightTiles(Tile _tile, Material changeMaterial)
    {
        _tile.GetComponent<Renderer>().material = changeMaterial;
    }

    public void ResetBoardMaterial(Material material1, Material material2)
    {
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                tileList[i, j].GetComponent<Renderer>().material = tileList[i, j].OwnMaterial;
            }
        }
    }

    public void UnHighlightTile(Tile _tile)
    {
        _tile.GetComponent<Renderer>().material = _tile.OwnMaterial;
    }

    public void MoveHeroFromTileToAnother(GameObject heroModel, Tile oldTile, Tile newTile)
    {
        oldTile.VanishMonster(newTile);
        newTile.MoveMonsterToTile(heroModel);
    }

    public (Tile tile, float distance) closestTileToObject(GameObject _object)
    {
        float minmimumDistance = 10000f;
        Tile closestTile = null;
        foreach(Tile _tile in tileList)
        {
            float distanceBetweenTileAndObject = Vector3.Distance(_tile.transform.position, _object.transform.position);
            if (distanceBetweenTileAndObject < minmimumDistance)
            {
                minmimumDistance = distanceBetweenTileAndObject;
                closestTile = _tile;
            }
        }
        return (closestTile, minmimumDistance);
    }

    public Tile FindTile(int x, int y)
    {
        foreach (Tile _tile in tileList)
        {
            if (_tile.Position.PosX == x && _tile.Position.PosY == y)
            {
                return _tile;
            }
        }
        return null;
    }

    public Position PositionInBoardCoordinate(CardinalDirection direction, Position heroPosition, Position localPosition)
    {
        Position position = new Position();
        if (direction == CardinalDirection.NORTH)
        {
            position.PosX = heroPosition.PosX + localPosition.PosX;
            position.PosY = heroPosition.PosY + localPosition.PosY;
            //newX = heroPosition.PosX + localPosition.PosX;
            //newY = heroPosition.PosY + localPosition.PosY;
        }
        else if(direction == CardinalDirection.SOUTH)
        {
            position.PosX = heroPosition.PosX - localPosition.PosX;
            position.PosY = heroPosition.PosY - localPosition.PosY;
            //newX = heroPosition.PosX - localPosition.PosX;
            //newY = heroPosition.PosY - localPosition.PosY;
        }
        else if(direction == CardinalDirection.WEST)
        {
            position.PosX = heroPosition.PosX + localPosition.PosY;
            position.PosY = heroPosition.PosY - localPosition.PosX;
            //newX = heroPosition.PosX + localPosition.PosY;
            //newY = heroPosition.PosY - localPosition.PosX;
        }
        else
        {
            position.PosX = heroPosition.PosX - localPosition.PosY;
            position.PosY = heroPosition.PosY + localPosition.PosX;
            //newX = heroPosition.PosX - localPosition.PosY;
            //newY = heroPosition.PosY + localPosition.PosX;
        }
        return position;
    }

    public List<Position> LocalHeroPositionsToBoardPositions(CardinalDirection direction, Position heroPosition, List<Position> listOfLocalPositions)
    {
        List<Position> boardCoordinates = new List<Position>();

        foreach(Position pos in listOfLocalPositions)
        {
            boardCoordinates.Add(PositionInBoardCoordinate(direction, heroPosition, pos));
        }
        return boardCoordinates;
    }

    public bool ChoosenTileInMovementRange(Tile tile, List<Position> positions)
    {
        foreach(Position position in positions)
        {
            if (tile.Position.PosX == position.PosX && tile.Position.PosY == position.PosY)
                return true;
        }
        return false;
    }
}
