using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] private Vector2 startPos;
    //[SerializeField] private Vector2 moveDirection;
    [SerializeField] private float interval;

    // Start is called before the first frame update
    void OnEnable()
    {
        InvokeRepeating(nameof(GenerateEnemy), 0f, interval);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(GenerateEnemy)); 
    }

    private void GenerateEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, startPos, Quaternion.identity);
        /*EnemyManager enemyManager = enemy.GetComponent<EnemyManager>();
        enemyManager.SetMoveDirection(moveDirection);*/
    }
}
