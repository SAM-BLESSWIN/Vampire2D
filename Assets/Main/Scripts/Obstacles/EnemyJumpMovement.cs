using UnityEngine;

public class EnemyJumpMovement : Enemy
{
    private void Start()
    { 
        InitializePositions();
        endPosition = transform.position + (Vector3.up * distance);
    }

    private void Update()
    {
        MoveBackAndForth();
    }
}
