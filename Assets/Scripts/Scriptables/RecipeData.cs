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
    public bool isDiscovered = false;

}

