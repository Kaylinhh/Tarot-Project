using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Glass : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI recipeText;
    private List<IngredientData> ingredients = new List<IngredientData>();

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
        ingredients.Add(newIngredient);
        UpdateRecipeText();

    }

    private void UpdateRecipeText()
    {
        recipeText.text = "Recipe:\n";
        foreach(var ingredient in ingredients)
        {
            recipeText.text += "- " + ingredient.name + "\n";
        }
    }
}
