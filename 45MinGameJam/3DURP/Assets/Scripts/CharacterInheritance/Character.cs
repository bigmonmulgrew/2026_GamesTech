using UnityEngine;

public class Character : MonoBehaviour
{
    #region Configuration
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileForce = 50f;
    [SerializeField] float turnRate = 10f;

    #endregion
    AudioSource audioSource;
    Animator animator;
    #region Runtime Variables
    protected Vector3 moveInputDirection;
    protected Vector3 lookInputDirection; // World space rotation for look input
    protected bool attackPressed;
    protected bool jumpPressed;
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        Move();
        Attack();
        Jump();
        Look();
    }
    void Look()
    {
        if(lookInputDirection == Vector3.zero) return;   // No look input, skip rotation

        //Rotate towards the look input direction over time for smooth rotation
        Quaternion targetRotation = Quaternion.LookRotation(lookInputDirection);
        targetRotation = transform.rotation * targetRotation;   
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnRate);

    }
    void Move()
    {
        if (moveInputDirection == Vector3.zero) return;   

        Vector3 moveDirection = moveInputDirection;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
    void Attack()
    {
        if (!attackPressed) return;
        attackPressed = false;   // Reset the attack input so we only attack once per press

        animator?.SetTrigger("FireGun");
        if (audioSource != null)
        {
            audioSource.Play();
        }
        Debug.Log($"{gameObject.name} Attacking");

        GameObject projectile = Instantiate(projectilePrefab, transform.position + transform.forward, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
                rb.AddForce(transform.forward * projectileForce, ForceMode.Impulse);
        }
    }
    void Jump()
    {
        if (!jumpPressed) return;
        jumpPressed = false;   // Reset the jump input so we only jump once per press
        Debug.Log($"{gameObject.name} Jumping");

    }

}
