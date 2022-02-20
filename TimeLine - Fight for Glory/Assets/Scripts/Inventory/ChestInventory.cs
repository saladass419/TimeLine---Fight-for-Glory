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
    public void FillChest()
    {

    }
}
