using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroAttributeType { DAMAGE, MAGICDAMAGE, MAXHEALTH, BASICDEFENSE, MAGICDENFESE, ACTIONPOINT, MOVEMENT, }

public class HeroAttributes
{
    private Dictionary<HeroAttributeType, float> heroAttributesList = new Dictionary<HeroAttributeType, float>();

    public Dictionary<HeroAttributeType, float> HeroAttributesList { get => heroAttributesList; set => heroAttributesList = value; }
}
