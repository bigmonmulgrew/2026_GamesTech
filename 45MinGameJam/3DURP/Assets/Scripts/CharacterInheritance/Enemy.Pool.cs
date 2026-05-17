using UnityEngine;
using UnityEngine.Pool;
using System.Collections;
using System.Collections.Generic;

public partial class Enemy : Character
{
    
    static ObjectPool<Enemy> pool;
    //  This can be set as a default on the script but will be hidden in the inspector. It is used to create new instances for the pool so must be common across all instances of the Enemy class.
    [SerializeField, HideInInspector] Enemy prefab;

    static Enemy Prefab;
    static GameObject poolHolder;
    bool isReleased = false;
    
    private void CreateObjectPool()
    {
        if (pool != null)
        {
            Release(this);
            return;   // Only create the pool once, even if multiple Enemies are created.
                      
        }

        if (prefab == null)
        {
            Debug.LogError("Enemy prefab reference is missing! Please assign it to the script file as a default.");
            return;
        }

        Prefab = prefab;

        poolHolder = new GameObject("Enemy Pool");

        // Create a pool with the four core callbacks.
        pool = new ObjectPool<Enemy>(
            createFunc: CreateEnemy,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: OnDestroyItem,
            collectionCheck: true,   // helps catch double-release mistakes
            defaultCapacity: 10,
            maxSize: 50
        );


        // Using this method requires 1 enemy to exist on scene load, or to be manually spawned elsewhere
        Release(this);   // Add the current enemy to the pool

    }

    static Enemy CreateEnemy()
    {
        Enemy newEnemy = Instantiate(Prefab);
        newEnemy.gameObject.SetActive(false);

        if(poolHolder) newEnemy.transform.SetParent(poolHolder.transform);

        newEnemy.name = $"{Prefab.name} {pool.CountAll}";

        return newEnemy;

    }

    public static Enemy SpawnEnemy(Vector3 position, Quaternion rotation)
    {
        Enemy enemy = pool.Get();
        enemy.transform.parent = null;    // Detach from pool holder so it can be organized in the scene as needed.
        enemy.transform.position = position;
        enemy.transform.rotation = rotation;
        
        return enemy;
    }

   
    // Called when an item is taken from the pool.
    static void OnGet(Enemy enemy)
    {
        enemy.isReleased = false;
        enemy.gameObject.SetActive(true);
    }

    public static void Release(Enemy enemy)
    {
        if (enemy == null) return;
        if (enemy.isReleased) return;

        enemy.isReleased = true;
        pool.Release(enemy);
    }
    // Called when an item is returned to the pool.
    static void OnRelease(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        if (poolHolder) enemy.transform.SetParent(poolHolder.transform); 
        enemy.transform.position = Vector3.zero;            // Reset position or any other state as needed.
        enemy.transform.rotation = Quaternion.identity;

    }

    // Called when the pool decides to destroy an item (e.g., above max size).
    static void OnDestroyItem(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }

    static IEnumerator ReturnAfter(Enemy enemy, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        // Give it back to the pool.
        Release(enemy);
    }
}