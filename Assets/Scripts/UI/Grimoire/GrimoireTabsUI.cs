using System.Collections.Generic;
using UnityEngine;

public class GrimoireTabsUI : MonoBehaviour
{
    [Header("Tabs Content")]
    public PagedTabUI<RecipeData> recipesContent;
    public PagedTabUI<CharacterData> charactersContent;
    CharacterPagedTabUI characterPagedTabUI;

    public void ShowRecipes()
    {
        recipesContent.Show();
        charactersContent.Hide();
    }

    public void ShowCharacters()
    {
        charactersContent.Show();
        recipesContent.Hide();

    }
}
