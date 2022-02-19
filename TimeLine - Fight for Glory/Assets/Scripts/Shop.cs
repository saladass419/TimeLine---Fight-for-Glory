using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Shop : MonoBehaviour
{
    [SerializeField] private PlayerStats player;
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private int shopLevel;

    private List<GenericItemObject> supplies = new List<GenericItemObject>();
    private System.Random rnd = new System.Random();
    public void RefreshShop()
    {
        supplies.Clear();
        for (int i = 0; i < Database.instance.ItemObjects.Count; i++)
        {
            if (Database.instance.ItemObjects[i].itemTypes.Contains(ItemType.Shop)&&Database.instance.ItemObjects[i].ItemDimensionAvailable<=shopLevel) supplies.Add(Database.instance.ItemObjects[i]);
        }
        for (int i = 0; i < supplies.Count; i++)
        {
            if (rnd.Next(0, 100) > supplies[i].Rarity) supplies.Remove(supplies[i]);
        }
    }
    public void PurchaseItem(GenericItemObject item)
    {
        if(item.Price<=player.Currency)
        {
            if(!inventory.AddItemToInventory(item, 1)) return;
            player.Currency -= item.Price;
            supplies.RemoveAt(supplies.FindIndex(a => a.ItemId == item.ItemId));
        }
    }
    public void SuppliesOutInEditor()
    {
        foreach (GenericItemObject item in supplies)
        {
            Debug.Log(item.name + " "+ item.Price);
        }
    }
}
