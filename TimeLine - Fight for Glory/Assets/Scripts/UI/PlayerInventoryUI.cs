using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerInventoryUI : GenericInventoryUI
{
    void Awake()
    {
        Inventory = FindObjectOfType<PlayerInventory>();
        InventoryType = InventoryType.PlayerInventory;
    }
    
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        if (ObjectBeingDragged != null && DestinationInventory != null)
        {
            switch (DestType)
            {
                case InventoryType.ChestInventory:
                    ChestInventory chest = (ChestInventory)DestinationInventory.GetComponent<GenericInventoryUI>().Inventory;
                    PlayerInventory player = (PlayerInventory)StartInventory.GetComponent<GenericInventoryUI>().Inventory;

                    if(chest.AddItemToInventory(ItemBeingDragged.Item,ItemBeingDragged.Amount))
                        player.RemoveItemFromInventory(ItemBeingDragged.Item, ItemBeingDragged.Amount);
                    break;
                case InventoryType.PlayerEquipment:
                    PlayerEquipmentInventory equipment = (PlayerEquipmentInventory)DestinationInventory.GetComponent<GenericInventoryUI>().Inventory;

                    equipment.EquipItem(ItemBeingDragged.Item);
                    break;
                default:
                    break;
            }
        }
        DestroyItemBeingDragged();
        RefreshInventory();  
    }
}
