using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profile : MonoBehaviour
{
    [SerializeField] private string userName = "KisFaszos";
    [SerializeField] private Town town;
    [SerializeField] private int score;
    public string UserName { get => userName; set => userName = value; }
    public Town Town { get => town; set => town = value; }
    public int Score { get => score; set => score = value; }
}
