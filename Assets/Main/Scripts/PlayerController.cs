using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator player_animator;
    private BoxCollider2D player_boxCollider;
    private Rigidbody2D player_rb2D;

    private bool isCrouch;
    private bool canJump;
    public int jumpsRemaining;
    private bool isGrounded;

    private bool isDead;
    public bool IsDead { get { return isDead; } }


    [Header("Hitbox")]
    [SerializeField] private Vector2 standingColliderOffset;
    [SerializeField] private Vector2 standingColliderSize;
    [SerializeField] private Vector2 crouchColliderOffset;
    [SerializeField] private Vector2 crouchColliderSize;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private int maxJumps;


    private void Awake()
    {
        player_rb2D = GetComponent<Rigidbody2D>();
        player_boxCollider = GetComponent<BoxCollider2D>();
        player_animator = GetComponent<Animator>();
    }

    private void Start()
    {
        jumpsRemaining = maxJumps;
    }


    void Update()
    {
        float moveValue = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonUp("Jump") && !isCrouch)
        {
            if (jumpsRemaining > 0)
            {
                --jumpsRemaining; // reduced on double jump
                canJump = true;
                isGrounded = false;
            }
            else
            {
                canJump = false;
            }
        }
        MovePlayer(moveValue);
    }

    private void FixedUpdate()
    {
        Jump();
    }

    private void Jump()
    {
        if (canJump)
        {
            player_rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
            canJump = false;
        }
    }

    private void MovePlayer(float xAxis)
    {
        transform.position += Vector3.right * xAxis * speed * Time.deltaTime;
    }



   

}
