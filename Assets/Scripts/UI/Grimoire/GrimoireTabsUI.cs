using UnityEngine;

public class GrimoireTabsUI : MonoBehaviour
{
    [Header("Tabs Content")]
    [SerializeField] private PagedTabUI<RecipeData> recipesContent;
    [SerializeField] private PagedTabUI<CharacterData> charactersContent;

    [Header("Welcome Page")]
    [SerializeField] private GameObject welcomePagePrefab;
    [SerializeField] private Transform rightPageParent;

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
        // Nettoie les autres contenus
        recipesContent.Hide();
        charactersContent.Hide();

        // Désactive les boutons de navigation
        nextButton.SetActive(false);
        previousButton.SetActive(false);

        // Instancie la page d'accueil si elle n'existe pas déjà
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
