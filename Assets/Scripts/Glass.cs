using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Glass : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI recipeText;
    [SerializeField] List<RecipeData> allRecipes;
    [SerializeField] GameObject shakeButton;
    [SerializeField] GameObject serveButton;
    private List<IngredientData> currentIngredients = new List<IngredientData>();

    public void AddIngredient(IngredientData newIngredient)
    {
        currentIngredients.Add(newIngredient);
        UpdateRecipeText();
        
    }

    private void UpdateRecipeText()
    {
        recipeText.text = "Recipe:\n";
        foreach(var ingredient in currentIngredients)
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
                ServeDrink(recipe);
                return; 
            }
        }

        Debug.Log("No match");
        recipeText.text = "Unknown recipe";
        
    }


    private bool MatchRecipe(RecipeData recipe)
    {
        if (recipe.ingredients.Length != currentIngredients.Count)
            return false;


        //Temporary copy to avoid false positives doubles 
        var tempList = new List<IngredientData>(currentIngredients);

        foreach(var ingredient in recipe.ingredients)
        {
            if (!tempList.Contains(ingredient))
                return false;
            tempList.Remove(ingredient);
        }
        
        if (recipe.isDiscovered == false)
            recipe.isDiscovered = true;

        return true;
    }

    public void ServeDrink(RecipeData recipe)
    {
        if (MatchRecipe(recipe))
        {
            shakeButton.SetActive(false);
            serveButton.SetActive(true);
        }
    }
}
