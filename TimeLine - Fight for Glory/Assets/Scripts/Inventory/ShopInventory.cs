using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ShopInventory : GenericInventory
{
    private System.Random rnd = new System.Random();

    [SerializeField] private int shopLevel;
    private void Start()
    {
        RefreshShop();
    }
    public void RefreshShop()
    {
        inventory.Clear();
        for (int i = 0; i < Database.instance.ItemObjects.Count; i++)
        {
            if (Database.instance.ItemObjects[i].itemTypes.Contains(ItemType.Shop)&&Database.instance.ItemObjects[i].ItemDimensionAvailable<=shopLevel) AddItemToInventory(Database.instance.ItemObjects[i],1);
        }
        for (int i = 0; i < inventory.Count; i++)
        {
            if (rnd.Next(0, 100) > inventory.ElementAt(i).Key.Rarity) RemoveItemFromInventory(inventory.ElementAt(i).Key,inventory.ElementAt(i).Value);
        }
    }
    public void PurchaseItem(GenericItemObject item, GameObject player)
    {
        if(item.Price<=player.GetComponent<PlayerStats>().Currency)
        {
            Debug.Log(item.Price);
            if(!player.GetComponent<PlayerInventory>().AddItemToInventory(item, 1)) return;

            player.GetComponent<PlayerStats>().Currency -= item.Price;

            RemoveItemFromInventory(item,1);
        }
    }
}
