using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    [SerializeField] private HeroCard currentCreatureOnTile;
    [SerializeField] private GameObject currentHeroModelOnTile;
    [SerializeField] private (int PosX, int PosY) position;
    [SerializeField] private Material ownMaterial;
    [SerializeField] private bool occupied = false;

    public (int PosX, int PosY) Position { get => position; set => position = value; }
    public HeroCard CurrentCreatureOnTile { get => currentCreatureOnTile; set => currentCreatureOnTile = value; }
    public GameObject CurrentHeroModelOnTile { get => currentHeroModelOnTile; set => currentHeroModelOnTile = value; }
    public Material OwnMaterial { get => ownMaterial; set => ownMaterial = value; }
    public bool Occupied { get => occupied; set => occupied = value; }

    public void PlaceMonster(HeroCard heroCard) 
    {
        if(occupied == false)
        {
            heroCard.InstantiatedModel.GetComponent<Model>().Position = position;
            currentCreatureOnTile = heroCard;
            currentHeroModelOnTile = heroCard.InstantiatedModel;
            occupied = true;
        }
    }

    public void MoveMonsterToTile(HeroCard hero)
    {
        occupied = true;
        currentCreatureOnTile = hero;
        currentHeroModelOnTile = hero.InstantiatedModel;
    }


    public void VanishMonster(Tile _newTile)
    {
        occupied = false;
        currentHeroModelOnTile.transform.position = _newTile.transform.position;
        currentHeroModelOnTile.GetComponent<Model>().Position = _newTile.Position;
        currentCreatureOnTile.Position = _newTile.Position;
        currentCreatureOnTile = null;
        currentHeroModelOnTile = null;
    }
}
