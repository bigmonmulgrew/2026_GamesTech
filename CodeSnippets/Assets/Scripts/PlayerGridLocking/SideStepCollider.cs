using UnityEngine;
using UnityEngine.InputSystem;

public class SideStepCollider : MonoBehaviour
{
    // This version locks movement to a grid.
    // If a collider blocks the intended direction, the player sidesteps perpendicular to the collider instead.
    // This keeps rb.MovePosition while avoiding diagonal movement through objects.
    //
    // Improvement option:
    // Add a LayerMask later if you want to control what blocks movement.

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
        // Enable player inputs.
        playerInputs.Enable();
    }

    private void OnDisable()
    {
        // Disable player inputs.
        playerInputs.Disable();
    }

    // Fixed Update runs on the physics tick, rather than every frame. Default is 50fps
    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        // First get direction from inputs.
        Vector2 moveDirection = moveInput.ReadValue<Vector2>();

        // This exits early if input is 0,0 skipping the checks.
        if (moveDirection == Vector2.zero) return;

        // Find the position of the current object to use in calculations.
        Vector2 position = rb.position;

        // Calculate movement once so all movement this frame uses the same value.
        float moveAmount = moveSpeed * Time.fixedDeltaTime;

        // First choose which axis to move on. X or Y.
        // If both are pressed we prefer X, horizontal.
        // You may want to consider adding logic that tracks button order so you could chose to make
        // the latest input take priority or make the first input take priority.
        bool movingHorizontal = Mathf.Abs(moveDirection.x) >= Mathf.Abs(moveDirection.y);

        Vector2 moveAxis;

        if (movingHorizontal)
        {
            moveAxis = new Vector2(Mathf.Sign(moveDirection.x), 0);
        }
        else
        {
            moveAxis = new Vector2(0, Mathf.Sign(moveDirection.y));
        }

        // Check if the intended movement direction is blocked.
        RaycastHit2D hit = CheckForCollider(moveAxis, moveAmount);

        if (hit.collider == null)
        {
            // If there is no collider in the way, move normally.
            position += moveAxis * moveAmount;

            // Then move the other axis towards the nearest grid line.
            // This keeps the player aligned without teleporting them.
            position = MoveTowardsGrid(position, movingHorizontal);
        }
        else
        {
            // If something blocks the path, sidestep instead of pushing forward.
            // This prevents the player moving closer to the collider.
            position += GetSideStepDirection(hit.collider, movingHorizontal) * moveAmount;
        }

        // This applies the calculated move direction using physics movement.
        rb.MovePosition(position);
    }

    RaycastHit2D CheckForCollider(Vector2 direction, float distance)
    {
        // We use a local array here because performance is not a concern in this example.
        // A reusable class-level array would reduce allocations, but makes the example less clear.
        // In the absecnce of a compelling reasons otherwise, variables should remain in local scope
        RaycastHit2D[] hits = new RaycastHit2D[1];

        // Cast the Rigidbody2D in the movement direction.
        // This checks for any collider in the way.
        // Hits is modified in place.
        rb.Cast(direction, hits, distance);

        return hits[0];
    }

    Vector2 MoveTowardsGrid(Vector2 position, bool movingHorizontal)
    {
        if (movingHorizontal)
        {
            // Round based on grid size to find target Y position.
            float targetY = Mathf.Round(position.y / gridSize) * gridSize;

            // Move towards the grid locked Y value.
            position.y = Mathf.MoveTowards(position.y, targetY, gridAlignSpeed * Time.fixedDeltaTime);
        }
        else
        {
            // Round based on grid size to find target X position.
            float targetX = Mathf.Round(position.x / gridSize) * gridSize;

            // Move towards the grid locked X value.
            position.x = Mathf.MoveTowards(position.x, targetX, gridAlignSpeed * Time.fixedDeltaTime);
        }

        return position;
    }

    Vector2 GetSideStepDirection(Collider2D collider, bool movingHorizontal)
    {
        // Find the direction from the collider centre to the player.
        // This lets us move away from the collider, not towards it.
        //
        // This assumes the collider has a reasonably uniform shape.
        // Polygon colliders or very irregular colliders may behave unpredictably,
        // because the centre may not represent the useful blocking surface.
        Vector2 colliderToPlayer = rb.position - (Vector2)collider.bounds.center;

        if (movingHorizontal)
        {
            // If horizontal movement is blocked, sidestep vertically.
            // Mathf.Sign chooses up or down based on which side of the collider centre the player is on.
            return new Vector2(0, Mathf.Sign(colliderToPlayer.y));
        }
        else
        {
            // If vertical movement is blocked, sidestep horizontally.
            // Mathf.Sign chooses left or right based on which side of the collider centre the player is on.
            return new Vector2(Mathf.Sign(colliderToPlayer.x), 0);
        }
    }
}