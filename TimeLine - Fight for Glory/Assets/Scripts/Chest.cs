using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Chest : GenericInventory
{
    private List<GenericItemObject> possibleItems = new List<GenericItemObject>();
    private void Awake()
    {
        inventoryType = InventoryType.ChestInventory;
        maxSlot = 12;
    }
    private void Start()
    {
        foreach (GenericItemObject item in Database.instance.ItemObjects)
        {
            possibleItems.Add(item);
            Debug.Log(item.name);
        }
        //StaticImportantFunction.Shuffle(possibleItems);
        /*Debug.Log("SHUFFLING");
        foreach (GenericItemObject item in possibleItems)
        {
            Debug.Log(item.name);
        }*/
    }
    public void FillChest()
    {

    }
}
