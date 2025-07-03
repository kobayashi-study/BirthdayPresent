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
                // ���x���[���ɂ��Ă��̏�Ɏ~�߂�
                rb.velocity = Vector2.zero;

                // �v���C���[��ǂ̊O���ɋ����ړ�������
                Vector3 pushBackPosition = new Vector3(transform.position.x - 3f, other.transform.position.y, other.transform.position.z);
                other.transform.position = pushBackPosition;
            }
        }
    }
}
