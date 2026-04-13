using UnityEngine;

public class Brick : MonoBehaviour
{
    static int count = 0;
    [SerializeField] int health = 1;

    [SerializeField] AudioClip breakSound;

    private void Awake()
    {
        count++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        health--;
        AudioSource.PlayClipAtPoint(breakSound, transform.position);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        count--;
        Debug.Log($"Ramaining blocks {count}");
    }
}
