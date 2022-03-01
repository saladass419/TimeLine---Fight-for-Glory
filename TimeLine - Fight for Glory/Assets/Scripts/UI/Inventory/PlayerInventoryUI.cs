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
                    StartCoroutine(WaitForValue());
                    break;
                case InventoryType.PlayerEquipment:
                    PlayerEquipmentInventory equipment = (PlayerEquipmentInventory)DestinationInventory.GetComponent<GenericInventoryUI>().Inventory;

                    equipment.EquipItem(ItemBeingDragged.Item);

                    DestroyItemBeingDragged();
                    break;
                default:
                    DestroyItemBeingDragged();
                    break;
            }
        }
    }
    public IEnumerator WaitForValue()
    {
        ChestInventory chest = (ChestInventory)DestinationInventory.GetComponent<GenericInventoryUI>().Inventory;
        PlayerInventory player = (PlayerInventory)StartInventory.GetComponent<GenericInventoryUI>().Inventory;

        Slider.gameObject.SetActive(true);
        Slider.SetBasics(ItemBeingDragged.Amount);
        yield return new WaitUntil(() => SliderUI.isValueSet);

        ItemAmount = SliderUI.value;
        if (chest.AddItemToInventory(ItemBeingDragged.Item, ItemAmount))
            player.RemoveItemFromInventory(ItemBeingDragged.Item, ItemAmount);

        DestroyItemBeingDragged();
    }
}
