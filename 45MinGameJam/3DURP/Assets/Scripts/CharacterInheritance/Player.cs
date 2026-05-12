using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    static Player instance;

    [SerializeField] bool cameraRelativeMovement = true;

    public static Player Instance => instance;

    InputSystem_Actions playerInputs;
    InputAction moveInput;
    InputAction lookInput;
    InputAction attackInput;
    InputAction jumpInput;

    private void Awake()
    {
        instance = this;
        playerInputs = new();
        moveInput = playerInputs.Player.Move;
        attackInput = playerInputs.Player.Attack;
        jumpInput = playerInputs.Player.Jump;
        lookInput = playerInputs.Player.Look;
    }
    private void OnEnable()
    {
        playerInputs.Enable();

    }
    private void OnDisable()
    {
        playerInputs.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        //Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    protected override void Update()
    {
        GetMoveInput();
        GetAttackInput();
        GetJumpInput();
        GetLookInput();
        base.Update();
    }
    void GetMoveInput()
    {
        Vector2 input = moveInput.ReadValue<Vector2>();
        moveInputDirection = new Vector3(input.x, 0, input.y);
        
        if (cameraRelativeMovement) ConvertInputToCameraRelative();

    }
    void GetLookInput()
    {
        Vector2 input = lookInput.ReadValue<Vector2>();
        lookInputDirection = new Vector3(input.x, 0, 0);
    }
    void GetAttackInput() 
    {
        attackPressed = attackInput.triggered;
    }
    void GetJumpInput()
    {
        jumpPressed = jumpInput.triggered;
    }
    void ConvertInputToCameraRelative()
    {
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0; // Flatten the forward vector to the horizontal plane
        forward.Normalize();

        Vector3 right = Camera.main.transform.right;
        right.y = 0; // Flatten the right vector to the horizontal plane
        right.Normalize();
        
        moveInputDirection = moveInputDirection.z * forward + moveInputDirection.x * right;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemy)) 
        {
            Enemy.Release(enemy);
            LevelManager.LoadGameOver();
        }
        if(other.gameObject.TryGetComponent<Exit>(out Exit exit))
        {
            LevelManager.LoadNextLevel();
        }
    }
}
