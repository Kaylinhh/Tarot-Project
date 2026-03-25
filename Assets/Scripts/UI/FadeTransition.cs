using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FadeTransition : MonoBehaviour
{
    public static FadeTransition Instance { get; private set; }

    [SerializeField] Image image;
    [SerializeField] AnimationCurve fadeCurve;
    [SerializeField] float fadeDuration;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FadeIn(UnityEvent callback = null)
    {
        StartCoroutine(_FadeAlphaAsync(1, callback));
    }

    public void FadeOut(UnityEvent callback = null)
    {
        StartCoroutine(_FadeAlphaAsync(0, callback));
    }

    IEnumerator _FadeAlphaAsync(float destination, UnityEvent callback)
    {
        Color startValue = image.color;
        float elapsed = 0;
        image.raycastTarget = true;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = fadeCurve.Evaluate(elapsed / fadeDuration);
            Color newColor = new Color(startValue.r, startValue.g, startValue.b, Mathf.Lerp(startValue.a, destination, t));
            image.color = newColor;
            yield return null;
        }

        image.color = new Color(startValue.r, startValue.g, startValue.b, destination);

        if (destination <= 0)
        {
            image.raycastTarget = false;
        }

        callback?.Invoke();
    }
}