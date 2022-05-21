using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    Queue<Card> cardsInDeck;
    List<Card> cardsInHand;
    List<Card> cardsInDiscardPile;


    public void DrawCard()
    {
        Card drawnCard = cardsInDeck.Dequeue();
        cardsInHand.Add(drawnCard);
    }
}
