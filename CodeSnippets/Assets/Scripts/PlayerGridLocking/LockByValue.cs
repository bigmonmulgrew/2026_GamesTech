using UnityEngine;
using UnityEngine.InputSystem;

public class LockByValue : MonoBehaviour
{
    // This version locks the player to an axis. But if there are colliders or other objects in the way they will stop when hitting them

    [SerializeField] float gridSize = 1.0f;
    [SerializeField] float moveSpeed = 10.0f;
    [SerializeField] float gridAlignSpeed = 5.0f;

    Rigidbody2D rb;
    InputSystem_Actions playerInputs;
    InputAction moveInput;

    private void Awake()
    {
        // Find the rigidbody and cache it so we don't need to keep searching for it.
        rb = GetComponent<Rigidbody2D>();

        // Setup player inputs.
        playerInputs = new InputSystem_Actions();
        moveInput = playerInputs.Player.Move;
    }
    private void OnEnable()
    {
        // Enable player inputs
        playerInputs.Enable();
    }
    private void OnDisable()
    {
        // Disable player inputs
        playerInputs.Disable();
    }
    // Fixed Update runs on the physics tick, rather than every frame. Default is 50fps
    private void FixedUpdate()
    {
        MovePlayer();
    }
    void MovePlayer()
    {
        // First get direction from inputs
        Vector2 moveDirection = moveInput.ReadValue<Vector2>();

        // This exits early if input is 0,0 skipping the checks.
        if (moveDirection == Vector2.zero) return;

        // Find the position of the current object to use in calculations
        Vector2 position = transform.position;

        // First choose which axis to move on. X or Y. If both are pressed we prefer x, horizontal.
        // You may want to consider adding logic that tracks button order so you could chose to make
        // the latest input take priority or make the first input take priority.
        bool movingHorizontal = Mathf.Abs(moveDirection.x) >= Mathf.Abs(moveDirection.y);

        if (movingHorizontal)
        {
            // Move in X axis
            
            // Add to the x position
            position.x += moveDirection.x * moveSpeed * Time.fixedDeltaTime;

            // Round based on grid size to find target Y position
            float targetY = Mathf.Round(position.y / gridSize) * gridSize;

            
            position.y = Mathf.MoveTowards(position.y, targetY, gridAlignSpeed * Time.deltaTime);
        }
        else
        {
            // Move in y axis

            // Add to the y position
            position.y += moveDirection.y * moveSpeed * Time.fixedDeltaTime;

            // Round based on grid size to find target X position
            float targetX = Mathf.Round(position.x / gridSize) * gridSize;

            // Move towards the grid locked x value.
            position.x = Mathf.MoveTowards(position.x, targetX, gridAlignSpeed * Time.fixedDeltaTime);
        }


        // This applies the calculated move direaction.
        rb.MovePosition(position);
    }
}
