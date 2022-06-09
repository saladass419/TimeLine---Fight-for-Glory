using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Profile : MonoBehaviour
{
    [SerializeField] private string userName = "KisFaszos";

    private List<GameObject> cardCollection = new List<GameObject>();
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

    public void AddGameObjectToCollection(GameObject gameObject)
    {
        cardCollection.Add(gameObject);
        deck.CardsInDeck.Add(gameObject);
    }

    public string UserName { get => userName; set => userName = value; }
    public Deck Deck { get => deck; set => deck = value; }
}
