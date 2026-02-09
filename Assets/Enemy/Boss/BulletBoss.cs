using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBoss : MonoBehaviour
{
    private Vector2 direction;
    private float speed;
    
    private float damage = 5f;
    public PlayerLogic player;
    private ManegementHpBar hpBar;

    public float lifeTime = 5f;
    
    private MenegmentXpBar menegmentXp;
    
    private void Start()
    {
        hpBar = GameObject.FindGameObjectWithTag("Slider").GetComponent<ManegementHpBar>();
        menegmentXp = GameObject.FindGameObjectWithTag("XpBar").GetComponent<MenegmentXpBar>();
    }

    public void Init(Vector2 dir, float spd)
    {
        direction = dir.normalized;
        speed = spd;

        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        if (!menegmentXp.isPaused)
        {
            if (!player.isDead)
            {
                transform.position += (Vector3)(direction * speed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player.health > 0)
            {
                player.TakeDamage -= damage;

                hpBar.CurrentHp -= damage;
            }
            else
            {
                player.isDead = true;
            }

            Destroy(gameObject);
        }
    }
}
