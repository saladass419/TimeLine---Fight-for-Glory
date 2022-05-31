using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCard : Card
{
    private GameObject prefabObject;
    public GameObject PrefabObject { get => prefabObject; set => prefabObject = value; }

    public ObjectCard()
    {
        CardName = "ObjectCard";
        CardType = CardType.OBJECT;
    }
}
