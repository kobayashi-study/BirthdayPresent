using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneratorController : MonoBehaviour
{
    [SerializeField] private GameObject enemyGenerator1;
    [SerializeField] private GameObject enemyGenerator2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.enabled) return;
        if (other.CompareTag("Player"))
        {
            enemyGenerator1.SetActive(true);
            enemyGenerator2.SetActive(true);
            Debug.Log("ìGê∂ê¨äJén");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.enabled) return;
        if (other.CompareTag("Player"))
        {
            enemyGenerator1.SetActive(false);
            enemyGenerator2.SetActive(false);
            Debug.Log("ìGê∂ê¨í‚é~");
        }
    }
}
