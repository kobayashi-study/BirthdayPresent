using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] LayerMask blockLayer;
    [SerializeField] GameObject deathEffect;
    public enum DIRECTION_TYPE
    {
        STOP,
        RIGHT,
        LEFT,
    }

    DIRECTION_TYPE direction = DIRECTION_TYPE.STOP;
    Rigidbody2D rigidbody2D;
    float speed = 0;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        direction = DIRECTION_TYPE.RIGHT;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGround()) 
        {
            ChangeDircetion();   
        }
    }

    private void FixedUpdate()
    {
        //directionの中のDIRECTION_TYPEによってプレイヤーの移動させる。
        switch (direction)
        {
            case DIRECTION_TYPE.STOP:
                speed = 0;
                break;
            case DIRECTION_TYPE.RIGHT:
                transform.localScale = new Vector3(1, 1, 1);
                speed = 3;
                break;
            case DIRECTION_TYPE.LEFT:
                transform.localScale = new Vector3(-1, 1, 1);
                speed = -3;
                break;
        }
        rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
    }

    bool isGround()
    {
        Vector3 startVec = transform.position + transform.right * 0.5f * transform.localScale.x;
        Vector3 endVec = startVec - transform.up * 2.0f;
        Debug.DrawLine(startVec, endVec);
        return Physics2D.Linecast(startVec, endVec, blockLayer);
    }

    void ChangeDircetion()
    {
        if (direction == DIRECTION_TYPE.RIGHT)
        {
            direction = DIRECTION_TYPE.LEFT;
        }
        else if (direction == DIRECTION_TYPE.LEFT)
        {
            direction = DIRECTION_TYPE.RIGHT;
        }
    }

    public void DestroyEnemy() 
    {
        Instantiate(deathEffect, this.transform.position,this.transform.rotation);
        Destroy(this.gameObject);
    }
}
