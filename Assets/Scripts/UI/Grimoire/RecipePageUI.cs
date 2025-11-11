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
    public TMP_Text ingredientsText;

    [Header("Locked Placeholder")]
    [SerializeField] private string lockedName = "???";
    [SerializeField] private Sprite lockedImage;
    [SerializeField] private string lockedDescription = "You haven't discovered this drink yet.";
    [SerializeField] private string lockedIngredients = "???";

    private void Start()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    public void FillPage(RecipeData recipe)
    {
        if (recipe.isDiscovered == true)
        {
            // Nom et image du cocktail
            recipeNameText.text = recipe.recipeName;
            recipeImage.sprite = recipe.recipeImage;

            // Description
            descriptionText.text = recipe.description;

            // Liste des ingrédients sous forme de texte
            ingredientsText.text = "Ingredients: " + string.Join(", ", recipe.ingredients.Select(i => i.ingredientName));
        }
        else
        {
            recipeNameText.text = lockedName;
            recipeImage.sprite = lockedImage;
            descriptionText.text = lockedDescription;
            ingredientsText.text = lockedIngredients;
        }

            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }


}
