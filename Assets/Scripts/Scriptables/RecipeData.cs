using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Grimoire/Recipe")]
public class RecipeData : ScriptableObject
{
    public string recipeName;
    public Sprite recipeImage;
    public IngredientData[] ingredients;
    [TextArea] public string description;

    public void FillPage(GameObject page)
    {
        page.transform.Find("Name").GetComponent<TMPro.TextMeshProUGUI>().text= recipeName;
        page.transform.Find("Image").GetComponent<UnityEngine.UI.Image>().sprite = recipeImage;
        page.transform.Find("Description").GetComponent<TMPro.TextMeshProUGUI>().text = description;

        string ingredientsList = "Ingredients: " + string.Join(", ", ingredients.Select(i => i.ingredientName));

        page.transform.Find("Ingredients").GetComponent<TMPro.TextMeshProUGUI>().text = ingredientsList;
    }
}

