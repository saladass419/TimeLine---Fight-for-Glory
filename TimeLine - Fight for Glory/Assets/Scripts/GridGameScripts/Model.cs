using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{
    private (int PosX, int PosY) position;

    public (int PosX, int PosY) Position { get => position; set => position = value; }

}
