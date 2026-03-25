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

                RectTransform rect = tooltipPanel.GetComponent<RectTransform>();
                rect.anchorMin = new Vector2(0, 1);
                rect.anchorMax = new Vector2(0, 1);
                rect.pivot = new Vector2(0, 1);

                tooltipPanel.SetActive(false);
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

            UpdateTooltipPosition();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltipPanel != null)
        {
            tooltipPanel.SetActive(false);
        }
    }

    void Update()
    {
        // Update position if the tooltip is active
        if (tooltipPanel != null && tooltipPanel.activeSelf)
        {
            UpdateTooltipPosition();
        }
    }

    void UpdateTooltipPosition()
    {
        RectTransform tooltipRect = tooltipPanel.GetComponent<RectTransform>();
        Canvas canvas = tooltipPanel.GetComponentInParent<Canvas>();

        // convert screen point to local point in the canvas
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            canvas.worldCamera,
            out localPoint
        );

        // offset to avoid mouse cursor overlap
        localPoint += new Vector2(15, -15);

        tooltipRect.localPosition = localPoint;
    }
}