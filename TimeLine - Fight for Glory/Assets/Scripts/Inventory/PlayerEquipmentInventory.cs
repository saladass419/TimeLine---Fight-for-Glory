using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class PlayerEquipmentInventory : GenericInventory
{
    public event Action<AttributeName, float> itemEquipped;
    private PlayerInventory playerInventory;
    public PlayerInventory PlayerInventory { get => playerInventory; set => playerInventory = value; }

    private void Awake()
    {
        inventoryType = InventoryType.PlayerEquipment;
        if (maxSlot == 0) maxSlot = 6;
        playerInventory = gameObject.GetComponent<PlayerInventory>();
    }
    private void Start()
    {
        PlayerInventory = gameObject.GetComponent<PlayerInventory>();

        SetStartingEquipmentsFromInspector();
    }
    public void EquipItem(GenericItemObject item)
    {
        if (!item.itemTypes.Contains(ItemType.Equipment)&&item.EquipmentType==EquipmentType.None) return;
        foreach (var key in inventory.Keys)
        {
            if (key.EquipmentType == item.EquipmentType) return;
        }
        if (!PlayerInventory.RemoveItemFromInventory(item, 1)) return;
        AddItemToInventory(item,1);

        if (itemEquipped != null)
        {
            foreach (Attribute attribute in item.Attributes)
            {
                itemEquipped(attribute.AttributeName, attribute.Value);
            }
        }
    }
    public void UnequipItem(GenericItemObject item)
    {
        if (!PlayerInventory.AddItemToInventory(item, 1)) return;
        RemoveItemFromInventory(item,1);
        if (itemEquipped != null)
        {
            foreach (Attribute attribute in item.Attributes)
            {
                itemEquipped(attribute.AttributeName, -1 * attribute.Value);
            }
        }
    }
    private void SetStartingEquipmentsFromInspector()
    {
        foreach (GenericItemObject item in inventory.Keys)
        {
            foreach (Attribute attribute in item.Attributes)
            {
                itemEquipped(attribute.AttributeName, attribute.Value);
            }
        }
    }
}
