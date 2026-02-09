using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardPlusAttackSpeed : MonoBehaviour
{
    public bool chouce = false;
    public bool isAttackSpeed = false;
    public int price = 15;
    private PlayerLogic _playerLogic;
    
    public CardToxicBall cardToxicBall;
    public CardColdBall cardColdBall;
    public CardAntiMateria cardAntiMateria;
    public CardFireBall cardFireBall;
    public CardPlusHp cardPlusHp;

    void Start()
    {
        _playerLogic = GameObject.FindWithTag("Player").GetComponent<PlayerLogic>();
    }

    public bool Chouce
    {
        get { return chouce; }
        set { chouce = value; }
    }
    
    public void ChouceAttackSpeed()
    {
        if (price <= _playerLogic.counterCoins)
        {
            Chouce = true;
            
            cardPlusHp.Chouce = false;
            cardToxicBall.Chouce = false;
            cardColdBall.Chouce = false;
            cardAntiMateria.Chouce = false;
            cardFireBall.Chouce = false;
        }
        else Chouce = false;
    }
    
}
