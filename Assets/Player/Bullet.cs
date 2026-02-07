using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    public Transform target;
    private float bulletSpeed = 15f;
    public float damage = 2f;
    
    public Camera mainCamera;
    
    private Vector2 direction;

    private Vector3 mousePos;

    private CardPlusAttackSpeed cardPlusAttackSpeed;
    
    public SpriteRenderer spriteRenderer;
    
    public Sprite fireBall;
    public Sprite coldBall;
    public Sprite toxicBall;
    public Sprite antiMateria;
    
    public GameObject player;

    private Rigidbody2D rb;
    
    void Start()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        cardPlusAttackSpeed = GameObject.FindGameObjectWithTag("CardPlusDamage").GetComponent<CardPlusAttackSpeed>();
        spriteRenderer.sprite = GameObject.FindGameObjectWithTag("SpriteAntiMateria").GetComponent<SpriteRenderer>().sprite;
        rb = GetComponent<Rigidbody2D>();
        
        switch (gameObject.tag)
        {
            case "Bullet":
                break;
            case "BulletFireBall":
                spriteRenderer.sprite = fireBall;
                break;
            case "BulletColdBall":
                spriteRenderer.sprite = coldBall;
                break;
            case "BulletToxicBall":
                spriteRenderer.sprite = toxicBall;
                break;
            case "BulletAntiMateria":
                spriteRenderer.sprite = antiMateria;
                break;
        }

    }

    public float Damage
    {
        get => damage;
        set => damage = value;
    }


    void Update()
    {
        
        if (gameObject.CompareTag("Bullet") || gameObject.CompareTag("BulletFireBall") || gameObject.CompareTag("BulletColdBall") || gameObject.CompareTag("BulletToxicBall") || gameObject.CompareTag("BulletAntiMateria"))
        {
            MoveBullet();
        }
        
    }
    
    private void MoveBullet()
    {
        Vector3 direction = (mousePos - player.transform.position).normalized;
        
        transform.Translate(direction * bulletSpeed * Time.deltaTime, Space.World);
        
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);
        rb.velocity = direction * bulletSpeed;
        if (viewportPos.x < -0.1f || viewportPos.x > 1.1f || 
            viewportPos.y < -0.1f || viewportPos.y > 1.1f)
        {
            Destroy(gameObject);
        }
    }
}


