using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class RecipePageUI : MonoBehaviour, IPageFiller<RecipeData>
{
    // ===== UI ELEMENTS =====
    [Header("UI References")]
    [SerializeField] private TMP_Text recipeNameText;
    [SerializeField] private Image recipeImage;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text ingredientsText;

    // ===== LOCKED PLACEHOLDERS =====
    [Header("Locked Placeholder")]
    [SerializeField] private string lockedName = "???";
    [SerializeField] private Image recipeImageLocked;
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
            recipeNameText.text = recipe.recipeName;
            recipeImage.sprite = recipe.recipeImage;
            descriptionText.text = recipe.description;
            ingredientsText.text = "Ingredients: " + string.Join(", ", recipe.ingredients.Select(i => i.ingredientName));
        }
        else
        {
            recipeNameText.text = lockedName;
            recipeImage.sprite = recipe.recipeImageLocked;
            descriptionText.text = lockedDescription;
            ingredientsText.text = lockedIngredients;
        }

            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
}
