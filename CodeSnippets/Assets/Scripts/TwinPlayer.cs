using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class TwinPlayer : MonoBehaviour
{
    // Static references
    static TwinPlayer instanceOne;
    static TwinPlayer instanceTwo;
    static TwinPlayer selectedPlayer;
    static bool wasChangedThisFrame;

    // Local property to return other player. This makes reusing the logic easier.
    // This is conditional assignment, also called a ternary operator.
    // Format is 
    // something = condition ? valueIfTrue : valueIfFalse; 
    // => turns this into a lambda expression so it resolves the condition every time we access OtherPlayer
    static TwinPlayer OtherPlayer => selectedPlayer != instanceOne ? instanceOne : instanceTwo; // Will work if not static, but saves memory

    // Add configuration to the inspector for move speed
    [SerializeField] float movesSpeed = 20f;

    // Store references to the player input object we create and easy access to the imput actions we are using.
    InputSystem_Actions playerInputs;
    InputAction moveInput;
    InputAction swapPlayerInput;

    private void Awake()
    {
        // If statements with a single line do not need brackets {}
        if (!SetupPlayerInstances()) return;    // If we get an error setting up player instances we exit immediately. No further setup needed.
        
        SetupPlayerInputs();
    }
    private void SetupPlayerInputs()
    {
        playerInputs = new();
        moveInput = playerInputs.Player.Move;
        swapPlayerInput = playerInputs.Player.Interact;     // Default binding for is E on keyboard and North button on controller.

    }
    private bool SetupPlayerInstances()
    {
        // We return true when successfully storing an instance. This allows us to exit Awake() early since the rest of it isn't needed.
        // We need to be aware that these can execute in any order.
        // If two players exists on start it is difficult to predict which executes first
        // If two players are instantiated it is difficult to predict which executes first
        
        // If the starting player order matters we can add a setting to define the selected one, or spawn one per frame in a coroutie.
        // In this example I don't care which one has control first.

        if (selectedPlayer == null) selectedPlayer = this;  // Assign the selected player as the first one executed

        if (instanceOne == null)
        {
            Debug.Log($"Assinging {gameObject.name} to instane one ");
            instanceOne = this;
            return true;
        }
        else if (instanceTwo == null)
        {
            Debug.Log($"Assinging {gameObject.name} to instane two ");
            instanceTwo = this;
            return true;
        }
        else
        {
            Debug.LogError("More than two players detected, deleting extras");
            Destroy(gameObject);
        }

        return false;   // This could also be inside the last else block.
                        // But since all code paths must return a value its good practice to have the default fallback at the end.
                        // In our case, true means success, we return false in all other instances.
    }
    private void OnEnable()
    {
        playerInputs.Enable();
    }
    private void OnDisable()
    {
        playerInputs.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        if (this != selectedPlayer) return; // Exit immediately if we are not the selected player

        // If the swap player button is pressed swap the player and then exit immediately.
        // Consider execution order here.
        // If player one executes first and WasPressedThisFrame() is true then the selected player becomes 
        // player two before it executes. Then it executes while WasPressedThisFrame() is still true and swaps back.
        // We need to protect against that.
        if (swapPlayerInput.WasPressedThisFrame() && !wasChangedThisFrame)
        {
            Debug.Log($"Swapping Players from {gameObject.name}");
            selectedPlayer = OtherPlayer;
            wasChangedThisFrame = true;     // Since this is static the first player sets it true, blocking any immediate swap backs.
            return;
        }

        Move();
    }
    // LateUpdate() runs after all objects have ran Update() this allows us to reset wasChangedThisFrame after all players have checked it.
    private void LateUpdate()
    {
        wasChangedThisFrame = false;
    }
    void Move()
    {
        Vector3 moveDirection = moveInput.ReadValue<Vector2>();
        transform.position += moveDirection * movesSpeed * Time.deltaTime;
    }
}
