using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Cinemachine.CinemachineVirtualCamera virtualCamera;

    public IEnumerator MoveCamera(Vector3 targetPosition, float duration)
    {
        yield return StartCoroutine(MoveCoroutine(targetPosition, duration));
    }

    private IEnumerator MoveCoroutine(Vector3 targetPosition, float duration) 
    {
        virtualCamera.Follow = null;
        Vector3 startPos = virtualCamera.transform.position;
        float elapsed = 0f;
        while (elapsed < duration) 
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            virtualCamera.transform.position = Vector3.Lerp(startPos, targetPosition, t);
            yield return null;
        }
        virtualCamera.transform.position = targetPosition;
    }

    public IEnumerator ZoomOut(float size, float duration) 
    {
        yield return StartCoroutine(ZoomOutCoroutine(size, duration));
    }

    public IEnumerator ZoomOutCoroutine(float size, float duration)
    {
        float startSize = virtualCamera.m_Lens.OrthographicSize;
        float elapsed = 0f;

        while (elapsed < duration) 
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(startSize, size, t);
            yield return null;
        }
    }
}
