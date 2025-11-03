using System.Collections.Generic;
using UnityEngine;

public class PagedTabUI<T> : MonoBehaviour where T : ScriptableObject
{
    [Header("Data & Prefab")]
    [SerializeField] private GameObject pagePrefab;
    [SerializeField] private Transform leftPageParent;
    [SerializeField] private Transform rightPageParent;

    private List<T> dataList = new List<T>();
    private readonly List<GameObject> instantiatedPages = new();
    private int currentIndex = 0;

    private void OnEnable()
    {
        // On s'abonne aux events du DataManager
        if (typeof(T) == typeof(CharacterData))
            DataManager.Instance.OnCharacterDiscovered += Refresh;
        else if (typeof(T) == typeof(RecipeData))
            DataManager.Instance.OnRecipeDiscovered += Refresh;

        // Initialisation
        Refresh();
    }

    private void OnDisable()
    {
        // On se désabonne
        if (typeof(T) == typeof(CharacterData))
            DataManager.Instance.OnCharacterDiscovered -= Refresh;
        else if (typeof(T) == typeof(RecipeData))
            DataManager.Instance.OnRecipeDiscovered -= Refresh;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        UpdatePages();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Next()
    {
        if (currentIndex + 2 < instantiatedPages.Count)
        {
            currentIndex += 2;
            UpdatePages();
        }
    }

    public void Previous()
    {
        if (currentIndex - 2 >= 0)
        {
            currentIndex -= 2;
            UpdatePages();
        }
    }


    // Rafraîchit les pages sans tout détruire.
    // Met à jour le contenu si des données changent.
    public void Refresh()
    {
        // Récupère les données à jour
        if (typeof(T) == typeof(CharacterData))
            dataList = DataManager.Instance.GetCharacters() as List<T>;
        else if (typeof(T) == typeof(RecipeData))
            dataList = DataManager.Instance.GetRecipes() as List<T>;

        // Si le nombre de pages change (nouveau perso/recette découvert)
        if (instantiatedPages.Count != dataList.Count)
        {
            // Supprime les anciennes pages
            foreach (var page in instantiatedPages)
                Destroy(page);
            instantiatedPages.Clear();

            // Recrée proprement
            InstantiatePages();
        }
        else
        {
            // Sinon, on met simplement à jour les pages existantes
            for (int i = 0; i < instantiatedPages.Count; i++)
            {
                var filler = instantiatedPages[i].GetComponent<IPageFiller<T>>();
                filler?.FillPage(dataList[i]);
            }
        }

        UpdatePages();
    }

    private void InstantiatePages()
    {
        foreach (var data in dataList)
        {
            GameObject page = Instantiate(pagePrefab);
            page.SetActive(false);

            instantiatedPages.Add(page);

            var filler = page.GetComponent<IPageFiller<T>>();
            if (filler != null)
                filler.FillPage(data);
        }
    }

    private void UpdatePages()
    {
        foreach (var page in instantiatedPages)
            page.SetActive(false);

        if (currentIndex < instantiatedPages.Count)
        {
            instantiatedPages[currentIndex].SetActive(true);
            instantiatedPages[currentIndex].transform.SetParent(leftPageParent, false);
        }

        if (currentIndex + 1 < instantiatedPages.Count)
        {
            instantiatedPages[currentIndex + 1].SetActive(true);
            instantiatedPages[currentIndex + 1].transform.SetParent(rightPageParent, false);
        }
    }
}
