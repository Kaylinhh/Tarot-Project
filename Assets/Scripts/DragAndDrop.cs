using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private IngredientData ingredient;

    private Transform originalParent;
    private int originalSiblingIndex;
    private Canvas rootCanvas;
    private Vector2 savedAnchoredPosition;

    private Vector2 oldPos;
    private bool dragging = false;
    private Vector2 dragOffset;

    private Canvas canvas;
    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        rootCanvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (dragging)
        {
            // Utilise le bon parent selon si ingrédient ou verre
            RectTransform parentRect = ingredient != null
                ? rootCanvas.GetComponent<RectTransform>()
                : rectTransform.parent as RectTransform;

            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentRect,
                Input.mousePosition,
                canvas.worldCamera,
                out localPoint
            );

            rectTransform.anchoredPosition = localPoint + dragOffset;
        }
    }

    public void SetIngredient(IngredientData data)
    {
        ingredient = data;
        Debug.Log($"[DragDrop] Ingredient set: {(data != null ? data.ingredientName : "NULL")}");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"[DragDrop] ===== OnPointerDown on {gameObject.name} =====");
        Debug.Log($"[DragDrop] ingredient: {(ingredient != null ? ingredient.ingredientName : "NULL")}");
        Debug.Log($"[DragDrop] oldPos BEFORE: {rectTransform.anchoredPosition}");

        oldPos = rectTransform.anchoredPosition;
        dragging = true;

        Debug.Log($"[DragDrop] oldPos SAVED: {oldPos}");
        Debug.Log($"[DragDrop] dragging set to TRUE");

        // SI C'EST UN INGRÉDIENT, reparent au Canvas root
        if (ingredient != null)
        {
            // SAVE state
            originalParent = transform.parent;
            originalSiblingIndex = transform.GetSiblingIndex();

            // SAVE world position AVANT reparent
            Vector3 worldPosBefore = rectTransform.position;

            // REPARENT
            transform.SetParent(rootCanvas.transform);
            transform.SetAsLastSibling();

            // RESTORE world position
            rectTransform.position = worldPosBefore;

            // CALCULE l'offset (dans le système du Canvas root)
            Vector2 localPointerPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rootCanvas.GetComponent<RectTransform>(),
                eventData.position,
                rootCanvas.worldCamera,
                out localPointerPos
            );

            dragOffset = rectTransform.anchoredPosition - localPointerPos;
        }
        else
        {
            // C'EST LE VERRE - pas de reparent, juste calcule offset dans son parent
            Vector2 localPointerPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform.parent as RectTransform,
                eventData.position,
                canvas.worldCamera,
                out localPointerPos
            );

            dragOffset = rectTransform.anchoredPosition - localPointerPos;
        }
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

        if (dragging)
        {
            Canvas canvas = GetComponentInParent<Canvas>();

            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform.parent as RectTransform,
                Input.mousePosition,
                canvas.worldCamera,
                out localPoint
            );

            // add offset
            rectTransform.anchoredPosition = localPoint + dragOffset;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log($"[DragDrop] ===== OnPointerUp on {gameObject.name} =====");

        dragging = false;

        if (ingredient == null)
        {
            // C'est le verre - pas de reparent à restore
            CheckForBin(eventData);
            rectTransform.anchoredPosition = oldPos;
            return;
        }

        // C'est un ingrédient - restore le parent
        CheckForGlassOrBin(eventData);

        transform.SetParent(originalParent);
        transform.SetSiblingIndex(originalSiblingIndex);

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