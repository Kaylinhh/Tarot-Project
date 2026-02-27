using UnityEngine;

public class Bin : MonoBehaviour
{
    private CocktailManager cocktailManager;

    void Start()
    {
        // Trouve le CocktailManager dans la scène
        cocktailManager = FindFirstObjectByType<CocktailManager>();

        if (cocktailManager == null)
        {
            Debug.LogError("CocktailManager not found!");
        }
    }

    public void DeleteIngredients()
    {
        if (cocktailManager != null)
        {
            cocktailManager.ResetCocktail();
            Debug.Log("Cocktail reset!");
        }
    }
}