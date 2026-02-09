using System.Collections;
using UnityEngine;

public class MovingMiniBoss : MonoBehaviour
{
    public GameObject target;
    private Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    private Animator animator;
    
    private bool isRunning = true;
    
    private PlayerLogic playerLogic;
    private ManegementHpBar hpBar;
    
    public float health = 100f;
    public float moveSpeed = 3f;
    public float damageEnemy = 5f;
    
    public CardFireBall cardFireBall;
    public CardColdBall cardColdBall;
    public CardToxicBall cardToxicBall;
    
    public SpriteRenderer spriteRendererXp;
    public TMPro.TextMeshProUGUI xpbarText;
    
    private MenegmentXpBar xpBar;
    private bool canGetXp = true;
    
    public bool isDead = false;
    
    private float cooldown = 1f;
    
    public AudioClip _audioClipHit;
    public AudioSource _audioSourceHit;
    
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
    }


    private void TakeDamage(float damageAmount)
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
    
    private void MoveTowardsTarget()
    {
        if (target == null) return;

        if (!xpBar.isPaused)
        {
            Vector2 direction = (target.transform.position - transform.position).normalized;
            
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            if (direction.x != 0 && spriteRenderer != null)
            {
                spriteRenderer.flipX = direction.x < 0;
            }
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
            xpBar.CurrentXp += 5f;

            canGetXp = false;
            
            Invoke(nameof(CooldownXp), cooldown);
        }

        StartCoroutine(CounterXp());
        
        
        Destroy(gameObject, 1f);

        
    }
    
    void CooldownXp()
    {
        canGetXp = true;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ColliderForBurning") && cardFireBall.burning)
        {
            MovingMiniBoss movingMiniBoss = GetComponent<MovingMiniBoss>();

            StartCoroutine(cardFireBall.BurningMiniBoss(movingMiniBoss));
            StartCoroutine(EffectBurning());
        }

        if (collision.CompareTag("BulletOrig"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
                
                _audioSourceHit.PlayOneShot(_audioClipHit);
                
                Destroy(collision.gameObject);
            }
        }

        if (collision.CompareTag("BulletFireBall"))
        {
            Bullet bulletFireBall = collision.GetComponent<Bullet>();
            MovingMiniBoss movingMiniBoss = GetComponent<MovingMiniBoss>();

            if (bulletFireBall != null)
            {
                TakeDamage(bulletFireBall.damage);
                
                _audioSourceHit.PlayOneShot(_audioClipHit);

                StartCoroutine(cardFireBall.BurningMiniBoss(movingMiniBoss));

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
                
                _audioSourceHit.PlayOneShot(_audioClipHit);
                
                Destroy(collision.gameObject);
            }
        }

        if (collision.CompareTag("BulletColdBall"))
        {
            Bullet bulletColdBall = collision.GetComponent<Bullet>();
            MovingMiniBoss movingMiniBoss = GetComponent<MovingMiniBoss>();

            if (bulletColdBall != null)
            {
                TakeDamage(bulletColdBall.damage);
                
                _audioSourceHit.PlayOneShot(_audioClipHit);

                StartCoroutine(cardColdBall.GlaciationMiniBoss(movingMiniBoss));

                StartCoroutine(EffectGlaciation());

                Destroy(collision.gameObject);
            }
        }

        if (collision.CompareTag("BulletToxicBall"))
        {
            Bullet bulletColdBall = collision.GetComponent<Bullet>();
            MovingMiniBoss movingMiniBoss = GetComponent<MovingMiniBoss>();

            if (bulletColdBall != null)
            {
                TakeDamage(bulletColdBall.damage);
                
                _audioSourceHit.PlayOneShot(_audioClipHit);

                StartCoroutine(cardToxicBall.PoisoningMiniBoss(movingMiniBoss));

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
    
    private IEnumerator CounterXp()
    {
        spriteRendererXp.enabled = true;
        xpbarText.text = "+5";
        xpbarText.enabled = true;
        yield return new WaitForSeconds(0.5f);
        EndCounterXp();
    }

    private void EndCounterXp()
    {
        spriteRendererXp.enabled = false;
        xpbarText.text = "+1";
        xpbarText.enabled = false;
    }
}
