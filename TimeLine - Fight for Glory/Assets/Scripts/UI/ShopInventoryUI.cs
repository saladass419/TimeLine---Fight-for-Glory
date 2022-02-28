using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ShopInventoryUI : GenericInventoryUI
{
    void Awake()
    {
        InventoryType = InventoryType.ShopInventory;
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        if (ObjectBeingDragged != null && DestinationInventory != null)
        {
            switch (DestType)
            {
                case InventoryType.PlayerInventory:
                    ShopInventory shop = (ShopInventory)StartInventory.GetComponent<GenericInventoryUI>().Inventory;
                    PlayerInventory player = (PlayerInventory)DestinationInventory.GetComponent<GenericInventoryUI>().Inventory;

                    shop.PurchaseItem(ItemBeingDragged.Item, 1, player.gameObject);
                    break;
                default:
                    break;
            }
        }

        DestroyItemBeingDragged();
        RefreshInventory();
    }
}
