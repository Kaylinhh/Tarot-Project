using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Bin : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Sprites")]
    [SerializeField] private Sprite binClosed;
    [SerializeField] private Sprite binOpen;

    private Image binImage;
    private CocktailManager cocktailManager;

    void Start()
    {
        cocktailManager = FindFirstObjectByType<CocktailManager>();
        binImage = GetComponent<Image>();

        if (binImage == null)
        {
            Debug.LogError("[Bin] No Image component found!");
        }

        // Set closed par défaut
        if (binClosed != null)
        {
            binImage.sprite = binClosed;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Ouvre le couvercle au hover
        if (binOpen != null && binImage != null)
        {
            binImage.sprite = binOpen;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Ferme le couvercle
        if (binClosed != null && binImage != null)
        {
            binImage.sprite = binClosed;
        }
    }

    public void DeleteIngredients()
    {
        if (cocktailManager != null)
        {
            cocktailManager.ResetCocktail();
        }
    }
}