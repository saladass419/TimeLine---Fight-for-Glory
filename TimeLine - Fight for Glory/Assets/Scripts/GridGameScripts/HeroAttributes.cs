using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroAttributeType { DAMAGE, MAGICDAMAGE, HEALTH, BASICDEFENSE, MAGICDENFESE, ACTIONPOINT, MOVEMENT, }

public class HeroAttributes
{
    private Dictionary<HeroAttributeType, float> heroAttributes = new Dictionary<HeroAttributeType, float>();
}
