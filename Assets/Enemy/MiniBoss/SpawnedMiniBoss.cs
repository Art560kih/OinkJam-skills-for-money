using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnedMiniBoss : MonoBehaviour
{
    [SerializeField] private GameObject miniBossPrefab;
    [SerializeField]private Transform[] miniBossSpawnPoint;
    public int maxMiniBoss = 2;
    protected float spawnInterval = 1f;
    
    private List<GameObject> miniBoss = new List<GameObject>();

    private MenegmentXpBar xpBar;

    private void Start()
    {
        xpBar = GameObject.FindGameObjectWithTag("XpBar").GetComponent<MenegmentXpBar>();
        
        if (miniBossSpawnPoint.Length == 0)
        {
            Debug.LogError("No spawn points assigned!");
        }
    }

    void Update()
    {
        if (miniBoss.Count < maxMiniBoss)
        {
            StartCoroutine(SpawnEnemiesRoutine());
        }
    }


    public IEnumerator SpawnEnemiesRoutine()
    {
        if (!xpBar.isPaused)
        {
            while (miniBoss.Count < maxMiniBoss)
            {
                SpawnEnemy();
                
                miniBoss.RemoveAll(enemy => enemy == null);

                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }
    
    private void SpawnEnemy()
    {
        if (!xpBar.isPaused)
        {
            int randomIndex = Random.Range(0, miniBossSpawnPoint.Length);
            Transform spawnPoint = miniBossSpawnPoint[randomIndex];

            GameObject enemy = Instantiate(miniBossPrefab, spawnPoint.position, Quaternion.identity);

            enemy.tag = "MiniBoss";
            enemy.name = "MiniBoss";
            
            miniBoss.Add(enemy);

            ActivateClone(enemy);
        }
    }
    
    
    void ActivateClone(GameObject clone)
    {

        clone.SetActive(true);


        SpawnedMiniBoss[] spawnedMiniBoss = clone.GetComponents<SpawnedMiniBoss>();
        foreach (SpawnedMiniBoss script in spawnedMiniBoss)
        {
            script.enabled = true;
        }
        
        MovingMiniBoss scriptMove = clone.GetComponent<MovingMiniBoss>();
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
