 using System;
using System.Collections.Generic;
using UnityEngine;


public enum HeroCardType { ORC, DWARF, FAIRY, GIANT, ONI, CENTAUR, GOBLIN}
public enum AttackType { FIRE, FREEZE, ELECTRIC, POISON}
public enum RangeType { SHORT, MEDIUM, LONG}


public class HeroCard : Card
{
    [SerializeField] private HeroCardType heroCardType;
    [SerializeField] private AttackType attackType;
    [SerializeField] private RangeType rangeType;


    [SerializeField] private GameObject modelPrefab;

    [SerializeField] private float currentHealth;
    [SerializeField] private int currentActionPoints;
    [SerializeField] private HeroAttributes heroAttributes;

    [SerializeField] private List<ItemCard> items;

    [SerializeField] private List<(int PositionX, int PositionY)> tilesToMove = new List<(int, int)>();
    [SerializeField] private List<(int PositionX, int PositionY)> tilesToAttack = new List<(int, int)>();


    public HeroCard()
    {
        CardName = "HeroCard";
        CardType = CardType.HERO;
    }


    public List<ItemCard> Items { get => items; set => items = value; }
    public GameObject ModelPrefab { get => modelPrefab; set => modelPrefab = value; }
    public List<(int, int)> TilesToMove { get => tilesToMove; set => tilesToMove = value; }
    public List<(int, int)> TilesToAttack { get => tilesToAttack; set => tilesToAttack = value; }
    public RangeType RangeType { get => rangeType; set => rangeType = value; }
    public AttackType AttackType { get => attackType; set => attackType = value; }
    public HeroCardType HeroCardType { get => heroCardType; set => heroCardType = value; }

    public void Attack()
    {
        if (currentActionPoints > 0)
        {
            currentActionPoints--;
        }
    }

    public void EquipItem(ItemCard item)
    {
        items.Add(item);        
    }

    public void Move()
    {
        if (currentActionPoints > 0)
        {
            currentActionPoints--;
        }
    }

    public void Ability1()
    {
        if (currentActionPoints > 0)
        {
            currentActionPoints--;
        }
    }

    public void Ability2()
    {
        if(currentActionPoints > 0)
        {
            currentActionPoints--;
        }
    }

    public void ResetActionPoints()
    {
        //currentActionPoints = maximumActionPoints;
    }
}
