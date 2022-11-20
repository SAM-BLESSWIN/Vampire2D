using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private BoxCollider2D player_boxCollider;
    private Rigidbody2D player_rb2D;
    private bool isDead;
    public bool IsDead { get { return isDead; } }

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float fallMultiplier;

    [Header("LayerMask")]
    [SerializeField] private LayerMask groundLayerMask;


    private void Awake()
    {
        player_boxCollider = GetComponent<BoxCollider2D>();
        player_rb2D = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        float moveValue = Input.GetAxisRaw("Horizontal");

        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump();
        }

        if(player_rb2D.velocity.y < 0)
        {
            player_rb2D.velocity -= Vector2.down* Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        }

        MovePlayer(moveValue);
    }


    private void MovePlayer(float direction)
    {
        player_rb2D.velocity = new Vector2(direction * speed , player_rb2D.velocity.y);
    }

    private void Jump()
    {
        player_rb2D.velocity = Vector2.up * jumpForce ;
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(player_boxCollider.bounds.center, player_boxCollider.bounds.size, 0f, Vector2.down,1f, groundLayerMask);
        return raycastHit2D.collider != null;
    }

}
