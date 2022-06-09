 using System;
using System.Collections.Generic;
using UnityEngine;


public enum HeroCardType { ORC, DWARF, FAIRY, GIANT, ONI, CENTAUR, GOBLIN, DRAGON}
public enum AttackType { FIRE, FREEZE, ELECTRIC, POISON}
public enum RangeType { SHORT, MEDIUM, LONG}


public class HeroCard : Card
{
    [SerializeField] private HeroCardType heroCardType;
    [SerializeField] private AttackType attackType;
    [SerializeField] private RangeType rangeType;

    [SerializeField] private float currentHealth;
    [SerializeField] private int currentActionPoints;
    [SerializeField] private HeroAttributes heroAttributes;

    [SerializeField] private List<ItemCard> items;

    [SerializeField] private List<Position> tilesToMove;
    [SerializeField] private List<Position> tilesToAttack;

    public event Action<float> OnHealthPercentageChanged;

    private void Awake()
    {
        OnHealthPercentageChanged = delegate { };
        tilesToMove = new List<Position>();
        tilesToAttack = new List<Position>();
        heroAttributes = new HeroAttributes();
    }


    public List<ItemCard> Items { get => items; set => items = value; }
    public RangeType RangeType { get => rangeType; set => rangeType = value; }
    public AttackType AttackType { get => attackType; set => attackType = value; }
    public HeroCardType HeroCardType { get => heroCardType; set => heroCardType = value; }
    public HeroAttributes HeroAttributes { get => heroAttributes; set => heroAttributes = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public List<Position> TilesToMove { get => tilesToMove; set => tilesToMove = value; }
    public List<Position> TilesToAttack { get => tilesToAttack; set => tilesToAttack = value; }

    public void ModifyHealth(float value)
    {
        currentHealth += value;
        float currentHealhtPercentage = currentHealth / heroAttributes.HeroAttributesList[HeroAttributeType.MAXHEALTH];
        OnHealthPercentageChanged(currentHealhtPercentage);
    }

    public virtual void Attack()
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

    public virtual void Ability1()
    {
        if (currentActionPoints > 0)
        {
            currentActionPoints--;
        }
    }

    public virtual void Ability2()
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
