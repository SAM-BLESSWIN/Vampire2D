using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolMovement : Enemy
{
    private void Start()
    {
        InitializePositions();
        endPosition = transform.position + (Vector3.right * distance);
    }

    private void Update()
    {
        MoveBackAndForth();
    }
}
