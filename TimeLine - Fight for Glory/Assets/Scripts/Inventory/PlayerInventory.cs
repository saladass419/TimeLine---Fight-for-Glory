using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PlayerInventory : GenericInventory
{
    private List<Item> foundItems;

    private PlayerEquipmentInventory equipmentInventory;
    public PlayerEquipmentInventory EquipmentInventory { get => equipmentInventory; set => equipmentInventory = value; }

    private void Awake()
    {
        inventoryType = InventoryType.PlayerInventory;
        if(maxSlot==0) maxSlot = 24;
        equipmentInventory = gameObject.GetComponent<PlayerEquipmentInventory>();
    }
    private void Start()
    {
        foundItems = new List<Item>();
        SetStartingItemsFromInspector();
    }
    private void Update()
    {
        List<Item> tempItems = FindObjectsOfType<Item>().ToList();
        for (int i = 0; i < tempItems.Count; i++)
        {
            if (!foundItems.Contains(tempItems[i]))
            {
                tempItems[i].itemPickUpEventInventory += AddItemToInventory;
                foundItems.Add(tempItems[i]);
            }
        }
    }
}
