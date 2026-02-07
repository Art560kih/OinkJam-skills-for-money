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
    public CardAntiMateria cardAntiMateria;
    
    public void ChouceColdBall()
    {
        Chouce = true;
        
        cardToxicBall.Chouce = false;
        cardFireBall.Chouce = false;
        cardAntiMateria.Chouce = false;
        
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
    
    public IEnumerator GlaciationMiniBoss(MovingMiniBoss _movingMiniBoss)
    {
        
        float origSpeed = _movingMiniBoss.moveSpeed;
        
        float newSpeed = _movingMiniBoss.moveSpeed / 2f;
        
        yield return new WaitForSeconds(1f);
        
        _movingMiniBoss.moveSpeed = newSpeed;
        
        yield return new WaitForSeconds(3f);
        _movingMiniBoss.moveSpeed = origSpeed;

    }
    
    public IEnumerator GlaciationBoss(MovingBoss _movingBoss)
    {
        
        float origSpeed = _movingBoss.moveSpeed;
        
        float newSpeed = _movingBoss.moveSpeed / 2f;
        
        yield return new WaitForSeconds(1f);
        
        _movingBoss.moveSpeed = newSpeed;
        
        yield return new WaitForSeconds(3f);
        _movingBoss.moveSpeed = origSpeed;

    }
}
