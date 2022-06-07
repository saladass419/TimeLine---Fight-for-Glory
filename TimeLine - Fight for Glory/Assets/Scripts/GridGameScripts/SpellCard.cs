using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpellCard : Card
{

    public SpellCard()
    {
        CardName = this.GetType().Name;
        CardType = CardType.SPELL;
    }
    public void Effect()
    {

    }
}
