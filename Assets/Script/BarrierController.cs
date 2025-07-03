using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // 速度をゼロにしてその場に止める
                rb.velocity = Vector2.zero;

                // プレイヤーを壁の外側に強制移動させる
                Vector3 pushBackPosition = new Vector3(transform.position.x - 3f, other.transform.position.y, other.transform.position.z);
                other.transform.position = pushBackPosition;
            }
        }
    }
}
