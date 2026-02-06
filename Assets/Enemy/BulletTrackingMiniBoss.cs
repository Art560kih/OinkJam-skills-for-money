using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrackingMiniBoss : MonoBehaviour
{
    private float damage = 10f;
    public PlayerLogic player;
    private ManegementHpBar hpBar;

    private void Start()
    {
        hpBar = GameObject.FindGameObjectWithTag("Slider").GetComponent<ManegementHpBar>();
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
