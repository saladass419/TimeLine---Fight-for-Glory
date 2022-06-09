using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    [SerializeField] List<GameObject> cardsInDeck = new List<GameObject>();
    [SerializeField] List<GameObject> cardsInHand = new List<GameObject>();
    List<GameObject> cardsInDiscardPile = new List<GameObject>();

    public List<GameObject> CardsInDeck { get => cardsInDeck; set => cardsInDeck = value; }


    public void AddGameObjectToDeck(GameObject card)
    {
        cardsInDeck.Add(card);
    }

    public void DrawCard()
    {
        GameObject drawnGameObject = cardsInDeck[0];
        cardsInDeck.RemoveAt(0);
        cardsInHand.Add(drawnGameObject);
    }
}
