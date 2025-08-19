using UnityEngine;

public class GrimoireTabsUI : MonoBehaviour
{
    [Header("Tabs Content")]
    public PagedTabUI<RecipeData> recipesContent;
    public PagedTabUI<CharacterData> charactersContent;

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
