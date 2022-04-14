using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class GenericInventoryUI: MonoBehaviour,IBeginDragHandler , IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject[] slots;
    private GenericInventory inventory;

    private GameObject startInventory;
    private GameObject destinationInventory;

    private InventoryType inventoryType;
    private InventoryType startType;
    private InventoryType destType;

    private GameObject objectHoveredOver;
    private GameObject objectBeingDragged;
    private ItemInUI itemBeingDragged;

    [SerializeField] private SliderUI slider;
    [SerializeField] private GameObject informationTab;
    private int itemAmount;

    public GenericInventory Inventory { get => inventory; set => inventory = value; }
    public GameObject StartInventory { get => startInventory; set => startInventory = value; }
    public GameObject DestinationInventory { get => destinationInventory; set => destinationInventory = value; }
    public GameObject ObjectHoveredOver { get => objectHoveredOver; set => objectHoveredOver = value; }
    public GameObject ObjectBeingDragged { get => objectBeingDragged; set => objectBeingDragged = value; }
    public InventoryType InventoryType { get => inventoryType; set => inventoryType = value; }
    public ItemInUI ItemBeingDragged { get => itemBeingDragged; set => itemBeingDragged = value; }
    public InventoryType StartType { get => startType; set => startType = value; }
    public InventoryType DestType { get => destType; set => destType = value; }
    public SliderUI Slider { get => slider; set => slider = value; }
    public int ItemAmount { get => itemAmount; set => itemAmount = value; }

    private void Start()
    {
        GenericInventory[] inventoriesInScene = FindObjectsOfType<GenericInventory>();
        for (int i = 0; i < inventoriesInScene.Length; i++)
        {
            inventoriesInScene[i].InventoryChanged += RefreshInventory;
        }
        GetSlots();
        RefreshInventory();
    }
    private void GetSlots()
    {
        slots = new GameObject[this.transform.childCount];
        for (int i = 0; i < this.transform.childCount; i++)
        {
            slots[i] = this.transform.GetChild(i).gameObject;
        }
    }
    public void RefreshInventory()
    {
        GetSlots();
        int i = 0;
        foreach (var item in Inventory.inventory)
        {
            if (!item.Key.IsStackable)
            {
                for (int j = 0; j < item.Value; j++)
                {
                    Image[] images = slots[i].GetComponentsInChildren<Image>();
                    slots[i].GetComponentInChildren<ItemInUI>().Item = item.Key;
                    slots[i].GetComponentInChildren<ItemInUI>().Amount = 1;
                    images[1].sprite = item.Key.BackGround;
                    images[2].sprite = item.Key.ItemSprite;
                    slots[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
                    i++;
                }
            }
            else
            {
                Image[] images = slots[i].GetComponentsInChildren<Image>();
                slots[i].GetComponentInChildren<ItemInUI>().Item = item.Key;
                slots[i].GetComponentInChildren<ItemInUI>().Amount = item.Value;
                images[1].sprite = item.Key.BackGround;
                images[2].sprite = item.Key.ItemSprite;
                slots[i].GetComponentInChildren<TextMeshProUGUI>().text = item.Value.ToString();
                i++;
            }
        }
        for (int j = i; j < slots.Length; j++)
        {
            Image[] images = slots[j].GetComponentsInChildren<Image>();

            images[1].sprite = null;

            slots[j].GetComponentInChildren<ItemInUI>().Item = null;

            images[2].sprite = null;
            slots[j].GetComponentInChildren<TextMeshProUGUI>().text = null;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (objectHoveredOver!=null&&objectHoveredOver.GetComponent<ItemInUI>().Item != null)
        {
            objectBeingDragged = CreateTempItem(objectHoveredOver);
            itemBeingDragged = objectBeingDragged.GetComponent<ItemInUI>();

            StartInventory = eventData.hovered.Find(a => a.CompareTag("InventoryItems"));
            StartType = startInventory.GetComponent<GenericInventoryUI>().InventoryType;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (ObjectBeingDragged != null)
        {
            ObjectBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        DestinationInventory = eventData.hovered.Find(a => a.CompareTag("InventoryItems"));
        if (objectBeingDragged != null) objectBeingDragged.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        if (destinationInventory != null) DestType = DestinationInventory.GetComponent<GenericInventoryUI>().InventoryType;
        else DestroyItemBeingDragged();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ObjectHoveredOver = eventData.hovered.Find(a => a.CompareTag("InventoryItemPrefab"));
        if (ObjectHoveredOver == null) return;
        if (ObjectHoveredOver.GetComponent<ItemInUI>().Item != null && ObjectBeingDragged == null)
            UIManager.instance.SetInformation(ObjectHoveredOver); 
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        ObjectHoveredOver = null;
        UIManager.instance.SetInformation(null);
    }
    public void DestroyItemBeingDragged()
    {
        if(ObjectBeingDragged!=null)
            Destroy(ObjectBeingDragged);

        ItemBeingDragged = null;
        ObjectBeingDragged = null;
        DestinationInventory = null;
        StartInventory = null;
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
        image.transform.localScale *= 2;

        var item = tempItem.AddComponent<ItemInUI>();
        item.Item = obj.GetComponent<ItemInUI>().Item;
        item.Amount = obj.GetComponent<ItemInUI>().Amount;

        return tempItem;
    }
}
