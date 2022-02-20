using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attribute
{
   [SerializeField] private AttributeName attributeName;
   [SerializeField] private float value;
    public AttributeName AttributeName { get => attributeName; set => attributeName = value; }
    public float Value { get => value; set => this.value = value; }
    public void ModifyValue(float amount)
    {
        Value += amount;
    }
}
public enum AttributeName
{
    Health,
    Damage,
    Speed
} 
