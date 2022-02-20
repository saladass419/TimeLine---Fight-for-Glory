using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ChestInventory : GenericInventory
{
    private List<GenericItemObject> possibleItems = new List<GenericItemObject>();

    private void Awake()
    {
        inventoryType = InventoryType.ChestInventory;
        if(maxSlot == 0) maxSlot = 12;
    }
    private void Start()
    {
        SetStartingItemsFromInspector();
        StaticImportantFunction.Shuffle(possibleItems);
    }
    private void FillChest()
    {

    }
    public bool PutItemsInChest(GenericInventory inventoryFrom, GenericItemObject item, int amount) // needs a interaction managers of some sort
    {
        if (IsEnoughSpaceInInventory(item,amount)==amount)
        {
            if (inventoryFrom.RemoveItemFromInventory(item, amount))
            {
                AddItemToInventory(item, amount);
                return true;
            }
        }
        return false;
    }
    public bool TakeItemsFronChest(GenericInventory inventoryTo, GenericItemObject item, int amount) // needs a interaction managers of some sort
    {
        if (inventoryTo.AddItemToInventory(item, amount))
        {
            RemoveItemFromInventory(item, amount);
            return true;
        }
        return false;
    }
}
