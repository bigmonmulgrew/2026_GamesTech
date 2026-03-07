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
            Decision1Made();
        }
        if (inputAction2.WasPressedThisFrame())
        {
            Decision2Made();
        }
    }

    private void Decision1Made()
    {
        Debug.Log("Decision 1 made!");
    }

    private void Decision2Made()
    {
        Debug.Log("Decision 2 made!");
    }

    public void Button1Click()
    {
        Decision1Made();
    }
    public void Button2Click()
    {
        Decision2Made();
    }
}
