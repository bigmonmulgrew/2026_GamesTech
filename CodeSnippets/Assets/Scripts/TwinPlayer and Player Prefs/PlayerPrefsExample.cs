using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerPrefsExample : MonoBehaviour
{
    [SerializeField] float colourChangeRate = 2.0f;
    // Cache a reference ot the sprite renderer
    SpriteRenderer spriteRenderer;

    // PlayerPrefs keys
    // These are the names that the value will be saved under.
    // We place it here in a constant for easy reuse and to not place "magic strings" in our code
    // Configureation should never be hard coded
    const string R_KEY = "PlayerColor_R";
    const string G_KEY = "PlayerColor_G";
    const string B_KEY = "PlayerColor_B";
    const string A_KEY = "PlayerColor_A";

    InputSystem_Actions playerInputs;
    InputAction moveInput;
    InputAction lookInput;
        
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetupInputs();
        LoadColor();
    }

    private void SetupInputs()
    {
        playerInputs = new();
        moveInput = playerInputs.Player.Move;
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

    void Update()
    {
        // Reads the player inputs for look and move and changes the colour based on values
        SetPlayerColour();
    }

    private void SetPlayerColour()
    {
        Vector2 move = moveInput.ReadValue<Vector2>();
        Vector2 look = lookInput.ReadValue<Vector2>();

        Color oldColor = spriteRenderer.color;
        float red, green, blue, alpha;  // You can declare several variables of the same type on one line like this, usually only best to do this if they are related
        
        red   = oldColor.r;
        green = oldColor.g;
        blue  = oldColor.b;
        alpha = oldColor.a;

        red = red + (move.x * colourChangeRate * Time.deltaTime);
        red = Mathf.Repeat(red, 1); // Simple way to make the float wrap if its bigger then 1 or lower than 0

        green = green + (move.y * colourChangeRate * Time.deltaTime);
        green = Mathf.Repeat(green, 1);

        blue = blue + (look.x * colourChangeRate * Time.deltaTime);
        blue = Mathf.Repeat(blue, 1);

        alpha = alpha + (look.y * colourChangeRate * Time.deltaTime);
        alpha = Mathf.Repeat(alpha, 1);


        Color newColor = new(red, green, blue, alpha);

        spriteRenderer.color = newColor;
    }

    private void LoadColor()
    {
        if (PlayerPrefs.HasKey(R_KEY))
        {
            float r = PlayerPrefs.GetFloat(R_KEY);
            float g = PlayerPrefs.GetFloat(G_KEY);
            float b = PlayerPrefs.GetFloat(B_KEY);
            float a = PlayerPrefs.GetFloat(A_KEY);
            
            spriteRenderer.color = new Color(r, g, b, a);
        }
    }

    private void SaveColor()
    {
        Color c = spriteRenderer.color;

        PlayerPrefs.SetFloat(R_KEY, c.r);
        PlayerPrefs.SetFloat(G_KEY, c.g);
        PlayerPrefs.SetFloat(B_KEY, c.b);
        PlayerPrefs.SetFloat(A_KEY, c.a);

        PlayerPrefs.Save();
    }

    void OnApplicationQuit()
    {
        SaveColor();
    }

    void OnDestroy()
    {
        SaveColor();
    }
}