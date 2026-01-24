using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLogic : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] public float bulletSpeed = 5f;
    
    private Vector2 moveInput;
    
    private Rigidbody2D rb;
    
    public Collider2D coll;
    
    public Camera mainCamera;

    public float health = 100f;
    
    public bool isDead = false;
    
    private Queue<GameObject> bulletsQueue = new Queue<GameObject>();
    
    public GameObject cloneBulletPrefab;
    public GameObject bulletPrefab;

    public GameObject target;

    public Vector3 mousePos;
    public Vector3 worldPos;
    
    [SerializeField] private float spawnRate = 10f;
    
    private float nextSpawnTime;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();  
    }
    
    private void Update()
    {
        if (!isDead)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            moveInput = new Vector2(horizontal, vertical);

            if (moveInput.magnitude > 1f)
            {
                moveInput.Normalize();
            }
            
            if (Input.GetMouseButton(0))
            {
                if (Time.time >= nextSpawnTime)
                {
                    Move();
                    nextSpawnTime = Time.time + (1f / spawnRate);
                }

            }

        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            Vector2 movement = moveInput * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }
    }

    public void hitOnPlayer(float damageEnemy)
    {
        if (health > 0)
        {
            health -= damageEnemy;
            Debug.Log(health);
            if (health <= 0) isDead = true;
        }
    }

    private void Move()
    {
        cloneBulletPrefab = Instantiate(bulletPrefab, rb.position, Quaternion.identity);
    
        cloneBulletPrefab.tag = "Bullet";
        cloneBulletPrefab.name = "Bullet";
        
        bulletsQueue.Enqueue(cloneBulletPrefab);
    
    }
}
