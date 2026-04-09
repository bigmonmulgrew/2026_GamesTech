using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] InputAction winButton;
    [SerializeField] InputAction loseButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        winButton.Enable();
        loseButton.Enable();
    }
    private void OnDisable()
    {
        winButton.Disable();
        loseButton.Disable();
    }
    private void Update()
    {
        if (winButton.triggered)
        {
            LevelManager.Instance.LoadWinScreen();
        }
        if (loseButton.triggered)
        {
            LevelManager.Instance.LoadGameOver();
        }
    }
}
