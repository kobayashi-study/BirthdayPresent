using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrapController : MonoBehaviour
{
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform ballSprite;
    [SerializeField] AudioClip deathSE;
    AudioSource audioSource;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        transform.Translate(moveDirection.normalized * speed * Time.deltaTime, Space.World);

        float rotate = 360f;
        float rotateDirection = (moveDirection.x >= 0) ? -1 : 1;
        transform.Rotate(0, 0, rotateDirection * rotate * Time.deltaTime);
    }

    public void SetMoveDirection(Vector2 direction) 
    {
        moveDirection = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trap") 
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            EnemyManager enemy = collision.gameObject.GetComponent<EnemyManager>();
            audioSource.PlayOneShot(deathSE);
            enemy.DestroyEnemy();
        }
    }
}
