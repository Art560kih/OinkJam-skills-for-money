
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
    
    public Image buttonImage;

    private Color darkColor;

    public Button button;
    
    
    void Start()
    {
        _playerLogic = GameObject.FindWithTag("Player").GetComponent<PlayerLogic>();
        
        darkColor = new Color(0.5f, 0.5f, 0.5f, 1f);
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
            button.enabled = true;
            buttonImage.color = Color.cyan;
            
            cardPlusHp.Chouce = false;
            cardToxicBall.Chouce = false;
            cardColdBall.Chouce = false;
            cardAntiMateria.Chouce = false;
            cardFireBall.Chouce = false;
        }
        else
        {
            Chouce = false;
            button.enabled = false;
            buttonImage.color = darkColor;
        }
    }
    
}
