using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private bool touchFlg = true;
    private Vector2 startPos;
    private float startOfset;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        float rangeCenter = (minX + maxX) / 2f;
        float rangeWidth = (maxX - minX) / 2f;
        float relativePos = (startPos.x - rangeCenter) / rangeWidth;
        startOfset = Mathf.Asin(Mathf.Clamp(relativePos, -1f, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        if (touchFlg) 
        {
            float t = (Mathf.Sin(Time.time * speed + startOfset) + 1) / 2;
            float newX = Mathf.Lerp(minX, maxX, t);
            this.transform.position = new Vector2(newX, startPos.y);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform, true);
            touchFlg = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") &&
            collision.transform.parent == transform &&
            gameObject.activeInHierarchy)
        {
            collision.transform.SetParent(null, true);
        }
    }
}
