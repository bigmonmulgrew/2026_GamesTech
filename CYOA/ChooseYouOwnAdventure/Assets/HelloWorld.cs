using UnityEngine;
using UnityEngine.InputSystem;

public class HelloWorld : MonoBehaviour
{
    [SerializeField] string userName = "Dave";
    [SerializeField] InputAction button1;

    void Start()
    {
        Debug.Log($"Hello, {userName}!");
    }

    void OnEnable()
    {
        button1.Enable();
    }
    void OnDisable()
    {
        button1.Disable();
    }
    private void Update()
    {
        if (button1.WasPressedThisFrame())
        {
            Debug.Log($"{userName} pressed the button!");
        }
    }
}
