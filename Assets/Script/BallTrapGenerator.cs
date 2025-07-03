using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrapGenerator : MonoBehaviour
{
    [SerializeField] GameObject ballTrapPrefab;
    [SerializeField] private Vector2 startPos;
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private float interval;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(GenerateBallTrap), 0f, interval);
    }


    private void GenerateBallTrap() 
    {
        GameObject ball = Instantiate(ballTrapPrefab, startPos, Quaternion.identity);  
        BallTrapController ballTrapController = ball.GetComponent<BallTrapController>();
        ballTrapController.SetMoveDirection(moveDirection);
    }
}
