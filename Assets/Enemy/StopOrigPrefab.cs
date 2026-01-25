using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopOrigPrefab : MonoBehaviour
{
    [SerializeField] private bool lockInScene = true;
    
    void Start()
    {
        if (lockInScene)
        {
            LockThisPrefab();
        }
    }
    
    void LockThisPrefab()
    {

        MoveEnemy scriptMoveEnemy = GetComponent<MoveEnemy>();
        if(scriptMoveEnemy)
        {
            scriptMoveEnemy.enabled = false;
        }
        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = true;
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
        }
        
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D col in colliders)
        {
            col.enabled = false;
        }

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = false;
        
        transform.position = new Vector3(1000, 1000, 0);
        
        gameObject.SetActive(false);
    }
    
    
}
