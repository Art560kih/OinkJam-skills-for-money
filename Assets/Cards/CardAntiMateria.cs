using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAntiMateria : MonoBehaviour
{
    public bool chouce = false;
    public bool isAntiMateria = false;
    
    public CardToxicBall cardToxicBall;
    public CardColdBall cardColdBall;
    public CardFireBall cardFireBall;
    
    private float x = 0.3f, y = 0.3f;


    public void ChouceAntiMateria()
    {
        chouce = true;
        
        cardToxicBall.Chouce = false;
        cardColdBall.Chouce = false;
        cardFireBall.Chouce = false;
    }

    public bool Chouce
    {
        get { return chouce; }
        set { chouce = value; }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("MiniBoss"))
        {
            x += 0.2f;
            y += 0.2f;
            transform.localScale = new Vector2(x, y);
        }
    }
}
