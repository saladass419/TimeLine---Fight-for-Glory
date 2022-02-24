using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class InventoryUIManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject[] slots;
    [SerializeField] private GenericInventory inventory;

    private GameObject startInventory;
    private GameObject destinationInventory;

    private GameObject objectHoveredOver;
    private GameObject objectBeingDragged;
    public GenericInventory Inventory { get => inventory; set => inventory = value; }
    public void OpenInventory(GameObject inventoryToOpen)
    {
        Inventory = inventoryToOpen.GetComponent<PlayerInventory>();
    }
    private void Start()
    {
        FindObjectOfType<GenericInventory>().InventoryChanged += RefreshInventory;
        RefreshInventory();
    }
    private void Update()
    {
        RefreshInventory();
    }
    public void RefreshInventory()
    {
        int i = 0;
        foreach (var item in Inventory.inventory)
        {
            slots[i].GetComponentInChildren<ItemInUI>().Item = item.Key;
            slots[i].GetComponentInChildren<ItemInUI>().Amount = item.Value;

            slots[i].GetComponentInChildren<Image>().sprite = item.Key.ItemSprite;
            slots[i].GetComponentInChildren<TextMeshProUGUI>().text = item.Value.ToString();
            i++;
        }
        for (int j = i; j < slots.Length; j++)
        {
            slots[j].GetComponentInChildren<ItemInUI>().Item = null;

            slots[j].GetComponentInChildren<Image>().sprite = null;
            slots[j].GetComponentInChildren<TextMeshProUGUI>().text = null;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (objectHoveredOver!=null&&objectHoveredOver.GetComponent<ItemInUI>().Item != null)
        {
            objectBeingDragged = CreateTempItem(objectHoveredOver);
            startInventory = eventData.hovered.Find(a => a.CompareTag("InventoryItems"));
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (objectBeingDragged != null)
        {
            objectBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (objectBeingDragged != null)
        {
            destinationInventory = eventData.hovered.Find(a => a.CompareTag("InventoryItems"));

            destinationInventory.GetComponent<InventoryUIManager>().inventory.AddItemToInventory(objectBeingDragged.GetComponent<ItemInUI>().Item, objectBeingDragged.GetComponent<ItemInUI>().Amount);
            startInventory.GetComponent<InventoryUIManager>().inventory.RemoveItemFromInventory(objectBeingDragged.GetComponent<ItemInUI>().Item, objectBeingDragged.GetComponent<ItemInUI>().Amount);

            Destroy(objectBeingDragged);

            objectBeingDragged = null;
            destinationInventory = null;
            startInventory = null;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        objectHoveredOver = eventData.hovered.Find(a => a.CompareTag("InventoryItemPrefab"));
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        objectHoveredOver = null;
    }

    public GameObject CreateTempItem(GameObject obj)
    {
        GameObject tempItem = null;
        tempItem = new GameObject();

        var rt = tempItem.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector3(50, 50);
        tempItem.transform.SetParent(transform.parent.parent);

        var image = tempItem.AddComponent<Image>();
        image.sprite = obj.GetComponent<ItemInUI>().Item.ItemSprite;
        image.raycastTarget = false;

        var item = tempItem.AddComponent<ItemInUI>();
        item.Item = obj.GetComponent<ItemInUI>().Item;
        item.Amount = obj.GetComponent<ItemInUI>().Amount;

        return tempItem;
    }
}
