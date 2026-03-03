using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class IngredientsManager : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] GameObject ingredientPrefab;

    [Header("Content Panels")]
    [SerializeField] GameObject alcoholContent;
    [SerializeField] GameObject fruitContent;
    [SerializeField] GameObject herbContent;
    [SerializeField] GameObject liquorContent;
    [SerializeField] GameObject mixerContent;
    [SerializeField] GameObject otherContent;
    [SerializeField] GameObject syrupContent;

    [Header("Data")]
    [SerializeField] List<IngredientData> allIngredients;

    void Start()
    {
        PopulateCategories();
    }

    void PopulateCategories()
    {
        var alcohols = allIngredients.Where(i => i.category == IngredientCategory.Alcohol).ToList();
        var fruits = allIngredients.Where(i => i.category == IngredientCategory.Fruit).ToList();
        var herbs = allIngredients.Where(i => i.category == IngredientCategory.Herb).ToList();
        var liquors = allIngredients.Where(i => i.category == IngredientCategory.Liquor).ToList();
        var mixers = allIngredients.Where(i => i.category == IngredientCategory.Mixer).ToList();
        var others = allIngredients.Where(i => i.category == IngredientCategory.Other).ToList();
        var syrups = allIngredients.Where(i => i.category == IngredientCategory.Syrup).ToList();

        CreateIngredients(alcohols, alcoholContent);
        CreateIngredients(fruits, fruitContent);
        CreateIngredients(herbs, herbContent);
        CreateIngredients(liquors, liquorContent);
        CreateIngredients(mixers, mixerContent);
        CreateIngredients(others, otherContent);
        CreateIngredients(syrups, syrupContent);
    }

    void CreateIngredients(List<IngredientData> ingredients, GameObject parent)
    {
        foreach (var ingredient in ingredients)
        {
            GameObject obj = Instantiate(ingredientPrefab, parent.transform);

            // Setup visual
            var image = obj.GetComponentInChildren<UnityEngine.UI.Image>();
            if (image != null && ingredient.sprite != null)
            {
                image.sprite = ingredient.sprite;
            }

            // Setup label
            var label = obj.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            if (label != null)
            {
                label.text = ingredient.ingredientName;
            }

            // Setup drag & drop
            var dragDrop = obj.GetComponentInChildren<DragAndDrop>();
            if (dragDrop != null)
            {
                dragDrop.SetIngredient(ingredient);
            }

            obj.name = ingredient.ingredientName;
        }
    }
}