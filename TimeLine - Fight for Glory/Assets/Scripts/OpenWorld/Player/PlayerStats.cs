using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PlayerStats : MonoBehaviour
{
    [SerializeField] private List<Attribute> attributes = new List<Attribute>();
    [SerializeField] private int currency;
    public int Currency { get => currency; set => currency = value; }

    private void Start()
    {
        FindObjectOfType<EquipmentInventory>().itemEquipped += ChangeAttributeValue;
    }
    public void ChangeAttributeValue(AttributeName _attribute, float value)
    {
        foreach (Attribute attribute in attributes)
        {
            if (attribute.AttributeName == _attribute)
            {
                attribute.ModifyValue(value);
                break;
            }
        }
    }
}