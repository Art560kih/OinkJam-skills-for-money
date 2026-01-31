using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class MovingEnemy : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private float health = 30f;
    [SerializeField] private float moveSpeed = 1.5f;
    public float damage = 10f;
    
    [Header("Ссылки")]
    private GameObject target;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    
    private Animator animator;
    private bool isRunning = true;

    private float attackRange = 2f;
    private bool canAttack = true;
    private float cooldown = 1f;
    
    public bool isDead = false;

    private PlayerLogic playerLogic;

    private ManegementHpBar hpBar;
    
    public Transform player;

    private MenegmentXpBar xpBar;
    
    public SpriteRenderer spriteRendererXp;
    public TMPro.TextMeshProUGUI xpbarText;

    private bool canGetXp = true;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerLogic = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLogic>();
        hpBar = GameObject.FindGameObjectWithTag("Slider").GetComponent<ManegementHpBar>();
        xpBar = GameObject.FindGameObjectWithTag("XpBar").GetComponent<MenegmentXpBar>();
        
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        
        if (rb != null)
        {
            rb.gravityScale = 0;
        }
    }
    
    void Update()
    {
        if (health <= 0)
        {
            Die();
            isRunning = false;
            return;
        }
        animator.SetBool("isRunning", isRunning);
        MoveTowardsTarget();

        if(target == null) return;
        
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange && canAttack)
        {
            Attack();
        }
    }

    private void MoveTowardsTarget()
    {
        if (target == null) return;
        
        Vector2 direction = (target.transform.position - transform.position).normalized;
        
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        
        if (direction.x != 0 && spriteRenderer != null)
        {
            spriteRenderer.flipX = direction.x > 0;
        }
    }
    
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        
        StartCoroutine(DamageEffect());
        
        if (health <= 0)
        {
            Die();
        }
    }
    
    private IEnumerator DamageEffect()
    {
        if (spriteRenderer != null)
        {
            Color originalColor = spriteRenderer.color;
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = originalColor;
        }
    }
    
    private void Die()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        }
        
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D col in colliders)
        {
            col.enabled = false;
        }
        
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.simulated = false;
        }
        isDead = true;

        if (canGetXp)
        {
            xpBar.CurrentXp += 1f;

            canGetXp = false;
            
            Invoke(nameof(CooldownXp), cooldown);
        }

        StartCoroutine(CounterXp());
        
        Destroy(gameObject, 1f);

        
    }
    
    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
                Destroy(collision.gameObject);
            }
        }
    }

    private void Attack()
    {
        if (playerLogic.health > 0)
        {
            playerLogic.TakeDamage -= damage;
            
            hpBar.CurrentHp -= damage;
            
            Debug.Log(playerLogic.health);
            canAttack = false;
            Invoke(nameof(CooldownAttack), cooldown);
        }
        else
        {
            playerLogic.isDead = true;
        }
    }

    void CooldownAttack()
    {
        canAttack = true;
    }

    void CooldownXp()
    {
        canGetXp = true;
    }

    private IEnumerator CounterXp()
    {
        spriteRendererXp.enabled = true;
        xpbarText.enabled = true;
        yield return new WaitForSeconds(0.5f);
        
        spriteRendererXp.enabled = false;
        xpbarText.enabled = false;
    }

  
}
