using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{
    private (int PosX, int PosY) position;
    [SerializeField] private HeroCard hero;

    public (int PosX, int PosY) Position { get => position; set => position = value; }
    public HeroCard Hero { get => hero; set => hero = value; }
}
