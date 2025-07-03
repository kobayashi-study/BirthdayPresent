using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] float scrollSpeed;
    [SerializeField] float deadLine;
    private Vector2 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(scrollSpeed * Time.deltaTime, 0, 0);

        if (this.transform.position.x < deadLine) 
        {
            this.transform.position = startPos;
        }
    }
}
