using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class CardFireBall : MonoBehaviour
{
    public float periodicDamage = 0.5f;

    public bool chouce = false;
    public bool isFireBall = false;
    public bool burning = false;
    
    public CardToxicBall cardToxicBall;
    public CardColdBall cardColdBall;
    public CardAntiMateria cardAntiMateria;
    public CardPlusAttackSpeed cardPlusAttackSpeed;
    public CardPlusHp cardPlusHp;

    public int price = 10;
    
    private PlayerLogic _playerLogic;
    
    public Button button;
    public Image buttonImage;

    private Color darkColor;
    void Start()
    {
        _playerLogic = GameObject.FindWithTag("Player").GetComponent<PlayerLogic>();
        
        darkColor = new Color(0.5f, 0.5f, 0.5f, 1f);
    }
    
    public void ChouceFireBall()
    {
        if (price <= _playerLogic.counterCoins)
        {
            Chouce = true;
            button.enabled = true;     
            buttonImage.color = Color.cyan;
            
            cardPlusHp.Chouce = false;
            cardPlusAttackSpeed.Chouce = false;
            
            cardToxicBall.Chouce = false;
            cardColdBall.Chouce = false;
            cardAntiMateria.Chouce = false;
        }
        else
        {
            Chouce = false;
            button.enabled = false;
            buttonImage.color = darkColor;
        }
    }

    public bool Chouce
    {
        get { return chouce; }
        set { chouce = value; }
    }
    
    
    public IEnumerator Burning(MovingEnemy _movingEnemy)
    {
        burning = true;
        
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1f);
            _movingEnemy.health -= periodicDamage;
            burning = false;
           
        }
    }
    public IEnumerator BurningMiniBoss(MovingMiniBoss _movingMiniBoss)
    {
        burning = true;
        
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1f);
            _movingMiniBoss.health -= periodicDamage;
            burning = false;
           
        }
    }
    
    public IEnumerator BurningBoss(MovingBoss _movingBoss)
    {
        burning = true;
        
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1f);
            _movingBoss.health -= periodicDamage;
            burning = false;
           
        }
    }
}
