using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    List<Card> cardsInDeck = new List<Card>();
    List<Card> cardsInHand = new List<Card>();
    List<Card> cardsInDiscardPile = new List<Card>();

    public List<Card> CardsInDeck { get => cardsInDeck; set => cardsInDeck = value; }


    public void AddCard(Card card)
    {
        cardsInDeck.Add(card);
    }

    public void DrawCard()
    {
        Card drawnCard = cardsInDeck[0];
        cardsInDeck.RemoveAt(0);
        cardsInHand.Add(drawnCard);
    }
}
