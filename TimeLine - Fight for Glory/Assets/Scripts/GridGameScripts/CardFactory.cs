using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFactory : MonoBehaviour
{
    private System.Random rnd = new System.Random();

    private void Start()
    {
        Card card = CreateCard();
    }

    private Card CreateCard()
    {
        int cardTypeNumber = rnd.Next(3);
        switch (cardTypeNumber)
        {
            case 0:
                HeroCard newHeroCard = new HeroCard();
                return newHeroCard;
            case 1:
                SpellCard newSpellCard = new SpellCard();
                return newSpellCard;
            case 2:
                ItemCard newItemCard = new ItemCard();
                return newItemCard;
            default:
                Debug.Log("Not a possible number");
                break;
        }
        return null;
    }
}
