using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class UIOpen : MonoBehaviour
{
    public static bool isAnythingOpen = false;
    [SerializeField] private CinemachineFreeLook freeLook;
    private void Update()
    {
        if (isAnythingOpen) freeLook.enabled = false;
        else freeLook.enabled = true;
    }
}
