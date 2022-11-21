using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D player_rb2D;
    private Animator player_animator;
    private bool isDead;
    public bool IsDead { get { return isDead; } }

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private Transform groundCheckPoint;

    [Header("LayerMask")]
    [SerializeField] private LayerMask groundLayerMask;


    private void Awake()
    {
        player_rb2D = GetComponent<Rigidbody2D>();
        player_animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (isDead) return;

        float moveValue = Input.GetAxisRaw("Horizontal");
        MovePlayer(moveValue);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump();
        }

        if(player_rb2D.velocity.y < 0)
        {
            player_rb2D.velocity -= Vector2.down* Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        }
    }


    private void MovePlayer(float direction)
    {
        player_rb2D.velocity = new Vector2(direction * speed , player_rb2D.velocity.y);
        PlayMovementAnimation(direction);
    }

    private void PlayMovementAnimation(float xAxis)
    {
        player_animator.SetBool("jump",!IsGrounded());

        if(IsGrounded())
        {
            player_animator.SetFloat("speed", Mathf.Abs(xAxis));
        }

        Vector3 scale = transform.localScale;

        if (xAxis < 0)
        {
            scale.x = -1 * Mathf.Abs(scale.x);
        }
        else if (xAxis > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
    }

    private void Jump()
    {
        player_rb2D.velocity = Vector2.up * jumpForce ;
    }

    private bool IsGrounded()
    {
        Collider2D isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.1f, groundLayerMask);
        return isGrounded;
    }

    public void Hurt(bool status)
    {
        player_animator.SetBool("hurt", status);
    }

    public void Dead()
    {
        isDead = true;
        player_animator.SetBool("dead", true);
    }
}
