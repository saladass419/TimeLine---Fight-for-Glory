using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentInventoryUI : GenericInventoryUI
{
    void Awake()
    {
        Inventory = FindObjectOfType<PlayerEquipmentInventory>();
        InventoryType = InventoryType.PlayerEquipment;
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        if (ObjectBeingDragged != null && DestinationInventory != null)
        {
            switch (DestType)
            {
                case InventoryType.PlayerInventory:
                    PlayerEquipmentInventory equipment = (PlayerEquipmentInventory)StartInventory.GetComponent<GenericInventoryUI>().Inventory;

                    equipment.UnequipItem(ItemBeingDragged.Item);
                    break;
                default:
                    break;
            }
        }
        DestroyItemBeingDragged();
    }
}
