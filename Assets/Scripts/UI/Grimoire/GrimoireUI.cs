using UnityEngine;

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

