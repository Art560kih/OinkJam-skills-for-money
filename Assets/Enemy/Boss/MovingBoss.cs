
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class MovingBoss : MonoBehaviour
{
    public GameObject target;
    private Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    private Animator animator;
    
    private bool isRunning = true;
    
    private PlayerLogic playerLogic;
    
    public float health = 500f;
    public float maxHealth = 500f;
    
    public float moveSpeed = 1f;
    
    public CardFireBall cardFireBall;
    public CardColdBall cardColdBall;
    public CardToxicBall cardToxicBall;
    
    public SpriteRenderer spriteRendererXp;
    public TMPro.TextMeshProUGUI xpbarText;
    
    private MenegmentXpBar xpBar;
    
    public bool isDead = false;
    
    private float cooldown = 1f;
    
    public GameObject panelWinning;
    
    [SerializeField] private float teleportCooldown = 3f;
    [SerializeField] private float minTeleportDistance = 3f;
    [SerializeField] private float maxTeleportDistance = 15f;

    [SerializeField] private Transform areaCenter;
    [SerializeField] private float areaRadius = 10f;
    private float teleportTimer;

    [SerializeField] private SpriteRenderer shadow;

    private GameObject puddlePrefab;

    public GameObject coin;
    public GameObject panelEnd;
    
    public HpBarBoss hpBarBoss;
    
    public AudioClip _audioClipHit;
    public AudioSource _audioSourceHit;
    
    
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        puddlePrefab = GameObject.FindGameObjectWithTag("Puddle");
        
        playerLogic = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLogic>();
 
        xpBar = GameObject.FindGameObjectWithTag("XpBar").GetComponent<MenegmentXpBar>();
        
        teleportTimer = 0.1f;
        
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
        if (!xpBar.isPaused)
        {
            if (!playerLogic.isDead)
            {
                if (health <= 0)
                {
                    Die();
                    isRunning = false;
                    Time.timeScale = 0;
                    panelWinning.SetActive(true);
                    
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        panelWinning.SetActive(false);
                        panelEnd.SetActive(true);
                        coin.SetActive(false);
                    }
                    return;
                }

                animator.SetBool("IsRunning", isRunning);
                MoveTowardsTarget();

                teleportTimer -= Time.deltaTime;

                if (teleportTimer <= 0)
                {
                    Teleportation();
                    teleportTimer = teleportCooldown;
                }


                if (target == null) return;
            }
        }
    }


    private void Teleportation()
    {
        if (health <= maxHealth / 2)
        {
            StartCoroutine(EffectTeleportation());

            isRunning = false;
            
            StartCoroutine(Teleport());
        }
    }

    private IEnumerator Teleport()
    {
        yield return new WaitForSeconds(teleportCooldown);

        Vector2 earlyPosition = transform.position;
        
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(minTeleportDistance, maxTeleportDistance);
        Vector2 newPosition = (Vector2)transform.position + randomDirection * randomDistance;

        health += 30f;
        if(hpBarBoss != null) hpBarBoss.CurrentHp += 30f;
            
        if (areaCenter != null)
        {
            newPosition = Vector2.ClampMagnitude(newPosition - (Vector2)areaCenter.position, areaRadius) + (Vector2)areaCenter.position;
        }
            
        transform.position = newPosition;
        
        GameObject clonePuddle = Instantiate(puddlePrefab, earlyPosition, Quaternion.identity);
        
        clonePuddle.tag = "Puddle";
        clonePuddle.name = "Puddle";
    }


    private void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if(hpBarBoss != null) hpBarBoss.CurrentHp -= damageAmount;
        
        StartCoroutine(DamageEffect());
        
        if (health <= 0)
        {
            Die();
        }
    }
    
    private IEnumerator EffectTeleportation()
    {
        yield return new WaitForSeconds(1f);
        
        moveSpeed = 0;
        
        for (int i = 0; i < 5; i++)
        {
            shadow.color = Color.blue;
            yield return new WaitForSeconds(0.1f);
            shadow.color = new Color(0.7f, 0.7f, 0.7f, 0.1f);
            
            yield return new WaitForSeconds(1f);
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
        
        Destroy(gameObject, 1f);

        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ColliderForBurning") && cardFireBall.burning)
        {
            MovingBoss movingBoss = GetComponent<MovingBoss>();

            StartCoroutine(cardFireBall.BurningBoss(movingBoss));
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
            MovingBoss movingBoss = GetComponent<MovingBoss>();

            if (bulletFireBall != null)
            {
                TakeDamage(bulletFireBall.damage);
                
                _audioSourceHit.PlayOneShot(_audioClipHit);

                for (int i = 0; i < 3; i++)
                {
                    hpBarBoss.CurrentHp -= cardFireBall.periodicDamage;
                }

                StartCoroutine(cardFireBall.BurningBoss(movingBoss));

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
            MovingBoss movingBoss = GetComponent<MovingBoss>();

            if (bulletColdBall != null)
            {
                TakeDamage(bulletColdBall.damage);
                
                _audioSourceHit.PlayOneShot(_audioClipHit);

                StartCoroutine(cardColdBall.GlaciationBoss(movingBoss));

                StartCoroutine(EffectGlaciation());

                Destroy(collision.gameObject);
            }
        }

        if (collision.CompareTag("BulletToxicBall"))
        {
            Bullet bulletColdBall = collision.GetComponent<Bullet>();
            MovingBoss movingBoss = GetComponent<MovingBoss>();

            if (bulletColdBall != null)
            {
                TakeDamage(bulletColdBall.damage);
                
                _audioSourceHit.PlayOneShot(_audioClipHit);
                
                for (int i = 0; i < 3; i++)
                {
                    hpBarBoss.CurrentHp -= cardToxicBall.periodicDamage;
                }

                StartCoroutine(cardToxicBall.PoisoningBoss(movingBoss));

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
}
