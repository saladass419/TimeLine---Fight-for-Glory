using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType { HERO, SPELL, ITEM};

public abstract class Card : ScriptableObject
{
    private int manaCost;
    private string cardName;
    private string description;
    private CardType cardType;

    public  int ManaCost { get => manaCost; set => manaCost = value; }
    public  string Description { get => description; set => description = value; }
    public string CardName { get => cardName; set => cardName = value; }
    public CardType CardType { get => cardType; set => cardType = value; }
}
