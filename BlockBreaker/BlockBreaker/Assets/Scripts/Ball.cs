using UnityEngine;
using UnityEngine.InputSystem;

public class Ball : MonoBehaviour
{
    [SerializeField] InputAction launchButton;
    [SerializeField] Vector2 launchPower;
    [SerializeField] AudioClip bounceSound;
    bool isLaunched = false;

    private void OnEnable()
    {
        launchButton.Enable();
    }

    private void OnDisable()
    {
        launchButton.Disable();
    }
    void Update()
    {
        if (isLaunched) return;


        if (launchButton.WasPressedThisFrame())
        {
            isLaunched = true;
            GetComponent<Rigidbody2D>().AddForce(launchPower, ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(bounceSound, transform.position);
    }
}
