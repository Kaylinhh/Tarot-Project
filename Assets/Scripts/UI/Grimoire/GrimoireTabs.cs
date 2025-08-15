using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimoireTabs : MonoBehaviour
{
    [SerializeField] GameObject recipesTab;
    [SerializeField] GameObject statsTab;
    [SerializeField] GameObject socialTab;

    public void ShowRecipes()
    {
        recipesTab.SetActive(true);
        statsTab.SetActive(false);
        socialTab.SetActive(false);
    }

    public void ShowStats()
    {
        recipesTab.SetActive(false);
        statsTab.SetActive(true);
        socialTab.SetActive(false);
    }

    public void ShowSocial()
    {
        recipesTab.SetActive(false);
        statsTab.SetActive(false);
        socialTab.SetActive(true);
    }
}
}
