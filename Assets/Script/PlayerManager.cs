using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] LayerMask blockLayer;
    [SerializeField] GameObject deathEffect;
    [SerializeField] AudioClip jumpSE;
    [SerializeField] AudioClip deathSE;
    [SerializeField] public AudioClip itemSE;

    public enum DIRECTION_TYPE
    {
        STOP,
        RIGHT,
        LEFT,
    }

    DIRECTION_TYPE direction = DIRECTION_TYPE.STOP;
    Rigidbody2D rigidbody2D;
    Animator animator;
    public AudioSource audioSource;
    float speed = 0;
    float jumpPower = 500;
    float ceilingHeight = 0;
    int JumpCount = 1;
    bool isDead = false;
    bool isCeiling = false;
    private bool doubleJumpFlg = false;
    public static bool isBlocked = false;
    public static bool isLocked = false;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        doubleJumpFlg = GameManager.wJumpFlg;
        if (GameManager.lastCheckpointPosition != Vector3.zero)
        {
            transform.position = GameManager.lastCheckpointPosition;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isDead || isBlocked || isLocked) return;
        float x = Input.GetAxis("Horizontal");
        animator.SetFloat("speed", Mathf.Abs(x));
        Move(x);
    }

    private void Move(float axis)
    {
        //入力された情報の値に合わせてDIRECTION_TYPEを変更
        if (axis == 0)
        {
            direction = DIRECTION_TYPE.STOP;
        }
        else if (axis > 0)
        {
            direction = DIRECTION_TYPE.RIGHT;
        }
        else if (axis < 0)
        {
            direction = DIRECTION_TYPE.LEFT;
        }

        if (IsGround())
        {
            if (Input.GetKeyDown("space"))
            {
                //animator.SetBool("isJump", true);
                Jump();
            }
            animator.SetBool("isJump", false);
            JumpCount = 1;
        }

        //↓二段ジャンプ
        if (!(IsGround()) && doubleJumpFlg) 
        {
            if (Input.GetKeyDown("space") && JumpCount > 0)
            {
                JumpCount--;
                animator.SetBool("isJump", true);
                Jump();
            }
        }
        //directionの中のDIRECTION_TYPEによってプレイヤーの移動させる。
        switch (direction)
        {
            case DIRECTION_TYPE.STOP:
                speed = 0;
                break;
            case DIRECTION_TYPE.RIGHT:
                speed = 7;
                transform.localScale = new Vector3(1, 1, 1);
                break;
            case DIRECTION_TYPE.LEFT:
                speed = -7;
                transform.localScale = new Vector3(-1, 1, 1);
                break;
        }

        if (isCeiling && Input.GetKey("space"))
        {
            transform.position = new Vector2(transform.position.x, ceilingHeight);

            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
            {
                rigidbody2D.velocity = new Vector2(speed, 0);
            }
            else 
            {
                rigidbody2D.velocity = Vector2.zero;
            }
        }
        else 
        {
            rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
        }
    }

    void Jump() 
    {
        animator.SetBool("isJump", true);
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
        rigidbody2D.AddForce(Vector2.up * jumpPower);
        audioSource.PlayOneShot(jumpSE);
    }

    bool IsGround() 
    {
        //始点と終点を作成
        Vector3 leftStartPoint = transform.position - transform.right * 0.3f + Vector3.up * 0.1f;
        Vector3 rightStartPoint = transform.position + transform.right * 0.3f + Vector3.up * 0.1f;
        Vector3 endPoint = transform.position - Vector3.up * 0.3f;
        Debug.DrawLine(leftStartPoint, endPoint);
        Debug.DrawLine(rightStartPoint, endPoint);
        return Physics2D.Linecast(leftStartPoint, endPoint, blockLayer)
            || Physics2D.Linecast(rightStartPoint, endPoint, blockLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        Transform enemyTransform = collision.gameObject.transform;

        if (this.transform.position.y + 0.2f > enemyTransform.position.y)
        {
            audioSource.PlayOneShot(deathSE);
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
            Jump();

            var enemy = collision.gameObject.GetComponent<EnemyManager>();
            if (enemy != null)
            {
                enemy.DestroyEnemy();
            }
            else
            {
                var floatingEnemy = collision.gameObject.GetComponent<FloatingEnemyManager>();
                if (floatingEnemy != null)
                {
                    floatingEnemy.DestroyEnemy();
                }
            }
        }
        else
        {
            audioSource.PlayOneShot(deathSE);
            Instantiate(deathEffect, new Vector2(this.transform.position.x, this.transform.position.y + 1.0f), this.transform.rotation);
            StartCoroutine(DestroyAfterSound(deathSE.length));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ceiling") 
        {
            isCeiling = true;
            ceilingHeight = transform.position.y;
        }

        if (collision.gameObject.tag == "Trap") 
        {
            audioSource.PlayOneShot(deathSE);
            Instantiate(deathEffect, new Vector2(this.transform.position.x, this.transform.position.y + 1.0f), this.transform.rotation);
            StartCoroutine(DestroyAfterSound(deathSE.length));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ceiling")
        {
            isCeiling = false;
        }
    }

    IEnumerator DestroyAfterSound(float delay)
    {
        isDead = true;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
        gameManager.GameOver();
    }

    public void changeJumpFlg() 
    {
        if (!(doubleJumpFlg))
        {
            doubleJumpFlg = true;
        }
        else 
        {
            doubleJumpFlg = false;
        }
    }

    public void LockInput()
    {
        isLocked = true; 
    }

    public void HideSprite()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
