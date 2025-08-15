using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimoireUI : MonoBehaviour
{
    
    [SerializeField] GameObject grimoirePanel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ToggleGrimoire()
    {
        bool isActive = grimoirePanel.activeSelf;
        grimoirePanel.SetActive(!isActive);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            ToggleGrimoire();
        }
    }
}
