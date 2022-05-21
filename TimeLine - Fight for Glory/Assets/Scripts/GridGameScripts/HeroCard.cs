using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroCardType { OGRE, DWARF, Fairy, Giant, Oni, Centaur, Werewolf, Unicorn}

[System.Serializable]
[CreateAssetMenu(menuName = "HeroCard")]
public class HeroCard : Card
{
    [SerializeField] private HeroCardType heroCardType;
    [SerializeField] private GameObject modelPrefab;
    [SerializeField] private float maximumHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private float attack;
    [SerializeField] private int actionPoints;
    [SerializeField] private int maximumActionPoints;
    [SerializeField] private (int, int) position;
    [SerializeField] private List<ItemCard> items;



    public (int, int) Position { get => position; set => position = value; }
    public List<ItemCard> Items { get => items; set => items = value; }
    public GameObject ModelPrefab { get => modelPrefab; set => modelPrefab = value; }

    public void Attack()
    {

    }

    public void EquipItem()
    {
        
    }

    public void Move()
    {

    }

    public void Ability1()
    {

    }

    public void Ability2()
    {

    }
}
