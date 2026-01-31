using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDamage : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    private Vector3 originalScale;
    
    void Start()
    {
        currentHealth = maxHealth;
        originalScale = transform.localScale;
    }
    
    public void SetHealth(float health)
    {
        currentHealth = Mathf.Clamp(health, 0, maxHealth);
        float percent = currentHealth / maxHealth;
        
        transform.localScale = new Vector3(
            originalScale.x * percent,
            originalScale.y,
            originalScale.z
        );
    }
  
}
