using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    
    public GameObject player;

    public int bulletCount = 7;
    public float spreadAngle = 60f;
    public float bulletSpeed = 6f;
    
    public MovingBoss _boss;
    
    private MenegmentXpBar menegmentXp;
    private PlayerLogic _playerLogic;
    

    void Start()
    {
        _boss = GetComponent<MovingBoss>();
        menegmentXp = GameObject.FindGameObjectWithTag("XpBar").GetComponent<MenegmentXpBar>();
        _playerLogic = player.GetComponent<PlayerLogic>();
        
        if (!menegmentXp.isPaused)
        {
            if (!_playerLogic.isDead)
            {            
                StartCoroutine(AttackLoop());
            }

        }
    }

    private IEnumerator AttackLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            ShootFan(player.transform.position);

            if (_boss == null && _boss.health <= _boss.maxHealth / 2)
            {
                yield return new WaitForSeconds(1f);
            }
            else
            {
                yield return new WaitForSeconds(2f);
            }
        }
    }
    
    public void ShootFan(Vector2 targetPosition)
    {
        Vector2 baseDirection = (targetPosition - (Vector2)firePoint.position).normalized;

        float angleStep = spreadAngle / (bulletCount - 1);
        float startAngle = -spreadAngle / 2f;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + angleStep * i;
            
            Vector2 shootDir = Quaternion.Euler(0f, 0f, angle) * baseDirection;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            
            bullet.tag = "BulletBoss";

            bullet.GetComponent<BulletBoss>().Init(shootDir, bulletSpeed);
        }
    }
}
