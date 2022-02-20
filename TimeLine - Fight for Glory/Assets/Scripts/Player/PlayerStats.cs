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
        FindObjectOfType<PlayerInventory>().itemEquipped += ChangeAttributeValue;
    }
    public void ChangeAttributeValue(AttributeName attribute, float value)
    {
        foreach (Attribute item in attributes)
        {
            if (item.AttributeName == attribute)
            {
                item.ModifyValue(value);
                break;
            }
        }
    }
}