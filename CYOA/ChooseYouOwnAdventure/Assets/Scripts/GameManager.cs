using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] InputAction inputAction1;
    [SerializeField] InputAction inputAction2;

    void OnEnable()
    {
        inputAction1.Enable();
        inputAction2.Enable();
    }
    void OnDisable()
    {
        inputAction1.Disable();
        inputAction2.Disable();
    }
    private void Update()
    {
        if (inputAction1.WasPressedThisFrame())
        {
            Debug.Log($"Input 1 pressed the button!");
        }
        if (inputAction2.WasPressedThisFrame())
        {
            Debug.Log($"Input 2 pressed the button!");
        }
    }

    public void Button1Click()
    {
        Debug.Log("Button 1 clicked!");
    }
    public void Button2Click()
    {
        Debug.Log("Button 2 clicked!");
    }
}
