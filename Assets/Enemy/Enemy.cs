using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] public GameObject target;
    [SerializeField] public float moveSpeed = 1.5f;
    public bool checkColl = true;

    public GameObject[] spawnPoints;
    public GameObject enemyPrefab;
    private GameObject cloneEnemyPrefab;
    
    public int spawnPoint;

    private Random rnd = new Random();
    
    private bool isPrefab = false;
    
    private Rigidbody2D rb;
    
    private Queue<GameObject> enemiesQueue = new Queue<GameObject>();

    
    public float health = 30f;

    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("PointsSpawnEnemies");
        
        StartCoroutine(SpawnEnemies());
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (health > 0f)
        {
            if (!checkColl)
            {
                if (target != null)
                {
                    Vector2 direction = (target.transform.position - transform.position).normalized;
                    transform.Translate(direction * moveSpeed * Time.deltaTime);
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            health -= bullet.damage;
        }
    }
    
    void ActivateClone(GameObject clone)
    {

        clone.SetActive(true);
        transform.parent = clone.transform;
        

        Enemy[] scripts = clone.GetComponents<Enemy>();
        foreach (Enemy script in scripts)
        {
            script.enabled = true;   
        }

        Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = true;
            rb.isKinematic = true;
        }

        Collider2D[] colliders = clone.GetComponents<Collider2D>();
        foreach (Collider2D col in colliders)
        {
            col.enabled = true;
        }

        SpriteRenderer sr = clone.GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = true;

        NewBehaviourScript locker = clone.GetComponent<NewBehaviourScript>();
        if (locker != null)
        {
            Destroy(locker);
        }
    }

    private IEnumerator SpawnEnemies()
    {

        
        yield return new WaitForSeconds(1.5f);
        
        spawnPoint = rnd.Next(0, spawnPoints.Length);
        
        Vector3 position = new Vector3(spawnPoints[spawnPoint].transform.position.x, spawnPoints[spawnPoint].transform.position.y, spawnPoints[spawnPoint].transform.position.z);
        
        cloneEnemyPrefab = Instantiate(enemyPrefab, position, Quaternion.identity);
        
        cloneEnemyPrefab.tag = "Enemy"; 
        cloneEnemyPrefab.name = "Enemy";
        
        ActivateClone(cloneEnemyPrefab);
        
        enemiesQueue.Enqueue(cloneEnemyPrefab);
        


    }
    

}
