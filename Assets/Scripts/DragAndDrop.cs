using UnityEngine;
using UnityEngine.EventSystems;
using vinkn;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    // ===== DATA =====
    private IngredientData ingredient; //null if it's the glass being dragged, otherwise it's an ingredient

    // ===== DRAG STATE =====
    private bool dragging = false;
    private Vector2 oldPos;
    private Vector2 dragOffset;

    // ===== HIERARCHY TRACKING (for ingredients) =====
    private Transform originalParent;
    private int originalSiblingIndex;

    // ===== COMPONENTS =====
    private Canvas rootCanvas;
    private Canvas canvas;
    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        rootCanvas = canvas;
    }

    // ===== EVENT SUBSCRIPTION (mode dialogue/minigame) =====

    void OnEnable()
    {
        VNEngine.OnDialogueMode += DisableDragging;
        VNEngine.OnMinigameMode += EnableDragging;
    }

    void OnDisable()
    {
        VNEngine.OnDialogueMode -= DisableDragging;
        VNEngine.OnMinigameMode -= EnableDragging;
    }

    void DisableDragging() => enabled = false;
    void EnableDragging() => enabled = true;

    // ===== PUBLIC API =====
    public void SetIngredient(IngredientData data)
    {
        ingredient = data;
    }

    // ===== DRAG LOGIC =====
    public void OnPointerDown(PointerEventData eventData)
    {
        oldPos = rectTransform.anchoredPosition;
        dragging = true;

        if (ingredient != null)
        {
            // ingredient : Reparent to Canvas root to drag above everything
            originalParent = transform.parent;
            originalSiblingIndex = transform.GetSiblingIndex();

            Vector3 worldPosBefore = rectTransform.position;
            transform.SetParent(rootCanvas.transform);
            transform.SetAsLastSibling();
            rectTransform.position = worldPosBefore;

            // calculte offset in Canvas root
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
            // glass :stay in its parent
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
        if (!dragging) return;

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform,
            Input.mousePosition,
            canvas.worldCamera,
            out localPoint
        );

        rectTransform.anchoredPosition = localPoint + dragOffset;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragging = false;

        if (ingredient == null)
        {
            // glass : Check only the bin
            CheckForBin(eventData);
        }
        else
        {
            // ingredient : check glass ou bin
            CheckForGlassOrBin(eventData);

            // Restore original parent and sibling index
            transform.SetParent(originalParent);
            transform.SetSiblingIndex(originalSiblingIndex);
        }

        // return to old position 
        rectTransform.anchoredPosition = oldPos;
    }

    // ===== DROP DETECTION =====
    void CheckForGlassOrBin(PointerEventData eventData)
    {
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            // check glass (CocktailManager)
            CocktailManager glass = result.gameObject.GetComponent<CocktailManager>();
            if (glass != null)
            {
                glass.AddIngredient(ingredient);
                return;
            }

            // check bin
            Bin bin = result.gameObject.GetComponent<Bin>();
            if (bin != null)
            {
                bin.DeleteIngredients();
                return;
            }
        }
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