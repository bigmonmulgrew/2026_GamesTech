using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy enemyPrefab;
    
    void Start()
    {
        SpawnEnemy();
    }
    void SpawnEnemy()
    {
        Enemy.SpawnEnemy(transform.position, Quaternion.identity);
    }
}
