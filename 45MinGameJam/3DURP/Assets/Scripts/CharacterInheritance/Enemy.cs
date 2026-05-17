using UnityEngine;

public partial class Enemy : Character
{

    static int enemyCount;
    public static int EnemyCount => enemyCount;

    void Awake()
    {
        CreateObjectPool();
        
    }
    private void OnEnable()
    {
        enemyCount++;
    }
    private void OnDisable()
    {
        enemyCount--;
    }
    protected override void Update()
    {
        GetMoveInput();
        base.Update();
    }
    void GetMoveInput()
    {
        if (Player.Instance == null)
        {
            moveInputDirection = Vector3.zero;
            return;
        }

        Vector3 directionToPlayer = Player.Instance.transform.position - transform.position;

        moveInputDirection = directionToPlayer.normalized;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Projectile"))
        {
            Enemy.Release(this);
        }
    }
}