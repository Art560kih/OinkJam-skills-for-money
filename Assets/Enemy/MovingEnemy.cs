using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class MovingEnemy : MonoBehaviour
{
    [Header("Настройки")]
    public float health = 30f;
    public float moveSpeed = 3f;
    public float damage = 10f;
    
    [Header("Ссылки")]
    public GameObject target;
    private Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    
    private Animator animator;
    private bool isRunning = true;

    private float attackRange = 1f;
    private bool canAttack = true;
    private float cooldown = 1f;
    
    public bool isDead = false;

    private PlayerLogic playerLogic;

    private ManegementHpBar hpBar;
    
    public Transform player;

    private MenegmentXpBar xpBar;
    
    public SpriteRenderer spriteRendererXp;
    public TMPro.TextMeshProUGUI xpbarText;
    
    public CardFireBall cardFireBall;
    public CardColdBall cardColdBall;
    public CardToxicBall cardToxicBall;

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

        if (target == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange && canAttack)
        {
            Attack();
        }
        
    }

    private void MoveTowardsTarget()
    {
        if (target == null) return;

        if (!xpBar.isPaused)
        {
            Vector2 direction = (target.transform.position - transform.position).normalized;

            transform.Translate(direction * moveSpeed * Time.deltaTime);

            if (direction.x != 0 && spriteRenderer != null)
            {
                spriteRenderer.flipX = direction.x > 0;
            }
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
        if (collision.CompareTag("Enemy") && cardFireBall.burning)
        {
            MovingEnemy movingEnemy = GetComponent<MovingEnemy>();
            
            StartCoroutine(cardFireBall.Burning(movingEnemy));
            StartCoroutine(EffectBurning());
        }
        
        if (collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
                Destroy(collision.gameObject);
            }
        }

        if (collision.CompareTag("BulletFireBall"))
        {
            Bullet bulletFireBall = collision.GetComponent<Bullet>();
            MovingEnemy movingEnemy = GetComponent<MovingEnemy>();
            
            if (bulletFireBall != null)
            {
                Debug.Log("Burning");
                
                TakeDamage(bulletFireBall.damage);
                
                StartCoroutine(cardFireBall.Burning(movingEnemy));
                
                StartCoroutine(EffectBurning());
                
                Destroy(collision.gameObject);
                
            }
        }

        if (collision.CompareTag("BulletAntiMateria"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
                Destroy(collision.gameObject);
            }
        }

        if (collision.CompareTag("BulletColdBall"))
        {
            Bullet bulletColdBall = collision.GetComponent<Bullet>();
            MovingEnemy movingEnemy = GetComponent<MovingEnemy>();
            
            if (bulletColdBall != null)
            {
                Debug.Log("Glaciation");
                
                TakeDamage(bulletColdBall.damage);
                
                StartCoroutine(cardColdBall.Glaciation(movingEnemy));
                
                StartCoroutine(EffectGlaciation());
                
                Destroy(collision.gameObject);
                
            }
        }

        if (collision.CompareTag("BulletToxicBall"))
        {
            Bullet bulletColdBall = collision.GetComponent<Bullet>();
            MovingEnemy movingEnemy = GetComponent<MovingEnemy>();
            
            if (bulletColdBall != null)
            {
                Debug.Log("Poisoning");
                
                TakeDamage(bulletColdBall.damage);
                
                StartCoroutine(cardToxicBall.Poisoning(movingEnemy));
                
                StartCoroutine(EffectPoisoning());
                
                Destroy(collision.gameObject);
                
            }
        }

    }

    private IEnumerator EffectPoisoning()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 3; i++)
        {
            spriteRenderer.color = new  Color(91f/255f, 191f/255f, 24f/255f, 1f);
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
        
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator EffectGlaciation()
    {
        yield return new WaitForSeconds(1f);
        
        spriteRenderer.color = Color.blue;
        yield return new WaitForSeconds(3f);
        spriteRenderer.color = Color.white;
        
        
    }
    
    private IEnumerator EffectBurning()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 3; i++)
        {
            spriteRenderer.color = new Color(255f/255f, 129f/255f, 1f/255f, 1f);
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
        
            yield return new WaitForSeconds(1f);
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
