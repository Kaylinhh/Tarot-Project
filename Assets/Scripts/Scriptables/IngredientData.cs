using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Data/Ingredient")]
public class IngredientData : ScriptableObject
{
    public string ingredientName;
    public Sprite sprite;

    [Header("Category")]
    public IngredientCategory category;
}

public enum IngredientCategory
{
    Alcohol,
    Fruit,
    Herb,
    Liquor,
    Mixer,
    Other,
    Syrup
}
