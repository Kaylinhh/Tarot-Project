using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CocktailManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI recipeText;
    [SerializeField] List<RecipeData> allRecipes;
    [SerializeField] GameObject shakeButton;
    [SerializeField] GameObject serveButton;
    private List<IngredientData> currentIngredients = new List<IngredientData>();
    private RecipeData currentRecipe = null;

    [Header("Animation")]
    [SerializeField] private RectTransform shakerTransform; // shaker
    [SerializeField] private CocktailRevealPanel revealPanel;

    public void AddIngredient(IngredientData newIngredient)
    {
        currentIngredients.Add(newIngredient);
        Debug.Log("added ingredient" + newIngredient.name);
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
                Debug.Log("FOUND" + recipe.recipeName);
                recipeText.text = "You created: " + recipe.recipeName;
                currentRecipe = recipe;

                if (revealPanel != null)
                {
                    revealPanel.Show(recipe);
                }

                ShowServeButton();
                return;
            }
        }

        Debug.Log("No match");
        recipeText.text = "Unknown recipe! You can't serve that, toss it in the bin.";

    }


    private bool MatchRecipe(RecipeData recipe)
    {
        if (recipe.ingredients.Length != currentIngredients.Count)
            return false;

        //Temporary copy to avoid false positives doubles 
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

    public void ShowServeButton()
    {
            shakeButton.SetActive(false);
            serveButton.SetActive(true);
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

    public void ResetCocktail()
    {
        currentIngredients.Clear();
        UpdateRecipeText();
    }

    public void OnShakeButtonClick()
    {
        StartCoroutine(ShakeAnimation());

        // Check recipe après l'animation
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

            // Shake horizontal + rotation
            float shakeX = Random.Range(-10f, 10f);
            float shakeY = Random.Range(-5f, 5f);
            float rotationZ = Mathf.Sin(elapsed * 40f) * 15f; // Rotation rapide

            shakerTransform.anchoredPosition = originalPos + new Vector3(shakeX, shakeY, 0);
            shakerTransform.localRotation = Quaternion.Euler(0, 0, rotationZ);

            yield return null;
        }

        // Retour position
        shakerTransform.anchoredPosition = originalPos;
        shakerTransform.localRotation = originalRot;
    }

}
