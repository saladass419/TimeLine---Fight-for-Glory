using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.EventSystems;

public class PlayerInventory : GenericInventory
{
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
        SetStartingItemsFromInspector();
    }
    public void SubscribeToItems(Item newItem)
    {
        newItem.itemPickUpEventInventory += AddItemToInventory;
    }
}
