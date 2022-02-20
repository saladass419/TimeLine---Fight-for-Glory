using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : MonoBehaviour
{
    [SerializeField] private GenericItemObject itemObject;
    [SerializeField] private int amount;
    public GenericItemObject ItemObject { get => itemObject; set => itemObject = value; }
    public int Amount { get => amount; set => amount = value; }

    private void OnValidate()
    {
        if (amount < 1) amount = 1;
    }
    public event Func<GenericItemObject, int, bool> itemPickUpEventInventory;
    public void ItemPickUp()
    {
        if(itemPickUpEventInventory != null)
        {
            itemPickUpEventInventory(itemObject, amount);
        }
    }
}
