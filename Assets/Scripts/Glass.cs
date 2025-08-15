using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Glass : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI recipeText;
    [SerializeField] List<RecipeData> allRecipes;
    private List<IngredientData> currentIngredients = new List<IngredientData>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
            recipeText.text += "- " + ingredient.name + "\n";
        }
    }

    public void CheckForRecipe()
    {
        foreach(var recipe in allRecipes)
        {
            Debug.Log(recipe.recipeName);
            return;
        }
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
        return true;
    }
}
