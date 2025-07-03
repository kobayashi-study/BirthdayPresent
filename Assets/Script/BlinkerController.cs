using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkerController : MonoBehaviour
{
    private Renderer renderer;
    private Coroutine blinkerCoroutine; 
    private float onTime = 0.8f;
    private float offTime = 0.5f;
    void Start()
    {
        renderer = GetComponent<Renderer>();
        blinkerCoroutine = StartCoroutine(Blinker());
    }

    IEnumerator Blinker() 
    {
        while (true) 
        {
            renderer.enabled = !renderer.enabled;
            yield return new WaitForSeconds(offTime);
            renderer.enabled = !renderer.enabled;
            yield return new WaitForSeconds(onTime);
        }
    }

    public void SetInterval(float onTime, float offTime) 
    {
        this.onTime = onTime;
        this.offTime = offTime;
        ResetBlinker();
    }

    private void ResetBlinker() 
    {
        if (blinkerCoroutine != null) 
        {
            StopCoroutine(blinkerCoroutine);
        }
        blinkerCoroutine = StartCoroutine (Blinker());
    }
}
