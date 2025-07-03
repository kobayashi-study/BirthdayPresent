using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEnemyManager : MonoBehaviour
{
    [SerializeField] GameObject deathEffect;
    [SerializeField] float floatSpeed = 1.5f;
    [SerializeField] float minY = -0.3f;
    [SerializeField] float maxY = 0.3f;
    [SerializeField] bool flipX = false;
    Rigidbody2D rb;
    float baseY;
    float timeCounter = 0f;

    // Start is called before the first frame update
    /*
    void Start()
    {
        baseY = transform.position.y;

        // å¸Ç´ÇÃê›íË
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = flipX;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime * floatSpeed;
        float offsetY = Mathf.Sin(timeCounter) * (maxY - minY) / 2f;
        transform.position = new Vector3(transform.position.x, baseY + offsetY, transform.position.z);
    }
    */
    void Start()
    {
        baseY = transform.position.y;
        rb = GetComponent<Rigidbody2D>();

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = flipX;
        }
    }

    void FixedUpdate()
    {
        timeCounter += Time.fixedDeltaTime * floatSpeed;
        float offsetY = Mathf.Sin(timeCounter) * (maxY - minY) / 2f;
        Vector2 newPosition = new Vector2(transform.position.x, baseY + offsetY);
        rb.MovePosition(newPosition);
    }
    public void DestroyEnemy()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
        }
        Destroy(this.gameObject);
    }
}
