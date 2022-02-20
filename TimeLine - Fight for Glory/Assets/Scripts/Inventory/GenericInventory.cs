using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GenericInventory : MonoBehaviour
{
    public InventoryType inventoryType;
    public int maxSlot;

    public Dictionary<GenericItemObject, int> inventory = new Dictionary<GenericItemObject, int>();

    [SerializeField] private List<ShowItemsInInspector> inventoryItems = new List<ShowItemsInInspector>();

    private void AddToInspectorInventory(GenericItemObject item, int amount)
    {
        if(inventoryItems.Find(a=>a.Item == item) != null)
        {
            inventoryItems.Find(a => a.Item == item).Amount += amount;
        }
        else
        {
            ShowItemsInInspector itemToShow = new ShowItemsInInspector();
            itemToShow.Item = item;
            itemToShow.Amount = amount;
            inventoryItems.Add(itemToShow);
        }
    }
    private void RemoveFromInspectorInventory(GenericItemObject item, int amount)
    {
        int index = inventoryItems.FindIndex(a => a.Item == item);
        if (inventory.ContainsKey(item)) //needs to check inventory
        {
            inventoryItems[index].Amount -= amount;
        }
        else
        {
            inventoryItems.RemoveAt(index);
        }
    }
    public bool AddItemToInventory(GenericItemObject item, int amount)
    {
        int putInAmount = IsEnoughSpaceInInventory(item, amount);
        if (putInAmount<1) return false;
        if (inventory.ContainsKey(item)) inventory[item] += putInAmount;
        else inventory.Add(item, putInAmount);
        AddToInspectorInventory(item, putInAmount);
        return true;
    }
    public void RemoveItemFromInventory(GenericItemObject item, int amount)
    {
        inventory[item] -= amount;
        if (!IsEnougItemsInInventory(item, 1)) inventory.Remove(item);
        RemoveFromInspectorInventory(item, amount);
    }
    public int IsEnoughSpaceInInventory(GenericItemObject item, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (item.IsStackable)
            {
                if (inventory.Keys.Count() < maxSlot) return amount;
                if (inventory.ContainsKey(item)) return amount;
            }else if (!item.IsStackable)
            {
                int _amount = Mathf.Clamp(maxSlot - inventory.Keys.Count,0,amount);
                if(_amount<amount) Debug.Log("Not enough space in inventory!");
                return _amount;
            }
        }
        Debug.Log("Not enough space in inventory!");
        return 0;
    }
    public bool IsEnougItemsInInventory(GenericItemObject item, int amountNeeded)
    {
        if (!inventory.ContainsKey(item)) return false;
        else if (inventory[item] >= amountNeeded) return true;
        else return false;
    }
    public void SetStartingItemsFromInspector()
    {
        foreach (var item in inventoryItems)
        {
            inventory.Add(item.Item, item.Amount);
        }
    }
}
[System.Serializable]
public class ShowItemsInInspector
{
    [SerializeField] private GenericItemObject item;
    [SerializeField] private int amount;
    public GenericItemObject Item { get => item; set => item = value; }
    public int Amount { get => amount; set => amount = value; }
}
public enum InventoryType
{
    PlayerInventory, ChestInventory
}
