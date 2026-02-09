
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

    public Button button;
    public Image buttonImage;

    private Color darkColor;

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
    
    public void PlusHp()
    {
        if (price <= _playerLogic.counterCoins)
        {
            Chouce = true;
            button.enabled = true;
            buttonImage.color = Color.cyan;
            
            cardPlusAttackSpeed.Chouce = false;
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
