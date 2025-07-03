using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndrollController : MonoBehaviour
{
    float duration = 40f;
    public IEnumerator Endroll() 
    {
        yield return StartCoroutine(EndrollCoroutine());
    }

    IEnumerator EndrollCoroutine() 
    {
        Vector2 startPos = this.transform.position;
        Vector2 targetPos = new Vector2(this.transform.position.x, 2800);
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            this.transform.position = Vector2.Lerp(startPos, targetPos, t);
            yield return null;
        }
        this.transform.position = targetPos;
        yield return null;
    }
}
