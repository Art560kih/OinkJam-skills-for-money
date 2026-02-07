using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawned : MonoBehaviour
{
    [SerializeField] private GameObject bossPrefab;
    [SerializeField]private Transform[] bossSpawnPoint;
    [SerializeField] private int maxBoss = 2;
    private float spawnInterval = 1f;
    
    private List<GameObject> Boss = new List<GameObject>();
    
    private MenegmentXpBar xpBar;

    private void Start()
    {
        xpBar = GameObject.FindGameObjectWithTag("XpBar").GetComponent<MenegmentXpBar>();
        
        if (bossSpawnPoint.Length == 0)
        {
            Debug.LogError("No spawn points assigned!");
        }
    
    }

    void Update()
    {
        if (Boss.Count < maxBoss)
        {
            StartCoroutine(SpawnEnemiesRoutine());
            Debug.Log("Boss spawned" + Boss.Count);
        }
    }


    public IEnumerator SpawnEnemiesRoutine()
    {
        if (!xpBar.isPaused)
        {
            while (true)
            {
                if (Boss.Count < maxBoss)
                {
                    SpawnEnemy();
                }

                Boss.RemoveAll(enemy => enemy == null);

                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }
    
    private void SpawnEnemy()
    {
        if (!xpBar.isPaused)
        {
            Transform spawnPoint = bossSpawnPoint[0];

            GameObject enemy = Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);

            enemy.tag = "Boss";
            enemy.name = "Boss";
            
            Boss.Add(enemy);

            ActivateClone(enemy);
        }
    }
    
    
    void ActivateClone(GameObject clone)
    {

        clone.SetActive(true);


        BossSpawned[] spawnedBoss = clone.GetComponents<BossSpawned>();
        foreach (BossSpawned script in spawnedBoss)
        {
            script.enabled = true;
        }
        
        MovingBoss scriptMove = clone.GetComponent<MovingBoss>();
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
}
