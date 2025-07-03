using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    [SerializeField] private Vector3 respawnPosition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.lastCheckpointPosition = transform.position + respawnPosition;
            Debug.Log(GameManager.lastCheckpointPosition);
        }
    }

    private void OnDrawGizmos()
    {
        // シーンビューでリスポーン地点が見えるように
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + respawnPosition, 0.2f);
    }
}
