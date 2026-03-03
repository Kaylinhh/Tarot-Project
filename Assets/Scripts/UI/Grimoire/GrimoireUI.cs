using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GrimoireUI : MonoBehaviour
{
    [SerializeField] private GameObject grimoireOverlay;

    public void ToggleGrimoire()
    {
        grimoireOverlay.SetActive(!grimoireOverlay.activeSelf);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            ToggleGrimoire();
    }
}

