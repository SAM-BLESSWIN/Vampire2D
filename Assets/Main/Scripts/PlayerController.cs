using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D player_rb2D;
    private Animator player_animator;
    private BoxCollider2D player_boxCollider;
    private bool isDead;
    public bool IsDead { get { return isDead; } }

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float fallMultiplier;

    [Header("LayerMask")]
    [SerializeField] private LayerMask groundLayerMask;

    [Header("Rewinding")]
    [SerializeField] private ToggleLight toggleLight;
    [SerializeField] private RewindController rewind;

    [Header("SlowMotion")]
    [SerializeField] private SlowMotion slowMotion;

    [Header("UI Controller")]
    [SerializeField] private GameoverController gameoverController;


    private void Awake()
    {
        player_rb2D = GetComponent<Rigidbody2D>();
        player_animator = GetComponent<Animator>();
        player_boxCollider = GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        if (isDead) return;
        if (rewind.IsRewinding) return;

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

            if(xAxis !=0)
                SoundManager.Instance.PlayContinuous(SoundTypes.PLAYERMOVE);
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
        SoundManager.Instance.Play(SoundTypes.PLAYERJUMP);
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(player_boxCollider.bounds.center,player_boxCollider.bounds.size,0,Vector2.down,1f,groundLayerMask);
        return hit.collider!=null;
    }

    public void Hurt(bool status)
    {
        player_animator.SetBool("hurt", status);
    }

    public void Dead()
    {
        isDead = true;
        player_animator.SetBool("dead", true);
        SoundManager.Instance.Play(SoundTypes.PLAYERDEATH);

        StartCoroutine(EnableGameoverPanel());
    }

    IEnumerator EnableGameoverPanel()
    {
        yield return new WaitForSecondsRealtime(3);
        gameoverController.GameOver();
    }

    public void OnHitByObstacle()
    {
        SoundManager.Instance.Play(SoundTypes.PLAYERHIT);
        slowMotion.Reset();
        if(rewind.CountLeft >0)
        {
            rewind.RewindTime();
            toggleLight.SwitchToDayView(3);
        }
        else
        {
            Dead();
            slowMotion.enabled = false;
            toggleLight.SwitchToDayView();
        }
    }
}
