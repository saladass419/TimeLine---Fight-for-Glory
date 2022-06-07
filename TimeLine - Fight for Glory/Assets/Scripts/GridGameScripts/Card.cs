using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType { HERO, SPELL, ITEM, OBJECT};

public abstract class Card
{
    private int manaCost;
    private string cardName;
    private string description;
    private CardType cardType;
    [SerializeField] private GameObject modelPrefab;

    public int ManaCost { get => manaCost; set => manaCost = value; }
    public  string Description { get => description; set => description = value; }
    public string CardName { get => cardName; set => cardName = value; }
    public CardType CardType { get => cardType; set => cardType = value; }
    public GameObject ModelPrefab { get => modelPrefab; set => modelPrefab = value; }
}
