using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FadeTransition : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] AnimationCurve fadeCurve;
    [SerializeField] float fadeDuration;
    [SerializeField] UnityEvent onFadeIn;
    [SerializeField] UnityEvent onFadeOut;



    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn() 
    {
        StartCoroutine(_FadeAlphaAsync(1));
    }

    public void FadeOut() 
    {
        StartCoroutine(_FadeAlphaAsync(0));
    }

    IEnumerator _FadeAlphaAsync(float destination)
    {
        Color startValue = image.color;
        float elapsed = 0;
        image.raycastTarget = true;
        while (elapsed < fadeDuration) 
        {
            elapsed += Time.deltaTime;
            Color newColor = new Color(startValue.r, startValue.g, startValue.b, Mathf.Lerp(startValue.a, destination, fadeCurve.Evaluate(elapsed/fadeDuration)));
            image.color = newColor;
            yield return null;
        }
        if (destination > 0) 
        {
            onFadeIn?.Invoke();
        } else
        {
            image.raycastTarget = false;
            onFadeOut?.Invoke();
        }
    }
}
