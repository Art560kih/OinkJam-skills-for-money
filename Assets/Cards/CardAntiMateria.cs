
using UnityEngine;
using UnityEngine.UI;

public class CardAntiMateria : MonoBehaviour
{
    public bool chouce = false;
    public bool isAntiMateria = false;
    
    public CardToxicBall cardToxicBall;
    public CardColdBall cardColdBall;
    public CardFireBall cardFireBall;
    public CardPlusAttackSpeed cardPlusAttackSpeed;
    public CardPlusHp cardPlusHp;
    
    private float x = 0.3f, y = 0.3f;
    
    public int price = 20;
    
    private PlayerLogic _playerLogic;
    
    public Button button;
    public Image buttonImage;
    
    private Color darkColor;
    
    void Start()
    {
        _playerLogic = GameObject.FindWithTag("Player").GetComponent<PlayerLogic>();
        
        darkColor = new Color(0.5f, 0.5f, 0.5f, 1f);
    }


    public void ChouceAntiMateria()
    {
        if (price <= _playerLogic.counterCoins)
        {
            chouce = true;
            button.enabled = true;
            buttonImage.color = Color.cyan;
            
            cardPlusHp.Chouce = false;
            cardPlusAttackSpeed.Chouce = false;
            
            cardToxicBall.Chouce = false;
            cardColdBall.Chouce = false;
            cardFireBall.Chouce = false;
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
