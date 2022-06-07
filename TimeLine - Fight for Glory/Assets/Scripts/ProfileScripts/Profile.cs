using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Profile : MonoBehaviour
{
    [SerializeField] private string userName = "KisFaszos";

    private List<Card> cardCollection = new List<Card>();
    private Deck deck = new Deck();
    private int heroEssence;
    private int heroTypeEssence;
    private int attackTypeEssence;
    private int rangeTypeEssence;


    private Profile instanceOfProfile;

    private void Awake()
    {
        instanceOfProfile = this;
    }

    public void AddCardToCollection(Card card)
    {
        cardCollection.Add(card);
        deck.CardsInDeck.Add(card);
    }

    public string UserName { get => userName; set => userName = value; }
    public Deck Deck { get => deck; set => deck = value; }
}
