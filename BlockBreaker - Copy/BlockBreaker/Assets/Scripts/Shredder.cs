using UnityEngine;

public class Shredder : MonoBehaviour
{
    [SerializeField] Ball ballPrefab;
    [SerializeField] Vector2 spawnPosition;

    private void Start()
    {
        SpawnBall();
    }

    void SpawnBall()
    {
        Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);

        if (collision.gameObject.GetComponent<Ball>())
        {
            SpawnBall();
        }
    }
}
