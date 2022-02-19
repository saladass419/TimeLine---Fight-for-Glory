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
        StaticImportantFunction.Shuffle(possibleItems);
    }
    public void FillChest()
    {

    }
}
