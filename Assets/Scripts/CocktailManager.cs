using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CocktailManager : MonoBehaviour
{
    // ===== UI =====
    [SerializeField] TextMeshProUGUI recipeText;
    [SerializeField] GameObject shakeButton;
    [SerializeField] GameObject serveButton;
    [SerializeField] GameObject resetButton;
    [SerializeField] GameObject backButton;

    // ===== DATA =====
    [SerializeField] List<RecipeData> allRecipes;
    private List<IngredientData> currentIngredients = new List<IngredientData>();
    private RecipeData currentRecipe = null;

    // ===== ANIMATION =====
    [Header("Animation")]
    [SerializeField] private RectTransform shakerTransform; 
    [SerializeField] private CocktailRevealPanel revealPanel;

    void Start()
    {
        if (GameModeManager.Instance.IsArcadeMode)
        {
            backButton.SetActive(true);
        } else
        {
            backButton.SetActive(false);
        }
    }

    public void AddIngredient(IngredientData newIngredient)
    {
        currentIngredients.Add(newIngredient);
        UpdateRecipeText();
    }

    private void UpdateRecipeText()
    {
        if (currentIngredients == null) 
        {
            recipeText.text = "";
        }

        recipeText.text = "Recipe:\n";
        foreach (var ingredient in currentIngredients)
        {
            recipeText.text += "- " + ingredient.ingredientName + "\n";
        }
    }

    public void CheckForRecipe()
    {
        foreach (var recipe in allRecipes)
        {
            if (MatchRecipe(recipe))
            {
                recipeText.text = $"You created: {recipe.recipeName}";
                currentRecipe = recipe;

                revealPanel.Show(recipe);
                AudioManager.instance.PlaySparkleSound();
                // only in story mode
                if (!GameModeManager.Instance.IsArcadeMode && recipe.isDiscovered)
                {
                    DataManager.Instance?.NotifyRecipeDiscovered();
                }

                ShowActionButton();
                return;
            }
        }

        recipeText.text = "Unknown recipe! Try again or toss it.";
    }


    private bool MatchRecipe(RecipeData recipe)
    {
        if (recipe.ingredients.Length != currentIngredients.Count)
            return false;

        //Temporary copy to avoid false positive doubles 
        var tempList = new List<IngredientData>(currentIngredients);

        foreach (var ingredient in recipe.ingredients)
        {
            if (!tempList.Contains(ingredient))
                return false;
            tempList.Remove(ingredient);
        }

        if (recipe.isDiscovered == false)
            recipe.isDiscovered = true;

        return true;
    }

    void ShowActionButton()
    {
        shakeButton.SetActive(false);

        if (GameModeManager.Instance.IsArcadeMode)
        {
            // arcade mode: reset button
            resetButton.SetActive(true); 
        }
        else
        {
            // story mode : serve button
            serveButton.SetActive(true);
        }
    }

    public string GetCurrentCocktailName()
    {
        if (currentRecipe != null)
        {
            return currentRecipe.recipeName;
        }

        Debug.LogError("no current recipe found");
        return "Unknown";
    }

    public void OnServeButtonClick()
    {
        string cocktailName = GetCurrentCocktailName();

        GameSceneManager gsm = FindFirstObjectByType<GameSceneManager>();
        if (gsm != null)
        {
            gsm.ChangeSceneFromBar(cocktailName);
        }
        else
        {
            Debug.LogError("GameSceneManager not found!");
        }
    }

    public void OnBackButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    } 

    public void OnResetButtonClick()
    {
        ResetCocktail();
        currentRecipe = null;

        resetButton.SetActive(false);
        shakeButton.SetActive(true);
        revealPanel.gameObject.SetActive(false);
    }

    public void ResetCocktail()
    {
        currentIngredients.Clear();
        UpdateRecipeText();
    }

    public void OnShakeButtonClick()
    {
        AudioManager.instance.PlayShakeSound();
        StartCoroutine(ShakeAnimation());

        // Check recipe after the animation
        Invoke(nameof(CheckForRecipe), 0.5f);
    }

    IEnumerator ShakeAnimation()
    {
        Vector3 originalPos = shakerTransform.anchoredPosition;
        Quaternion originalRot = shakerTransform.localRotation;

        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            // horizontal & vertical shake + rotation
            float shakeX = Random.Range(-10f, 10f);
            float shakeY = Random.Range(-5f, 5f);
            float rotationZ = Mathf.Sin(elapsed * 40f) * 15f; 
            shakerTransform.anchoredPosition = originalPos + new Vector3(shakeX, shakeY, 0);
            shakerTransform.localRotation = Quaternion.Euler(0, 0, rotationZ);

            yield return null;
        }

        // Return to position
        shakerTransform.anchoredPosition = originalPos;
        shakerTransform.localRotation = originalRot;

    }
}
