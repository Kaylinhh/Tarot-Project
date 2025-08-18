using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimoireTabsUI : MonoBehaviour
{
    [Header("Tab Contents")]
    [SerializeField] private GameObject recipesTab;
    [SerializeField] private GameObject charactersTab;

    public void ShowRecipes() => SetActiveTab(recipesTab);
    public void ShowCharacters() => SetActiveTab(charactersTab);

    private void SetActiveTab(GameObject activeTab)
    {
        recipesTab.SetActive(activeTab == recipesTab);
        charactersTab.SetActive(activeTab == charactersTab);

    }
}

