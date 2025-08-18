using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class RecipePageUI : MonoBehaviour, IPageFiller<RecipeData>
{
    [Header("UI References")]
    public TMP_Text recipeNameText;
    public Image recipeImage;
    public TMP_Text descriptionText;
    public TMP_Text ingredientsText; // Nouveau : juste un texte pour la liste des ingrédients

    public void FillPage(RecipeData recipe)
    {
        // Nom et image du cocktail
        recipeNameText.text = recipe.recipeName;
        recipeImage.sprite = recipe.recipeImage;

        // Description
        descriptionText.text = recipe.description;

        // Liste des ingrédients sous forme de texte
        ingredientsText.text = "Ingredients: " + string.Join(", ", recipe.ingredients.Select(i => i.ingredientName));
    }
}
