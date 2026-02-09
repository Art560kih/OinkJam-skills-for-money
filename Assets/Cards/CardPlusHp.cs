using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardPlusHp : MonoBehaviour
{
    public bool chouce = false;
    public bool isPlusHp = false;
    
    public CardToxicBall cardToxicBall;
    public CardColdBall cardColdBall;
    public CardAntiMateria cardAntiMateria;
    public CardFireBall cardFireBall;
    public CardPlusAttackSpeed cardPlusAttackSpeed;
    
    public int price = 25;
    
    private PlayerLogic _playerLogic;

    void Start()
    {
        _playerLogic = GameObject.FindWithTag("Player").GetComponent<PlayerLogic>();
    }
    
    public bool Chouce
    {
        get { return chouce; }
        set { chouce = value; }
    }
    
    public void PlusHp()
    {
        if (price > _playerLogic.counterCoins)
        {
            Chouce = true;
            
            cardPlusAttackSpeed.Chouce = false;
            cardToxicBall.Chouce = false;
            cardColdBall.Chouce = false;
            cardAntiMateria.Chouce = false;
            cardFireBall.Chouce = false;
        }
        else Chouce = false;
    }
    
}
