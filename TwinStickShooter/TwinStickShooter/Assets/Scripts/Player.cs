using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    DefaultInputActions playerInputs;
    InputAction moveAction;

    Rigidbody2D rb;

    private void Awake()
    {
        playerInputs = new DefaultInputActions();
        moveAction = playerInputs.Player.Move;
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {        
       playerInputs.Player.Enable();
    }


    void Update()
    {
        Move();
    }

    private void Move()
    {
        rb.MovePosition(rb.position + moveAction.ReadValue<Vector2>() * moveSpeed * Time.deltaTime);
    }
    
}
