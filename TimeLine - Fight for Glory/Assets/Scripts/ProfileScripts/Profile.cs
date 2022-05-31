using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Profile : MonoBehaviour
{
    [SerializeField] private string userName = "KisFaszos";
    [SerializeField] private int score;
    private List<Card> cardCollection = new List<Card>();
    [SerializeField] private Deck deck;
    private Profile instanceOfProfile;

    private void Awake()
    {
        instanceOfProfile = this;
        PopulateList();
    }

    private void PopulateList()
    {
        deck = new Deck(); 
        //string[] assetNames = AssetDatabase.FindAssets("", new[] { "Assets/ScriptableObjects/Cards" });
        //foreach (string SOName in assetNames)
        //{
        //    var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
        //    var card = AssetDatabase.LoadAssetAtPath<Card>(SOpath);
        //    deck.AddCard(card);
        //}
    }

    public void AddCard(Card card)
    {
        cardCollection.Add(card);
        deck.AddCardToDeck(card);
    }

    public string UserName { get => userName; set => userName = value; }
    public int Score { get => score; set => score = value; }
    public Deck Deck { get => deck; set => deck = value; }
}
