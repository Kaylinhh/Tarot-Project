using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private IngredientData ingredient;

    private Vector3 oldPos;
    private bool dragging = false;

    private Canvas canvas;
    private RectTransform rectTransform;

    void Awake()
    {
        Debug.Log($"[DragDrop] Awake on {gameObject.name}");

        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        if (rectTransform == null)
            Debug.LogError($"[DragDrop] NO RectTransform!");

        if (canvas == null)
            Debug.LogError($"[DragDrop] NO Canvas found!");
        else
            Debug.Log($"[DragDrop] Canvas found: {canvas.name}");
    }

    public void SetIngredient(IngredientData data)
    {
        ingredient = data;
        Debug.Log($"[DragDrop] Ingredient set: {(data != null ? data.ingredientName : "NULL")}");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // if no ingredients, then it's the glass:
        if (ingredient == null)
        {
        }

        oldPos = rectTransform.anchoredPosition;
        dragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!dragging)
        {
            return;
        }

        if (canvas == null || rectTransform == null)
        {
            return;
        }

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform, 
            eventData.position,
            eventData.pressEventCamera,
            out localPoint
        );

        rectTransform.anchoredPosition = localPoint;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log($"[DragDrop] OnPointerUp");

        dragging = false;

        if (ingredient == null)
        {
            // C'est le verre qui est draggé
            CheckForBin(eventData);
            rectTransform.anchoredPosition = oldPos;
            return;
        }

        // C'est un ingrédient
        CheckForGlassOrBin(eventData);

        // Retour position
        rectTransform.anchoredPosition = oldPos;
    }

    void CheckForGlassOrBin(PointerEventData eventData)
    {
        // Raycast UI pour trouver ce qui est sous la souris
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        Debug.Log($"[DragDrop] Found {results.Count} UI elements under pointer");

        foreach (var result in results)
        {
            Debug.Log($"[DragDrop] Hit: {result.gameObject.name}");

            // Check verre
            CocktailManager glass = result.gameObject.GetComponent<CocktailManager>();
            if (glass != null)
            {
                Debug.Log($"[DragDrop] Adding ingredient to glass");
                glass.AddIngredient(ingredient);
                return;
            }

            // Check poubelle
            Bin bin = result.gameObject.GetComponent<Bin>();
            if (bin != null)
            {
                Debug.Log($"[DragDrop] Deleting ingredients");
                bin.DeleteIngredients();
                return;
            }
        }

        Debug.Log("[DragDrop] No valid drop target found");
    }

    void CheckForBin(PointerEventData eventData)
    {
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            Bin bin = result.gameObject.GetComponent<Bin>();
            if (bin != null)
            {
                bin.DeleteIngredients();
                return;
            }
        }
    }
}