using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] private EnemyObject enemy;
    [SerializeField] private bool playerDetected = false;
    private void Detection()
    {
        
    }
    private void Movement()
    {

    }
    private void Update()
    {
        Detection();
        if (playerDetected) Movement();
    }
}
