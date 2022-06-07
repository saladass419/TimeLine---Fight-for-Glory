using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemCard : Card
{

    public ItemCard()
    {
        CardName = this.GetType().Name;
        CardType = CardType.ITEM;
    }

    public virtual void Equip()
    {
        ;
    }
}
