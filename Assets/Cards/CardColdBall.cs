using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardColdBall : MonoBehaviour
{
    public bool isColdBall = false;
    public bool chouce = false;
    
    public CardToxicBall cardToxicBall;
    public CardFireBall cardFireBall;
    
    
    public void ChouceColdBall()
    {
        Chouce = true;
        
        cardToxicBall.Chouce = false;
        cardFireBall.Chouce = false;
        
    }
    
    public bool Chouce
    {
        get { return chouce; }
        set { chouce = value; }
    }
    

    public IEnumerator Glaciation(MovingEnemy _movingEnemy)
    {
        
        float origSpeed = _movingEnemy.moveSpeed;
        
        float newSpeed = _movingEnemy.moveSpeed / 2f;
        
        yield return new WaitForSeconds(1f);
        
        _movingEnemy.moveSpeed = newSpeed;
        
        yield return new WaitForSeconds(3f);
        _movingEnemy.moveSpeed = origSpeed;

    }
}
