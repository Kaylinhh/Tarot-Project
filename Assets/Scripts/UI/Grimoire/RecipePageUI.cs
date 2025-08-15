using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RecipePageUI : MonoBehaviour
{
    public TMP_Text recipeNameText;
    public Image recipeImage;
    public TMP_Text descriptionText;
    public Transform ingredientsContainer;

    public void Setup(RecipeData recipe)
    {
        recipeNameText.text = recipe.recipeName;
        recipeImage.sprite = recipe.recipeImage;
        descriptionText.text = recipe.description;

        foreach (Transform child in ingredientsContainer)
            Destroy(child.gameObject);

        foreach (var ingredient in recipe.ingredients)
        {
            GameObject iconGO = new GameObject(ingredient.ingredientName);
            Image img = iconGO.AddComponent<Image>();
            img.sprite = ingredient.icon;
            iconGO.transform.SetParent(ingredientsContainer, false);
        }
    }
}
