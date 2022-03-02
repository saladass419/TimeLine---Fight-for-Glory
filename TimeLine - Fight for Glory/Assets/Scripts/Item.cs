using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : MonoBehaviour
{
    public event Func<GenericItemObject, int, bool> itemPickUpEventInventory;

    [SerializeField] private GenericItemObject itemObject;
    [SerializeField] private int amount;
    public GenericItemObject ItemObject { get => itemObject; set => itemObject = value; }
    public int Amount { get => amount; set => amount = value; }
    private void OnValidate()
    {
        if (amount < 1) amount = 1;
    }
    private void Awake()
    {
        PlayerInventory[] players = FindObjectsOfType<PlayerInventory>();
        for (int i = 0; i < players.Length; i++)
        {
            players[i].SubscribeToItems(this.GetComponent<Item>());
        }
    }
    public void ItemPickUp()
    {
        if(itemPickUpEventInventory != null)
        {
            if(itemPickUpEventInventory(itemObject, amount)) 
                Destroy(gameObject);
        }
    }
}
