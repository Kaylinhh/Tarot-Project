using UnityEngine;
using System.Collections.Generic;

public class RecipeBookUI : MonoBehaviour
{
    public Transform recipesContent;
    public GameObject recipePagePrefab;
    public List<RecipeData> recipes;

    private List<GameObject> instantiatedPages = new List<GameObject>();
    private int currentPage = 0;

    void Start()
    {
        foreach (var recipe in recipes)
        {
            GameObject page = Instantiate(recipePagePrefab, recipesContent);
            page.GetComponent<RecipePageUI>().Setup(recipe);
            page.SetActive(false);
            instantiatedPages.Add(page);
        }

        if (instantiatedPages.Count > 0)
            instantiatedPages[0].SetActive(true);
    }

    public void NextPage()
    {
        if (instantiatedPages.Count == 0) return;
        instantiatedPages[currentPage].SetActive(false);
        currentPage = (currentPage + 1) % instantiatedPages.Count;
        instantiatedPages[currentPage].SetActive(true);
    }

    public void PreviousPage()
    {
        if (instantiatedPages.Count == 0) return;
        instantiatedPages[currentPage].SetActive(false);
        currentPage = (currentPage - 1 + instantiatedPages.Count) % instantiatedPages.Count;
        instantiatedPages[currentPage].SetActive(true);
    }
}
