using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimoireUI : MonoBehaviour
{
    [SerializeField] private GameObject grimoirePanel;

    public void ToggleGrimoire()
    {
        grimoirePanel.SetActive(!grimoirePanel.activeSelf);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            ToggleGrimoire();
    }
}
