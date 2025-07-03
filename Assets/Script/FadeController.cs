using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [SerializeField] Image image;

    public IEnumerator FadeOut(float duration) 
    {
        yield return StartCoroutine(FadeOutMethod(duration));
    }

    IEnumerator FadeOutMethod(float duration)
    {
        Color color = image.color;
        color.a = 0f;

        for (float t = 0; t < duration; t += Time.deltaTime) 
        {
            color.a = Mathf.Lerp(0, 1, t / duration);
            image.color = color;
            yield return null;
        }
        yield return null;
    }

    public IEnumerator FadeIn(float duration)
    {
        yield return StartCoroutine(FadeInMethod(duration));
    }

    IEnumerator FadeInMethod(float duration)
    {
        Color color = image.color;
        color.a = 255f;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            color.a = Mathf.Lerp(1, 0, t / duration);
            image.color = color;
            yield return null;
        }
        yield return null;
    }
}
