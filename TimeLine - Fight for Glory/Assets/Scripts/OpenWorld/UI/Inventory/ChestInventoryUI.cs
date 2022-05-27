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
                    StartCoroutine(WaitForValue());
                    break;
                default:
                    DestroyItemBeingDragged();
                    break;
            }
        }
    }
    public IEnumerator WaitForValue()
    {
        PlayerInventory player = (PlayerInventory)DestinationInventory.GetComponent<GenericInventoryUI>().Inventory;
        ChestInventory chest = (ChestInventory)StartInventory.GetComponent<GenericInventoryUI>().Inventory;

        Slider.gameObject.SetActive(true);
        Slider.SetBasics(ItemBeingDragged.Amount);

        yield return new WaitUntil(() => SliderUI.isValueSet);

        ItemAmount = SliderUI.value;
        if (player.AddItemToInventory(ItemBeingDragged.Item, ItemAmount))
            chest.RemoveItemFromInventory(ItemBeingDragged.Item, ItemAmount);

        DestroyItemBeingDragged();
    }
}
