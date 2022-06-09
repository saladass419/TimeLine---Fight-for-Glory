using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType { HERO, SPELL, ITEM, OBJECT};

public abstract class Card : MonoBehaviour
{
    [SerializeField] private int manaCost;
    [SerializeField] private string cardName;
    [SerializeField] private string description;
    [SerializeField] private CardType cardType;

    public void Start()
    {
        cardName = this.GetType().Name;
    }


    public int ManaCost { get => manaCost; set => manaCost = value; }
    public  string Description { get => description; set => description = value; }
    public string CardName { get => this.GetType().Name; set => cardName = value; }
    public CardType CardType { get => cardType; set => cardType = value; }
}
