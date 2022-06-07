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

    [SerializeField] private float currentHealth;
    [SerializeField] private int currentActionPoints;
    [SerializeField] private HeroAttributes heroAttributes = new HeroAttributes();

    [SerializeField] private List<ItemCard> items;

    [SerializeField] private List<(int PositionX, int PositionY)> tilesToMove = new List<(int, int)>();
    [SerializeField] private List<(int PositionX, int PositionY)> tilesToAttack = new List<(int, int)>();

    public event Action<float> OnHealthPercentageChanged = delegate { };


    public HeroCard()
    {
        CardName = this.GetType().Name;
        CardType = CardType.HERO;
    }


    public List<ItemCard> Items { get => items; set => items = value; }
    public List<(int, int)> TilesToMove { get => tilesToMove; set => tilesToMove = value; }
    public List<(int, int)> TilesToAttack { get => tilesToAttack; set => tilesToAttack = value; }
    public RangeType RangeType { get => rangeType; set => rangeType = value; }
    public AttackType AttackType { get => attackType; set => attackType = value; }
    public HeroCardType HeroCardType { get => heroCardType; set => heroCardType = value; }
    public HeroAttributes HeroAttributes { get => heroAttributes; set => heroAttributes = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }


    public void ModifyHealth(float value)
    {
        currentHealth += value;
        float currentHealhtPercentage = currentHealth / heroAttributes.HeroAttributesList[HeroAttributeType.MAXHEALTH];
        OnHealthPercentageChanged(currentHealhtPercentage);
    }

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
