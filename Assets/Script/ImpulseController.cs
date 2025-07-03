using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ImpulseController : MonoBehaviour
{
    public CinemachineImpulseSource impulseSource;

    public void Shake(float duration, float interval) 
    {
        StartCoroutine(LoopShake(duration, interval));
    }

    IEnumerator LoopShake(float duration, float interval) 
    {
        float elapsed = 0;
        while (elapsed < duration) 
        {
            impulseSource.GenerateImpulse();
            yield return new WaitForSeconds(interval);
            elapsed += interval;
        }
    }
}
