using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemInUI : MonoBehaviour
{
    [SerializeField] private GenericItemObject item;
    [SerializeField] private int amount;

    public GenericItemObject Item { get => item; set => item = value; }
    public int Amount { get => amount; set => amount = value; }
}
