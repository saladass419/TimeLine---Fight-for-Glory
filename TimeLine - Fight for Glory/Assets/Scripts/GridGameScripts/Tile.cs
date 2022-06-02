using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    [SerializeField] private GameObject currentHeroModelOnTile;
    [SerializeField] private (int PosX, int PosY) position;
    [SerializeField] private Material ownMaterial;
    [SerializeField] private bool occupied = false;

    public (int PosX, int PosY) Position { get => position; set => position = value; }
    public GameObject CurrentHeroModelOnTile { get => currentHeroModelOnTile; set => currentHeroModelOnTile = value; }
    public Material OwnMaterial { get => ownMaterial; set => ownMaterial = value; }
    public bool Occupied { get => occupied; set => occupied = value; }

    public void PlaceMonster(GameObject heroModel) 
    {
        if(occupied == false)
        {
            currentHeroModelOnTile = heroModel;
            occupied = true;
        }
    }

    public void MoveMonsterToTile(GameObject heroModel)
    {
        occupied = true;
        currentHeroModelOnTile = heroModel;
    }


    public void VanishMonster(Tile _newTile)
    {
        occupied = false;
        currentHeroModelOnTile = null;
    }
}
