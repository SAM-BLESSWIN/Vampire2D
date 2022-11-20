using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Move Parameters")]
    [SerializeField] protected float distance;
    [SerializeField] private float speed;


    private Vector3 initialPosition;
    private float lerpValue;

    protected int dir;
    protected Vector3 endPosition;

    public void InitializePositions()
    {
        initialPosition = transform.position;
        dir = 1;
    }

    public void MoveBackAndForth()
    {
        lerpValue += dir * Time.deltaTime * speed;

        if(lerpValue >= 1)
        {
            dir = -1;
        }

        if(lerpValue <= 0)
        {
            dir = 1;
        }

        transform.position = Vector3.Lerp(initialPosition, endPosition, lerpValue);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            Debug.Log("rewind");
        }
    }
}
