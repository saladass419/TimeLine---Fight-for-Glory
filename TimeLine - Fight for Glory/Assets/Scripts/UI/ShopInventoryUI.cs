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
                    StartCoroutine(WaitForValue());
                    break;
                default:
                    DestroyItemBeingDragged();
                    RefreshInventory();
                    break;
            }
        }
    }
    public IEnumerator WaitForValue()
    {
        ShopInventory shop = (ShopInventory)StartInventory.GetComponent<GenericInventoryUI>().Inventory;
        PlayerInventory player = (PlayerInventory)DestinationInventory.GetComponent<GenericInventoryUI>().Inventory;

        Slider.gameObject.SetActive(true);
        Slider.SetBasics(ItemBeingDragged.Amount);

        yield return new WaitUntil(() => SliderUI.isValueSet);

        ItemAmount = SliderUI.value;
        shop.PurchaseItem(ItemBeingDragged.Item, ItemAmount, player.gameObject);

        DestroyItemBeingDragged();
        RefreshInventory();
    }
}
