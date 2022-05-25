using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private Tile[,] tileList = new Tile[8,8];

    public Tile[,] TileList { get => tileList; set => tileList = value; }


    private void Awake()
    {
        GameObject[] objectTiles = GameObject.FindGameObjectsWithTag("Tile");
        int n = 0;
        int m = 0;
        foreach (GameObject _tileObject in objectTiles)
        {
            Tile tTile = _tileObject.GetComponent<Tile>();
            tileList[n, m] = tTile;
            tTile.Position = (n, m);
            
            m++;

            if (m % 8 == 0)
            {
                n++;
                m = 0;
            }
        }
    }





    public void AddMonsterToTile(HeroCard heroCard, int x, int y)
    {
        GameObject instantiatedHeroPrefab = Instantiate(heroCard.ModelPrefab, transform.position, Quaternion.identity);
        heroCard.InstantiatedModel = instantiatedHeroPrefab;
        Debug.Log(heroCard.InstantiatedModel);
        Tile chosenTile = findTile(x, y);
        heroCard.InstantiatedModel.transform.position = chosenTile.transform.position;
        chosenTile.PlaceMonster(heroCard);
    }


    public (int,int) tellManhattanDistance(Tile tile1, Tile tile2)
    {
        (int x1, int y1) = tile1.Position;
        (int x2, int y2) = tile2.Position;
        return (Mathf.Abs(x1 - y1), Mathf.Abs(x2 - y2));
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
                if(i%2 == 0 && j%2 == 0)
                {
                    tileList[i, j].GetComponent<Renderer>().material = material1;
                }
                else if(i%2 == 1 && j%2 == 1)
                {
                    tileList[i, j].GetComponent<Renderer>().material = material1;
                }
                else
                {
                    tileList[i, j].GetComponent<Renderer>().material = material2;
                }
            }
        }
    }


    public void MoveHeroFromTileToAnother(HeroCard hero, Tile oldTile, Tile newTile)
    {
        oldTile.VanishMonster(newTile);
        newTile.MoveMonsterToTile(hero);
    }

    public Tile findTile(int x, int y)
    {
        foreach (Tile _tile in tileList)
        {
            if (_tile.Position == (x, y))
            {
                return _tile;
            }
        }
        return null;
    }
}
