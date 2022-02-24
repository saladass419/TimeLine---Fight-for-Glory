using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GenericInventory : MonoBehaviour
{
    public InventoryType inventoryType;
    public int maxSlot;

    public Dictionary<GenericItemObject, int> inventory = new Dictionary<GenericItemObject, int>();

    [SerializeField] public List<ShowItemsInInspector> inventoryItems = new List<ShowItemsInInspector>();

    public event Action InventoryChanged;
    public void RefreshInspector()
    {
        inventoryItems.Clear();
        foreach (var item in inventory)
        {
            ShowItemsInInspector newItem = new ShowItemsInInspector();
            newItem.Item = item.Key;
            newItem.Amount = item.Value;
            inventoryItems.Add(newItem);
        }
    }
    public bool AddItemToInventory(GenericItemObject item, int amount)
    {
        int putInAmount = IsEnoughSpaceInInventory(item, amount);
        if (putInAmount<1) return false;

        if (inventory.ContainsKey(item)) inventory[item] += putInAmount;
        else inventory.Add(item, putInAmount);

        RefreshInspector();
        if (InventoryChanged != null)
        {
            InventoryChanged();
        }
        return true;
    }
    public bool RemoveItemFromInventory(GenericItemObject item, int amount)
    {
        if (IsEnougItemsInInventory(item, amount))
        {
            inventory[item] -= amount;
            if (!IsEnougItemsInInventory(item, 1)) inventory.Remove(item);

            RefreshInspector();
            if (InventoryChanged != null)
            {
                InventoryChanged();
            }
            return true;
        }
        return false;
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
                return _amount;
            }
        }
        return 0;
    }
    public bool IsEnougItemsInInventory(GenericItemObject item, int amountNeeded)
    {
        if (inventory.ContainsKey(item) && inventory[item] >= amountNeeded)
            return true;
        else 
            return false;
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
