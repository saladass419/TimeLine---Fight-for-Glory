using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    [SerializeField] private HeroCard currentCreatureOnTile;
    [SerializeField] private GameObject currentHeroModelOnTile;
    [SerializeField] private (int PosX, int PosY) position;

    public (int PosX, int PosY) Position { get => position; set => position = value; }
    public HeroCard CurrentCreatureOnTile { get => currentCreatureOnTile; set => currentCreatureOnTile = value; }
    public GameObject CurrentHeroModelOnTile { get => currentHeroModelOnTile; set => currentHeroModelOnTile = value; }

    public void PlaceMonster(HeroCard heroCard) 
    {
        if(currentCreatureOnTile == null)
        {
            heroCard.InstantiatedModel.GetComponent<Model>().Position = position;
            currentCreatureOnTile = heroCard;
            currentHeroModelOnTile = heroCard.InstantiatedModel;
        }
    }

    public void MoveMonsterToTile(HeroCard hero)
    {
        currentCreatureOnTile = hero;
        currentHeroModelOnTile = hero.InstantiatedModel;
    }


    public void VanishMonster(Tile _newTile)
    {
        Debug.Log(position.PosX);
        Debug.Log(position.PosY);
        currentHeroModelOnTile.transform.position = _newTile.transform.position;
        currentHeroModelOnTile.GetComponent<Model>().Position = _newTile.Position;
        currentCreatureOnTile.Position = _newTile.Position;
        currentCreatureOnTile = null;
        currentHeroModelOnTile = null;
    }
}
