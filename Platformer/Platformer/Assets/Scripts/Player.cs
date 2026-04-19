using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float dashForce = 10f;
    [SerializeField] float dashDuration = 0.15f;
    [SerializeField] GameObject smokePrefab;
    [SerializeField] float interractionRange = 2f;

    InputSystem_Actions playerInputs;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction dashAction;
    InputAction interactAction;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;

    bool isGrounded;
    bool isDashing;

    float horizontalInput;
    float dashTimer;

    private void Awake()
    {
        playerInputs = new InputSystem_Actions();
        moveAction = playerInputs.Player.Move;
        jumpAction = playerInputs.Player.Jump;
        interactAction = playerInputs.Player.Interact;

        // Change this to Player.Dash if your input actions actually have one
        dashAction = playerInputs.Player.Crouch;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerInputs.Player.Enable();
    }

    private void OnDisable()
    {
        playerInputs.Player.Disable();
    }

    private void Update()
    {
        horizontalInput = moveAction.ReadValue<Vector2>().x;

        Jump();
        Dash();
        UpdateAnimations();
        HandleSpriteDirection();

        if (interactAction.triggered)
        {
            TriggerInteraction();
        }
    }

    void TriggerInteraction()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, interractionRange);
        foreach (var hitCollider in hitColliders)
        {
            IInteractable interactable = hitCollider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
    private void FixedUpdate()
    {
        Move();

        if (isDashing)
        {
            dashTimer -= Time.fixedDeltaTime;
            if (dashTimer <= 0f)
            {
                isDashing = false;
            }
        }
    }

    void Jump()
    {
        if (jumpAction.triggered && isGrounded && !isDashing)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            isGrounded = false;
            animator.SetBool("IsGrounded", false);
            animator.SetTrigger("JumpTrigger");
            Instantiate(smokePrefab, transform.position, Quaternion.identity);
        }
    }

    void Dash()
    {
        if (dashAction.triggered && !isDashing)
        {
            Vector2 inputDirection = moveAction.ReadValue<Vector2>();

            Vector2 dashDirection;

            if (inputDirection.sqrMagnitude > 0.01f)
            {
                dashDirection = inputDirection.normalized;
            }
            else
            {
                dashDirection = spriteRenderer.flipX ? Vector2.left : Vector2.right;
            }

            isDashing = true;
            dashTimer = dashDuration;

            rb.linearVelocity = Vector2.zero;
            rb.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);

            animator.SetTrigger("DashTrigger");

            if (dashDirection.x != 0)
            {
                spriteRenderer.flipX = dashDirection.x < 0;
            }
        }
    }

    private void Move()
    {
        if (isDashing)
        {
            return;
        }

        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }

    void HandleSpriteDirection()
    {
        if (horizontalInput < -0.01f)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontalInput > 0.01f)
        {
            spriteRenderer.flipX = false;
        }
    }

    void UpdateAnimations()
    {
        animator.SetFloat("MoveSpeed", Mathf.Abs(horizontalInput));
        animator.SetBool("IsGrounded", isGrounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
        animator.SetBool("IsGrounded", true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
        animator.SetBool("IsGrounded", false);
    }
}