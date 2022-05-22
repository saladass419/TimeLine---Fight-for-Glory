using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private List<Tile> tiles;


    private void Awake()
    {
        tiles = new List<Tile>();
        GameObject[] objectTiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject _tileObject in objectTiles)
        {
            Tile tTile = _tileObject.GetComponent<Tile>();
            tiles.Add(tTile);
        }

        int i = 0;
        int j = 0;

        foreach(Tile _tiles in tiles)
        {
            _tiles.Position = (i,j);
            i++;
            
            if(i % 8 == 0)
            {
                j++;
            }
        }
    }

    public void AddMonsterToTile(HeroCard heroCard, int x, int y)
    {
        Tile chosenTile = findTile(x, y);
        chosenTile.PlaceMonster(heroCard);
    }



    public Tile findTile(int x, int y)
    {
        foreach(Tile _tile in tiles)
        {
            if(_tile.Position == (x,y))
            {
                return _tile;
            }
        }
        return null;
    }
}
