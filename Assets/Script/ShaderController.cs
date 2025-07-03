using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ShaderController : MonoBehaviour
{
    [SerializeField] Material mosaicMaterial;
    private float startSize = 20f;
    private float endSize = 1f;
    public IEnumerator MosaicOut(float duration)
    {
        yield return StartCoroutine(MosaicOutCoroutine(duration));
    }

    IEnumerator MosaicOutCoroutine(float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration) 
        {
            float t = elapsed / duration;
            float currentSize = Mathf.Lerp(startSize, endSize, t);
            mosaicMaterial.SetFloat("_BlockSize", currentSize);

            elapsed += Time.deltaTime;
            yield return null;
        }
        mosaicMaterial.SetFloat("_BlockSize", endSize);
    }

    public void ResetMosaic() 
    {
        mosaicMaterial.SetFloat("_BlockSize", 20);
    }
}
