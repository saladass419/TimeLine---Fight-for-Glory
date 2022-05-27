using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.EventSystems;

public class PlayerInventory : GenericInventory
{
    private EquipmentInventory equipmentInventory;
    public EquipmentInventory EquipmentInventory { get => equipmentInventory; set => equipmentInventory = value; }
    private void Awake()
    {
        inventoryType = InventoryType.PlayerInventory;
        if(maxSlot==0) maxSlot = 24;
        equipmentInventory = gameObject.GetComponent<EquipmentInventory>();
    }
    private void Start()
    {
        SetStartingItemsFromInspector();
    }
    public void SubscribeToItems(Item newItem)
    {
        newItem.itemPickUpEventInventory += AddItemToInventory;
    }
}
