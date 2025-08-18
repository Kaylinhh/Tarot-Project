using System.Collections.Generic;
using UnityEngine;

public class PagedTabUI<T> : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform contentParent; // Conteneur où les pages sont instanciées
    [SerializeField] private GameObject pagePrefab;

    [Header("Data")]
    [SerializeField] private List<T> dataList = new List<T>();

    private List<GameObject> instantiatedPages = new List<GameObject>();
    private int currentPage = 0;

    private void Start()
    {
        GeneratePages();
        ShowPage(0);
    }

    private void GeneratePages()
    {
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        instantiatedPages.Clear();

        foreach (var data in dataList)
        {
            GameObject pageGO = Instantiate(pagePrefab, contentParent);
            pageGO.SetActive(false);

            // On cherche un composant capable de remplir la page
            var filler = pageGO.GetComponent<IPageFiller<T>>();
            if (filler != null)
            {
                filler.FillPage(data);
            }
            else
            {
                Debug.LogWarning("Le prefab ne possède pas de composant IPageFiller<" + typeof(T).Name + ">");
            }

            instantiatedPages.Add(pageGO);
        }
    }

    private void ShowPage(int index)
    {
        if (instantiatedPages.Count == 0) return;

        for (int i = 0; i < instantiatedPages.Count; i++)
            instantiatedPages[i].SetActive(i == index);

        currentPage = index;
    }

    public void NextPage()
    {
        if (instantiatedPages.Count == 0) return;
        ShowPage((currentPage + 1) % instantiatedPages.Count);
    }

    public void PreviousPage()
    {
        if (instantiatedPages.Count == 0) return;
        ShowPage((currentPage - 1 + instantiatedPages.Count) % instantiatedPages.Count);
    }

    // Pour assigner la liste de données dynamiquement
    public void SetDataList(List<T> list)
    {
        dataList = list;
        GeneratePages();
        ShowPage(0);
    }
}
