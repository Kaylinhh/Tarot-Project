using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cocktail/Recipe")]
public class RecipeData : ScriptableObject
{
    public string recipeName;
    public Sprite recipeImage;
    public IngredientData[] ingredients;
    [TextArea] public string description;
}

