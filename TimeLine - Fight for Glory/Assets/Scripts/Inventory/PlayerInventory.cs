using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerInventory : GenericInventory
{
    private void Awake()
    {
        inventoryType = InventoryType.PlayerInventory;
        if(maxSlot==0) maxSlot = 24;
    }
    private void Start()
    {
        SetStartingItemsFromInspector();
        FindObjectOfType<Item>().itemPickUpEventInventory += AddItemToInventory;

        AddItemToInventory(Database.instance.ItemObjects[2], 2);
    }
}
