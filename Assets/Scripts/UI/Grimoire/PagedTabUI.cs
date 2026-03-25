using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PagedTabUI<T> : MonoBehaviour where T : ScriptableObject
{
    // ===== CONFIGURATION =====
    [Header("Data & Prefab")]
    [SerializeField] private GameObject pagePrefab;
    [SerializeField] private Transform leftPageParent;
    [SerializeField] private Transform rightPageParent;


    // ===== INTERNAL STATE =====
    private List<T> dataList = new List<T>();
    private readonly List<GameObject> instantiatedPages = new();
    private int currentIndex = 0;

    // ===== LIFECYCLE =====
    private void OnEnable()
    {
        // subscribe to data manager events 
        if (typeof(T) == typeof(CharacterData))
            DataManager.Instance.OnCharacterDiscovered += Refresh;
        else if (typeof(T) == typeof(RecipeData))
            DataManager.Instance.OnRecipeDiscovered += Refresh;

        // Initialization
        Refresh();
    }

    private void OnDisable()
    {
        // unsubscribe to prevent memory leaks
        if (typeof(T) == typeof(CharacterData))
            DataManager.Instance.OnCharacterDiscovered -= Refresh;
        else if (typeof(T) == typeof(RecipeData))
            DataManager.Instance.OnRecipeDiscovered -= Refresh;
    }

    // ===== PUBLIC API =====
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

    public void ResetToFirstPage()
    {
        currentIndex = 0;
        UpdatePages();
    }

    // ===== REFRESH SYSTEM =====
    // refresh without destroying pages, updates content of existing pages and only creates new ones if new data is added
    public void Refresh()
    {
        // get the latest data list from the manager
        if (typeof(T) == typeof(CharacterData))
            dataList = DataManager.Instance.GetCharacters() as List<T>;
        else if (typeof(T) == typeof(RecipeData))
            dataList = DataManager.Instance.GetRecipes() as List<T>;

        // if the number of pages changes (means we discovered new character/recipe)
        if (instantiatedPages.Count != dataList.Count)
        {
            // delete all existing pages
            foreach (var page in instantiatedPages)
                Destroy(page);
            instantiatedPages.Clear();

            // recreate them cleanly
            InstantiatePages();
        }
        else
        {
            // or just update
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

            // rebuild left page
            RectTransform leftRT = instantiatedPages[currentIndex].GetComponent<RectTransform>();
            if (leftRT != null)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(leftRT);
            }
        }

        if (currentIndex + 1 < instantiatedPages.Count)
        {
            instantiatedPages[currentIndex + 1].SetActive(true);
            instantiatedPages[currentIndex + 1].transform.SetParent(rightPageParent, false);

            // rebuild right page
            RectTransform rightRT = instantiatedPages[currentIndex + 1].GetComponent<RectTransform>();
            if (rightRT != null)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(rightRT);
            }
        }
    }
}
