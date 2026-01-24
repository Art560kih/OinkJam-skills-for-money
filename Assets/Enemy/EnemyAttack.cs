using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackInterval = 2f;

    public float damage = 3f;
    public float health = 50f;
    
    private PlayerLogic playerInRange;
    private Coroutine attackCoroutine;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = other.GetComponent<PlayerLogic>();
            if (playerInRange != null && attackCoroutine == null)
            {
                Enemy check = new Enemy();
                StartCoroutine(AttackRoutine());
                check.checkColl = true;
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
            playerInRange = null;
        }
    }
    
    private IEnumerator AttackRoutine()
    {
        while (playerInRange != null)
        {
            playerInRange.hitOnPlayer(damage);
            
            Enemy obj = new Enemy();
            obj.checkColl = true;
            
            yield return new WaitForSeconds(attackInterval);
            
            obj.checkColl = false;
        }
    }
}
