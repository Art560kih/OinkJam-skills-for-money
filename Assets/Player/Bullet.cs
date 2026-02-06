using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public Transform target;
    private float bulletSpeed = 25f;
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
    
    void Start()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        cardPlusAttackSpeed = GameObject.FindGameObjectWithTag("CardPlusDamage").GetComponent<CardPlusAttackSpeed>();
        spriteRenderer.sprite = GameObject.FindGameObjectWithTag("SpriteAntiMateria").GetComponent<SpriteRenderer>().sprite;
        
        
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
        
        Debug.Log("damage: " + damage);

    }
    
    
    void Update()
    {
        
        if (gameObject.CompareTag("Bullet") || gameObject.CompareTag("BulletFireBall") || gameObject.CompareTag("BulletColdBall") || gameObject.CompareTag("BulletToxicBall") || gameObject.CompareTag("BulletAntiMateria"))
        {
            StartCoroutine(moveBullet());
            
        }
        
    }
    

    private IEnumerator moveBullet()
    {
        transform.position = Vector3.MoveTowards(transform.position, mousePos, bulletSpeed * Time.deltaTime);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    // private void MoveBullet()
    // {
    //     transform.position = Vector3.MoveTowards(transform.position, mousePos, bulletSpeed * Time.deltaTime);
    //     Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);
    //     if (viewportPos.x < -0.1f || viewportPos.x > 1.1f || 
    //         viewportPos.y < -0.1f || viewportPos.y > 1.1f)
    //     {
    //         Destroy(gameObject);
    //     }
    // }
}


