using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SimpleTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string tooltipText;

    private static GameObject tooltipPanel;
    private static TextMeshProUGUI tooltipTextComponent;

    void Start()
    {
        if (tooltipPanel == null)
        {
            tooltipPanel = GameObject.Find("TooltipPanel");

            if (tooltipPanel != null)
            {
                tooltipTextComponent = tooltipPanel.GetComponentInChildren<TextMeshProUGUI>();

                // FORCE anchor à Top-Left
                RectTransform rect = tooltipPanel.GetComponent<RectTransform>();
                rect.anchorMin = new Vector2(0, 1); // Top-Left
                rect.anchorMax = new Vector2(0, 1); // Top-Left
                rect.pivot = new Vector2(0, 1);     // Top-Left

                tooltipPanel.SetActive(false);
                Debug.Log("[Tooltip] TooltipPanel found, configured, and hidden");
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tooltipPanel != null && !string.IsNullOrEmpty(tooltipText))
        {
            tooltipPanel.SetActive(true);

            if (tooltipTextComponent != null)
            {
                tooltipTextComponent.text = tooltipText;
            }

            // Position près de la souris
            UpdateTooltipPosition();
        }
    }

    void Update()
    {
        // Si le tooltip est actif, update sa position en continu
        if (tooltipPanel != null && tooltipPanel.activeSelf)
        {
            UpdateTooltipPosition();
        }
    }

    void UpdateTooltipPosition()
    {
        RectTransform tooltipRect = tooltipPanel.GetComponent<RectTransform>();
        Canvas canvas = tooltipPanel.GetComponentInParent<Canvas>();

        // Converti mouse screen to canvas local
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            canvas.worldCamera,
            out localPoint
        );

        // Offset
        localPoint += new Vector2(15, -15);

        // Set LOCAL position (pas world position)
        tooltipRect.localPosition = localPoint;

        Debug.Log($"[Tooltip] Mouse: {Input.mousePosition}, LocalPos set to: {localPoint}, Actual localPos: {tooltipRect.localPosition}");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltipPanel != null)
        {
            tooltipPanel.SetActive(false);
        }
    }
}