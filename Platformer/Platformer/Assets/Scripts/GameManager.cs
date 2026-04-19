using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    InputSystem_Actions debugInputs;
    InputAction winButton;
    InputAction loseButton;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
            
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        debugInputs = new InputSystem_Actions();
        winButton = debugInputs.Debugging.WinLevel;
        loseButton = debugInputs.Debugging.LoseLevel;
    }
    private void OnEnable()
    {
        debugInputs?.Enable();
    }
    private void OnDisable()
    {
        debugInputs?.Disable();
    }
    private void Update()
    {
        if (winButton.triggered) LevelManager.Instance.LoadWinScreen();
        
        if (loseButton.triggered) LevelManager.Instance.LoadGameOver();
    }
}

