using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLogic : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] public float bulletSpeed = 5f;
    
    public GameObject target;
    
    public float health = 100f;
    
    private Vector2 moveInput;
    
    private Rigidbody2D rb;
    
    public Collider2D coll;
    
    public Camera mainCamera;
    
    public bool isDead = false;
    
    private Queue<GameObject> bulletsQueue = new Queue<GameObject>();
    
    public GameObject cloneBulletPrefab;
    public GameObject bulletPrefab;
    

    public Vector3 mousePos;
    public Vector3 worldPos;

    private float minMovingSpeed = 0.1f;
    private bool isMoving = false;
    
    private Animator animator;
    public SpriteRenderer spriteRenderer;
    
    [SerializeField] private float spawnRate = 10f;
    
    private float nextSpawnTime;
    public GameObject panel;
    
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();  
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        panel.SetActive(false);
    }
    
    private void Update()
    {
        if (isDead)
        {
            Time.timeScale = 0;
            panel.SetActive(true);
            if (Input.GetKeyDown(KeyCode.R))
            {
                health = 100f;
                panel.SetActive(false);
                SceneManager.LoadScene("SampleScene");
                Time.timeScale = 1;
            }
        }
        if(!isDead) Time.timeScale = 1;
  
        
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
            
            animator.SetBool("isMoving", isMoving);

        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            Vector2 movement = moveInput * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }

        if (Mathf.Abs(moveInput.x) > 0.1f || Mathf.Abs(moveInput.y) > 0.1f) isMoving = true; else isMoving = false;
        
    }


    private void Move()
    {
        cloneBulletPrefab = Instantiate(bulletPrefab, rb.position, Quaternion.identity);
    
        cloneBulletPrefab.tag = "Bullet";
        cloneBulletPrefab.name = "Bullet";
        
        bulletsQueue.Enqueue(cloneBulletPrefab);
    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyOrig"))
        {
            StartCoroutine(GetDamageEffect());
        }
    }

    private IEnumerator GetDamageEffect()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
        
        yield return new WaitForSeconds(0.2f);
        
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
    public float TakeDamage
    {
        get => health;
        set
        {
            health = value;
        }
        
    }
    
}
