using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardToxicBall : MonoBehaviour
{
    public float periodicDamage = 1f;

    public bool chouce = false;
    public bool isToxicball = false;
    
    
    public CardFireBall cardFireBall;
    public CardColdBall cardColdBall;
    public CardAntiMateria cardAntiMateria;
    public CardPlusAttackSpeed cardPlusAttackSpeed;
    public CardPlusHp cardPlusHp;
    
    public int price = 40;
    
    private PlayerLogic _playerLogic;

    void Start()
    {
        _playerLogic = GameObject.FindWithTag("Player").GetComponent<PlayerLogic>();
    }

    public void ChouceToxicBall()
    {
        if (price <= _playerLogic.counterCoins)
        {
            Chouce = true;

            cardPlusHp.Chouce = false;
            cardPlusAttackSpeed.Chouce = false;
            
            cardFireBall.Chouce = false;
            cardColdBall.Chouce = false;
            cardAntiMateria.Chouce = false;
        }  
        else Chouce = false;
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
