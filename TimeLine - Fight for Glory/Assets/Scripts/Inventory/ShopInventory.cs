using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShopInventory : GenericInventory
{
    [SerializeField] private int shopLevel;
    private GenericItemObject item;
    private void Start()
    {
        RefreshShop();
    }
    public void RefreshShop()
    {
        System.Random rnd = new System.Random();
        inventory.Clear();
        for (int i = 0; i < Database.instance.ItemObjects.Count; i++)
        {
            item = Database.instance.ItemObjects[i];
            if (item.ItemTypes.Contains(ItemType.Shop) && item.ItemDimensionAvailable <= shopLevel)
            {
                int amount;
                if (item.IsStackable)
                    amount = Mathf.RoundToInt(rnd.Next(1, 9) * item.Rarity / 100 + 0.5f);
                else
                    amount = Mathf.RoundToInt(rnd.Next(1, 3) * item.Rarity / 100 + 0.5f);
                AddItemToInventory(item, amount);
            }
        }
        for (int i = 0; i < inventory.Count; i++)
        {
            if (rnd.Next(0, 100) > inventory.ElementAt(i).Key.Rarity) RemoveItemFromInventory(inventory.ElementAt(i).Key,inventory.ElementAt(i).Value);
        }
    }
    public void PurchaseItem(GenericItemObject item, int amount, GameObject player)
    {
        if(item.Price*amount<=player.GetComponent<PlayerStats>().Currency)
        {
            if(!player.GetComponent<PlayerInventory>().AddItemToInventory(item, amount)) return;

            player.GetComponent<PlayerStats>().Currency -= item.Price*amount;

            Debug.Log(amount +" "+item.name+" for "+(item.Price*amount));
            RemoveItemFromInventory(item,amount);
        }
    }
}
