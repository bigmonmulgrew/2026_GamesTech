using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    DefaultInputActions playerInputs;
    InputAction moveAction;

    Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        playerInputs = new DefaultInputActions();
        moveAction = playerInputs.Player.Move;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
        Vector2 moveDirection = moveAction.ReadValue<Vector2>();
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);

        if(moveDirection.y <= 0)
        {
            animator.SetTrigger("WalkDownTrigger");
        }
        else
        {
            animator.SetTrigger("WalkUpTrigger");
        }
    }
    
}
