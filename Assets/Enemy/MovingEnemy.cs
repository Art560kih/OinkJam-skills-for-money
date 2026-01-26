using System;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private float health = 30f;
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float damage = 10f;
    
    [Header("Ссылки")]
    private GameObject target;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    
    private Animator animator;
    private bool isRunning = true;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Ищем игрока если цель не назначена
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        
        // Убедимся что Rigidbody2D настроен правильно
        if (rb != null)
        {
            rb.gravityScale = 0; // Для 2D врагов часто отключают гравитацию
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
    }

    private void MoveTowardsTarget()
    {
        if (target == null) return;
        
        // Движение в сторону цели
        Vector2 direction = (target.transform.position - transform.position).normalized;
        
        // Используем Transform для простого движения
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        
        
        // Поворот спрайта в сторону движения
        if (direction.x != 0 && spriteRenderer != null)
        {
            spriteRenderer.flipX = direction.x > 0;
        }
    }
    
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        Debug.Log($"Enemy took {damageAmount} damage. Health: {health}");
        
        // Эффект получения урона (мигание)
        StartCoroutine(DamageEffect());
        
        if (health <= 0)
        {
            Die();
        }
    }
    
    private System.Collections.IEnumerator DamageEffect()
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
        Debug.Log("Enemy died!");
        
        // Эффект смерти
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        }
        
        // Отключаем коллайдеры
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D col in colliders)
        {
            col.enabled = false;
        }
        
        // Отключаем движение
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.simulated = false;
        }
        
        // Уничтожаем через 1 секунду
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
                Destroy(collision.gameObject); // Уничтожаем пулю
            }
        }
        
        // Урон игроку при столкновении
        if (collision.CompareTag("Player"))
        {
            PlayerLogic player = collision.GetComponent<PlayerLogic>();
            if (player != null)
            {
                // player.TakeDamage(damage); // Раскомментируйте если есть метод
                Debug.Log("Player hit by enemy!");
            }
        }
    }
}
