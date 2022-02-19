using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerInventory : GenericInventory
{
    private void Awake()
    {
        inventoryType = InventoryType.PlayerInventory;
        maxSlot = 24;
    }
    private void Start()
    {
        FindObjectOfType<Item>().itemPickUpEventInventory += AddItemToInventory;
    }
    public void InventoryOutPutInEDitor()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            Debug.Log(inventory.ElementAt(i).Key + " " + inventory.ElementAt(i).Value);
        }
    }
}
