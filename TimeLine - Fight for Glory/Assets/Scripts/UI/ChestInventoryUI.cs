using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ChestInventoryUI : GenericInventoryUI
{
    void Awake()
    {
        InventoryType = InventoryType.ChestInventory;
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        if (ObjectBeingDragged != null && DestinationInventory != null)
        {
            switch (DestType)
            {
                case InventoryType.PlayerInventory:
                    PlayerInventory player = (PlayerInventory)DestinationInventory.GetComponent<GenericInventoryUI>().Inventory;
                    ChestInventory chest = (ChestInventory)StartInventory.GetComponent<GenericInventoryUI>().Inventory;

                    if (player.AddItemToInventory(ItemBeingDragged.Item, ItemBeingDragged.Amount))
                        chest.RemoveItemFromInventory(ItemBeingDragged.Item, ItemBeingDragged.Amount);
                    break;
                default:
                    break;
            }
        }

        DestroyItemBeingDragged();
        RefreshInventory();
    }
}
