using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CocktailRevealPanel : MonoBehaviour
{
    // ===== UI ELEMENTS =====
    [Header("UI")]
    [SerializeField] private Image cocktailImage;
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private ParticleSystem sparkles;

    // ===== ANIMATION COMPONENTS =====
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        rectTransform = GetComponent<RectTransform>();
    }

    public void Show(RecipeData recipe)
    {
        gameObject.SetActive(true);

        // Setup content
        cocktailImage.sprite = recipe.recipeImage;
        recipeNameText.text = recipe.recipeName;

        StartCoroutine(RevealAnimation());
    }

    IEnumerator RevealAnimation()
    {
        canvasGroup.alpha = 0f;
        rectTransform.localScale = Vector3.zero;

        float duration = 0.8f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            float scale = EaseOutBack(t);

            canvasGroup.alpha = t;
            rectTransform.localScale = Vector3.one * scale;

            yield return null;
        }

        canvasGroup.alpha = 1f;
        rectTransform.localScale = Vector3.one;

        if (sparkles != null)
            sparkles.Play();
    }

    // bounce effect
    float EaseOutBack(float t)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1f;

        return 1f + c3 * Mathf.Pow(t - 1f, 3f) + c1 * Mathf.Pow(t - 1f, 2f);
    }
}