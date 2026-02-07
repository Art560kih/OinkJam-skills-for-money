using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardToxicBall : MonoBehaviour
{
    public float periodicDamage = 1f;

    public bool chouce = false;
    public bool isToxicball = false;
    
    
    public CardFireBall cardFireBall;
    public CardColdBall cardColdBall;
    public CardAntiMateria cardAntiMateria;

    public void ChouceToxicBall()
    {
        Chouce = true;
        
        cardFireBall.Chouce = false;
        cardColdBall.Chouce = false;
        cardAntiMateria.Chouce = false;
    }
    
    public bool Chouce
    {
        get { return chouce; }
        set { chouce = value; }
    }
    
    public IEnumerator Poisoning(MovingEnemy _movingEnemy)
    {
        yield return new WaitForSeconds(1f);
        
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1f);
            _movingEnemy.health -= periodicDamage;
           
        }
    }
    
    public IEnumerator PoisoningMiniBoss(MovingMiniBoss _movingMiniBoss)
    {
        yield return new WaitForSeconds(1f);
        
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1f);
            _movingMiniBoss.health -= periodicDamage;
           
        }
    }
    
    public IEnumerator PoisoningBoss(MovingBoss _movingBoss)
    {
        yield return new WaitForSeconds(1f);
        
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1f);
            _movingBoss.health -= periodicDamage;
           
        }
    }
}
