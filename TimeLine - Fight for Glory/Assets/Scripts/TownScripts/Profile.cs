using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Profile : MonoBehaviour
{
    [SerializeField] private string userName = "KisFaszos";
    [SerializeField] private Town town;
    [SerializeField] private int score;
    private List<Card> cardCollection;
    private Deck deck;
    private Profile instanceOfProfile;


    private void Awake()
    {
        instanceOfProfile = this;
        PopulateList();
    }

    private void PopulateList()
    {
        deck = new Deck(); 
        string[] assetNames = AssetDatabase.FindAssets("", new[] { "Assets/ScriptableObjects/Cards" });
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var card = AssetDatabase.LoadAssetAtPath<Card>(SOpath);
            deck.AddCard(card);
        }
    }

    public void AddCard(Card card)
    {
        cardCollection.Add(card);
    }

    public string UserName { get => userName; set => userName = value; }
    public Town Town { get => town; set => town = value; }
    public int Score { get => score; set => score = value; }
    public Deck Deck { get => deck; set => deck = value; }
}
