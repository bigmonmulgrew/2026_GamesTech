using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    [SerializeField] InputAction moveLeft;
    [SerializeField] InputAction moveRight;
    [SerializeField] float moveSpeed;
    [SerializeField] float maxDistance;

    private void OnEnable()
    {
        moveLeft.Enable();
        moveRight.Enable();
    }

    private void OnDisable()
    {
        moveLeft.Disable();
        moveRight.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Limit frame rate to simulate potato PC for demonstration
        // Application.targetFrameRate = 10;
    }

    // Update is called once per frame
    void Update()
    {
        float direction = 0;
        if (moveLeft.IsPressed())
        {
            direction = -moveSpeed;
        }
        if (moveRight.IsPressed())
        {
            direction = moveSpeed;
        }

        Vector3 paddlePosition = transform.position;
        paddlePosition.x += direction * Time.deltaTime;

        paddlePosition.x = Mathf.Clamp(paddlePosition.x, -maxDistance, maxDistance);

        transform.position = paddlePosition;


    }
}
