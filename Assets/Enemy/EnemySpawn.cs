using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private int maxEnemies = 10;
    
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    
    public Animation animation;
    
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned!");
            return;
        }
        
        StartCoroutine(SpawnEnemiesRoutine());
    }
    
    private IEnumerator SpawnEnemiesRoutine()
    {
        while (true)
        {
            if (spawnedEnemies.Count < maxEnemies)
            {
                SpawnEnemy();
            }
            
            // Удаляем уничтоженных врагов из списка
            spawnedEnemies.RemoveAll(enemy => enemy == null);
            
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    
    void ActivateClone(GameObject clone)
    {

        clone.SetActive(true);


        EnemySpawn[] scripts = clone.GetComponents<EnemySpawn>();
        foreach (EnemySpawn script in scripts)
        {
            script.enabled = true;
        }

        MovingEnemy scriptMove = clone.GetComponent<MovingEnemy>();
        if (scriptMove != null)
        {
            scriptMove.enabled = true;
        }

        Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = true;
            rb.isKinematic = true;
            rb.useFullKinematicContacts = true;
        }
        Animator animator = clone.GetComponent<Animator>();
        if (animator != null)
        {
            animator.enabled = true;
        }

        Collider2D[] colliders = clone.GetComponents<Collider2D>();
        foreach (Collider2D col in colliders)
        {
            col.enabled = true;
            col.isTrigger = true;
        }

        SpriteRenderer sr = clone.GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = true;
        
        StopOrigPrefab locker = clone.GetComponent<StopOrigPrefab>();
        if (locker != null)
        {
            Destroy(locker);
        }
    }
    
    private void SpawnEnemy()
    {
        // Выбираем случайную точку спавна
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];
        
        // Создаем врага
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        
        
        spawnedEnemies.Add(enemy);
        
        ActivateClone(enemy);
        
        Debug.Log($"Spawned enemy at {spawnPoint.position}");
    }
}

