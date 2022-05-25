using System;
using System.Collections.Generic;
using UnityEngine;


public enum HeroCardType { OGRE, DWARF, Fairy, Giant, Oni, Centaur, Werewolf, Unicorn}
public enum AttackType { FIRE, FREEZE, ELECTRIC, POISON}
public enum RangeType { SHORT, MEDIUM, LONG, }


[System.Serializable]
[CreateAssetMenu(menuName = "HeroCard")]
public class HeroCard : Card
{
    [SerializeField] private HeroCardType heroCardType;
    [SerializeField] private AttackType attackType;
    [SerializeField] private RangeType rangeType;


    [SerializeField] private GameObject modelPrefab;
    [SerializeField] private GameObject instantiatedModel;
    [SerializeField] private float maximumHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private float attack;
    [SerializeField] private int actionPoints;
    [SerializeField] private int maximumActionPoints;
    [SerializeField] private (int PositionX, int PositionY) position;
    [SerializeField] private List<ItemCard> items;

    [SerializeField] private List<(int PositionX, int PositionY)> tilesToMove = new List<(int, int)>();
    [SerializeField] private List<(int PositionX, int PositionY)> tileToAttack = new List<(int, int)>();


    public HeroCard()
    {
        ;
    }

    public HeroCard(HeroCardType heroCardType, AttackType attackType, RangeType rangeType)
    {
        ;
    }

    public (int, int) Position { get => position; set => position = value; }
    public List<ItemCard> Items { get => items; set => items = value; }
    public GameObject ModelPrefab { get => modelPrefab; set => modelPrefab = value; }
    public List<(int, int)> TilesToMove { get => tilesToMove; set => tilesToMove = value; }
    public List<(int, int)> TileToAttack { get => tileToAttack; set => tileToAttack = value; }
    public GameObject InstantiatedModel { get => instantiatedModel; set => instantiatedModel = value; }

    public void Attack()
    {
        if (actionPoints > 0)
        {
            actionPoints--;
        }
    }

    public void EquipItem(ItemCard item)
    {
        items.Add(item);        
    }

    public void Move()
    {
        if (actionPoints > 0)
        {
            actionPoints--;
        }
    }

    public void Ability1()
    {
        if (actionPoints > 0)
        {
            actionPoints--;
        }
    }

    public void Ability2()
    {
        if(actionPoints > 0)
        {
            actionPoints--;
        }
    }

    public void ResetActionPoints()
    {
        actionPoints = maximumActionPoints;
    }
}
