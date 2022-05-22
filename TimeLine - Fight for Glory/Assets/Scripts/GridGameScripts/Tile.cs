using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    [SerializeField] private HeroCard currentCreatureOnTile;
    [SerializeField] private ItemCard currentObjectOnTile;
    [SerializeField] private (int, int) position;

    public (int, int) Position { get => position; set => position = value; }

    public void PlaceMonster(HeroCard heroCard) 
    {
        if(currentCreatureOnTile == null)
        {
            Instantiate(heroCard.ModelPrefab, transform.position, Quaternion.identity);
            currentCreatureOnTile = heroCard;
        }
    }


    public void PlaceObject(ItemCard itemCard)
    {
        if (currentObjectOnTile == null)
        {
            Instantiate(itemCard.PrefabObject, transform.position, Quaternion.identity);
            currentObjectOnTile = itemCard;
        }
    }

    public void VanishMonster()
    {
        currentCreatureOnTile = null;
    }
}
