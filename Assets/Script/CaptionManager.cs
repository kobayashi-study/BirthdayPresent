using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CaptionManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI captionText;
    [SerializeField] GameObject bottomBar;
    private float fadeDuration = 1f;
    private float displayTime = 3f;
    public IEnumerator ShowCaption(string massage)
    {
        yield return StartCoroutine(FadeText(massage));
    }

    IEnumerator FadeText(string massage) 
    {
        captionText.text = massage;
        Color color = captionText.color;
        color.a = 0f;
        captionText.color = color;

        for (float i = 0; i < fadeDuration; i += Time.deltaTime) 
        {
            color.a = Mathf.Lerp(0, 1, i / fadeDuration);
            captionText.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(displayTime);

        for (float i = 0; i < fadeDuration; i += Time.deltaTime)
        {
            color.a = Mathf.Lerp(1, 0, i / fadeDuration);
            captionText.color = color;
            yield return null;
        }
    }

    public IEnumerator ExtendBar(float endSize, float duration)
    {
        yield return StartCoroutine(ExtendBarCoroutine(endSize, duration));
    }

    IEnumerator ExtendBarCoroutine(float endSize, float duration) 
    {
        RectTransform rt = bottomBar.GetComponent<RectTransform>();
        float elapsed = 0f;
        float startSize = rt.sizeDelta.y;

        while (elapsed < duration) 
        {
            float t = elapsed / duration;
            float currentSize = Mathf.Lerp(startSize, endSize, t);
            Vector2 newSize = rt.sizeDelta;
            newSize.y = currentSize;
            rt.sizeDelta = newSize;
            elapsed += Time.deltaTime;
            yield return null;
        }
        Vector2 finalSize = rt.sizeDelta;
        finalSize.y = endSize;
        rt.sizeDelta = finalSize;
        yield return null;
    }
}
