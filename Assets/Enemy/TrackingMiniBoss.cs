using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingMiniBoss : MonoBehaviour
{
    private GameObject player;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    
    private MenegmentXpBar menegmentXpBar;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        menegmentXpBar = GameObject.FindGameObjectWithTag("XpBar").GetComponent<MenegmentXpBar>();
        InvokeRepeating("Shoot", 1f, 1f);
    }
    
    private void Shoot()
    {
        Vector3 direction = (player.transform.position - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.tag = "BulletMiniBoss";


        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

        Destroy(bullet, 3f);
    }
    

}
