using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public Transform target;
    private float bulletSpeed = 25f;
    public float damage = 10f;
    
    public Camera mainCamera;
    
    private Vector2 direction;
    
    public GameObject prefab;

    private Vector3 mousePos;

    void Start()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
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
}


