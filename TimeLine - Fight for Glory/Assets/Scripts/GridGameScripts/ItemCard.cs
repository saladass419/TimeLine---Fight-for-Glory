using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(menuName = "ItemCard")]
public class ItemCard : Card
{
    private GameObject prefabObject;

    public GameObject PrefabObject { get => prefabObject; set => prefabObject = value; }

    public void Equip()
    {

    }
}
