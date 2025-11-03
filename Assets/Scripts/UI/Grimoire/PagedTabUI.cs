using System.Collections.Generic;
using UnityEngine;

public class PagedTabUI<T> : MonoBehaviour where T : ScriptableObject
{
    [Header("Data & Prefab")]
    public List<T> dataList;
    public GameObject pagePrefab;
    public Transform leftPageParent;
    public Transform rightPageParent;

    private int currentIndex = 0;
    private List<GameObject> instantiatedPages = new List<GameObject>();

    public void Show()
    {
        gameObject.SetActive(true);

        if (instantiatedPages.Count == 0)
            InstantiatePages();

        currentIndex = 0;
        UpdatePages();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void InstantiatePages()
    {
        foreach (T data in dataList)
        {
            GameObject page = Instantiate(pagePrefab);
            page.SetActive(false);

            // Attache dans une liste générique
            instantiatedPages.Add(page);

            // Remplissage avec IPageFiller
            var filler = page.GetComponent<IPageFiller<T>>();
            if (filler != null) filler.FillPage(data);
        }
    }

    public void Next()
    {
        Debug.Log("[NEXT] avant incrément : " + currentIndex);

        if (currentIndex + 2 < instantiatedPages.Count)
        {
            currentIndex += 2;
            UpdatePages();
            Debug.Log("[NEXT] après incrément : " + currentIndex);

        }
    }

    public void Previous()
    {
        Debug.Log("[PREV] avant incrément : " + currentIndex);

        if (currentIndex - 2 >= 0)
        {
            currentIndex -= 2;
            UpdatePages();
            Debug.Log("[PREV] après incrément : " + currentIndex);

        }
    }

    public void UpdatePages()
    {
        // On cache tout
        foreach (var page in instantiatedPages)
            page.SetActive(false);

        // Page gauche
        if (currentIndex < instantiatedPages.Count)
        {
            instantiatedPages[currentIndex].SetActive(true);
            instantiatedPages[currentIndex].transform.SetParent(leftPageParent, false);
        }

        // Page droite
        if (currentIndex + 1 < instantiatedPages.Count)
        {
            instantiatedPages[currentIndex + 1].SetActive(true);
            instantiatedPages[currentIndex + 1].transform.SetParent(rightPageParent, false);
        }
    }
}
