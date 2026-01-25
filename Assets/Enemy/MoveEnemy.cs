using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    private GameObject target;
    
    [SerializeField] public float moveSpeed = 1.5f;
    private float health;

    void Update()
    {
        if (health > 0f)
        {
            if (target != null)
            {
                Vector2 direction = (target.transform.position - transform.position).normalized;
                transform.Translate(direction * moveSpeed * Time.deltaTime);
            }
            
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public float GetHp(float hp)
    {
        health = hp;
        return health;
    }

    public GameObject GetTarget (GameObject targ)
    {
        target = targ;
        return target;
    }
    
}


