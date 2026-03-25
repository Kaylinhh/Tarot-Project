using UnityEngine;

public class GrimoireTabsUI : MonoBehaviour
{
    // ===== TABS CONTENT =====
    [Header("Tabs Content")]
    [SerializeField] private PagedTabUI<RecipeData> recipesContent;
    [SerializeField] private PagedTabUI<CharacterData> charactersContent;

    // ===== WELCOME PAGE =====
    [Header("Welcome Page")]
    [SerializeField] private GameObject welcomePagePrefab;
    [SerializeField] private Transform rightPageParent;

    // ===== NAVIGATION =====
    [Header("Navigation Buttons")]
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject previousButton;

    private GameObject welcomePageInstance;

    private void Start()
    {
        ShowWelcomePage();
    }

    public void ShowWelcomePage()
    {
        // clean other content
        recipesContent.Hide();
        charactersContent.Hide();

        // inactive nav buttons
        nextButton.SetActive(false);
        previousButton.SetActive(false);

        // Instantiate welcome page if it doesn't exist, otherwise just show it
        if (welcomePageInstance == null)
        {
            welcomePageInstance = Instantiate(welcomePagePrefab, rightPageParent);
        }

        welcomePageInstance.SetActive(true);
    }

    public void ShowRecipes()
    {
        HideWelcomePage();
        recipesContent.Show();
        recipesContent.ResetToFirstPage();
        charactersContent.Hide();

        nextButton.SetActive(true);
        previousButton.SetActive(true);
    }

    public void ShowCharacters()
    {
        HideWelcomePage();
        charactersContent.Show();
        charactersContent.ResetToFirstPage();
        recipesContent.Hide();

        nextButton.SetActive(true);
        previousButton.SetActive(true);
    }

    private void HideWelcomePage()
    {
        if (welcomePageInstance != null)
            welcomePageInstance.SetActive(false);

        rightPageParent.gameObject.SetActive(true);
    }
}
