using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GenericInventory : MonoBehaviour
{
    public Dictionary<GenericItemObject, int> inventory = new Dictionary<GenericItemObject, int>();

    public InventoryType inventoryType;
    public int maxSlot;

    public bool AddItemToInventory(GenericItemObject item, int amount)
    {
        if (!IsEnoughSpaceInInventory(item)) return false;
        if (inventory.ContainsKey(item)) inventory[item] += amount;
        else inventory.Add(item, amount);
        return true;
    }
    public void RemoveItemFromInventory(GenericItemObject item, int amount)
    {
        inventory[item] -= amount;
        if (!IsEnougItemsInInventory(item, 1)) inventory.Remove(item);
    }
    public bool IsEnoughSpaceInInventory(GenericItemObject item)
    {
        if (inventory.Keys.Count() < maxSlot) return true;
        if (inventory.ContainsKey(item) && item.IsStackable) return true;
        return false;
    }
    public bool IsEnougItemsInInventory(GenericItemObject item, int amountNeeded)
    {
        if (!inventory.ContainsKey(item)) return false;
        else return true ? inventory[item] >= amountNeeded : false;
    }
}
public enum InventoryType
{
    PlayerInventory, ChestInventory
}
