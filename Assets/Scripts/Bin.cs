using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Bin : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // ===== SPRITES =====
    [Header("Sprites")]
    [SerializeField] private Sprite binClosed;
    [SerializeField] private Sprite binOpen;

    // ===== COMPONENTS =====
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

        if (binClosed != null)
        {
            binImage.sprite = binClosed;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (binOpen != null && binImage != null)
        {
            binImage.sprite = binOpen;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
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