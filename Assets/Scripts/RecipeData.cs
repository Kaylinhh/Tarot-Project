using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cocktail/Recipe")]
public class RecipeData : ScriptableObject
{
    public string recipeName;
    public IngredientData[] ingredients;
    [TextArea]
    public string description;
    public Sprite image;
}

