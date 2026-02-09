using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class MenegmentXpBar : MonoBehaviour
{
    public Slider currentSlider;
    public Image currentXpBar;
    public PlayerLogic _player;
    
    public float currentXp = 0f;
    public float  maxXp = 5f;
    
    public SpriteRenderer spriteRendererXp;
    public TextMeshProUGUI xpbarText;

    public int nextLvl = 0;
    public TextMeshProUGUI lvl;
    
    public GameObject choucePanel;
    
    public bool isPaused = false;
    
    public List<GameObject> pointsCards = new List<GameObject>();
    public List<GameObject> cards = new List<GameObject>();

    private Random rnd = new Random();

    private GameObject firstCard, secondCard, thirdCard;
    
    private GameObject cloneFirstCard, cloneSecondCard, cloneThirdCard;
    
    public CardFireBall cardFireBall;
    public CardColdBall cardColdBall;
    public CardToxicBall cardToxicBall;
    public CardAntiMateria cardAntiMateria;
    public CardPlusAttackSpeed cardPlusAttackSpeed;
    public CardPlusHp cardPlusHp;
    
    public ManegementHpBar manegementHpBar;

    public GameObject spawnMiniBoss;
    public GameObject spawnBoss;
    
    public SpawnedMiniBoss spawnMiniBossScript;
    
    public TextMeshProUGUI textFireBall;
    public TextMeshProUGUI textToxicBall;
    public TextMeshProUGUI textColdBall;
    public TextMeshProUGUI textAntiMateria;
    public TextMeshProUGUI textPlusAttackSpeed;
    public TextMeshProUGUI textPlusHp;

    public Button buttonPick;
    
    private void Cards() 
    {
        
        firstCard = cards[rnd.Next(0, cards.Count)];
        
        while(true) 
        {
            secondCard = cards[rnd.Next(0, cards.Count)];
            
            if(firstCard != secondCard) break;
        }
        while (true)
        {
            thirdCard = cards[rnd.Next(0, cards.Count)];
            
            if(thirdCard != firstCard &&  thirdCard != secondCard) break;
        }
        
        cloneFirstCard = Instantiate(firstCard, pointsCards[0].transform.position, Quaternion.identity);
        cloneSecondCard = Instantiate(secondCard, pointsCards[1].transform.position, Quaternion.identity);
        cloneThirdCard = Instantiate(thirdCard, pointsCards[2].transform.position, Quaternion.identity);
      
    }

    public float CurrentXp
    {
        get => currentXp;
        set => currentXp = value;
    }

    void Start()
    {
        spriteRendererXp.enabled = false;
        xpbarText.enabled = false;
        
        isPaused = false;
        
        spawnMiniBoss.SetActive(false);
        spawnBoss.SetActive(false);
        
        
    }
    
    public void Continue()
    {
        isPaused = false;
        choucePanel.SetActive(false);
        
        
        Destroy(cloneFirstCard);
        Destroy(cloneSecondCard);
        Destroy(cloneThirdCard);
    }

    public void ChouceAntiMateria()
    {
        if (cardAntiMateria.chouce)
        {
            if (cardFireBall != null) cardFireBall.isFireBall = false;

            if (cardColdBall != null) cardColdBall.isColdBall = false;

            if (cardToxicBall != null) cardAntiMateria.isAntiMateria = true;

            if (cardToxicBall != null) cardToxicBall.isToxicball = false;

            _player.CounterCoins -= cardAntiMateria.price; 
        }
    }

    public void ChouceHpBar()
    {
        if (cardPlusHp.chouce) cardPlusHp.isPlusHp = true;

        if (cardPlusHp.isPlusHp)
        {
            manegementHpBar.maxHp += 10;
            manegementHpBar.currentHp = manegementHpBar.maxHp;
            
            _player.maxHealth += 10;
            _player.health = _player.maxHealth;
            
            _player.CounterCoins -= cardPlusHp.price; 
        }


    }

    public void ChoucePlusAttackSpeed()
    {
        if(cardPlusAttackSpeed.chouce) cardPlusAttackSpeed.isAttackSpeed = true;
        
          
        if (cardPlusAttackSpeed.isAttackSpeed)
        {
            _player.spawnRate += 1f;
            cardPlusAttackSpeed.chouce = false;
            cardPlusAttackSpeed.isAttackSpeed = false;
            
            _player.CounterCoins -= cardPlusAttackSpeed.price; 
        }
    }
    
    
    public void ChouceFireBall()
    {
        if (cardFireBall.chouce)
        {

            if (cardFireBall != null) cardFireBall.isFireBall = true;
            
            if (cardColdBall != null) cardColdBall.isColdBall = false;
            
            if(cardToxicBall != null) cardAntiMateria.isAntiMateria = false;
            
            if (cardToxicBall != null) cardToxicBall.isToxicball = false;
            
            _player.CounterCoins -= cardFireBall.price; 
        }
    }
    

    public void ChouceToxicBall()
    {
        if (cardToxicBall.chouce)
        {

            if (cardFireBall != null) cardFireBall.isFireBall = false;
            
            if(cardToxicBall != null) cardAntiMateria.isAntiMateria = false;

            if (cardColdBall != null) cardColdBall.isColdBall = false;
            
            if (cardToxicBall != null) cardToxicBall.isToxicball = true;
 
            _player.CounterCoins -= cardToxicBall.price; 
        }
    }

    public void ChouceColdBall()
    {
        if (cardColdBall.chouce)
        {
            if (cardFireBall != null) cardFireBall.isFireBall = false;

            if (cardColdBall != null) cardColdBall.isColdBall = true;
            
            if (cardToxicBall != null) cardToxicBall.isToxicball = false;
            
            if(cardToxicBall != null) cardAntiMateria.isAntiMateria = false;

            _player.CounterCoins -= cardColdBall.price; 
        }
    }

    private void Pause()
    {
        isPaused = true;
        choucePanel.SetActive(true);
        
        cardFireBall.isFireBall = false;
        cardColdBall.isColdBall = false;
        cardToxicBall.isToxicball = false;
        
        if (cardAntiMateria.price > _player.counterCoins)
        {
            cardAntiMateria.Chouce = false;
            textAntiMateria.color = Color.red;
        }
        else textAntiMateria.color = Color.white;
        
        if (cardFireBall.price > _player.counterCoins)
        {
            cardFireBall.Chouce = false;
            textFireBall.color = Color.red;
        }
        else textFireBall.color = Color.white;
        
        if (cardColdBall.price > _player.counterCoins)
        {
            cardColdBall.Chouce = false;
            textColdBall.color = Color.red;
        }
        else textColdBall.color = Color.white;
        
        if (cardToxicBall.price > _player.counterCoins)
        {
            cardToxicBall.Chouce = false;
            textToxicBall.color = Color.red;
        }
        else textToxicBall.color = Color.white;
        
        if (cardPlusAttackSpeed.price > _player.counterCoins)
        {
            cardPlusAttackSpeed.Chouce = false;
            textPlusAttackSpeed.color = Color.red;
        }
        else textPlusAttackSpeed.color = Color.white;
       
        if (cardPlusHp.price > _player.counterCoins)
        {
            cardPlusHp.Chouce = false;
            textPlusHp.color = Color.red;
        }
        else textPlusHp.color = Color.white;
    }
    
    void Update()
    {
        if (!cardPlusAttackSpeed.chouce 
            && !cardPlusHp.chouce 
            && !cardToxicBall.chouce 
            && !cardAntiMateria.chouce 
            && !cardFireBall.chouce 
            && !cardColdBall.chouce)
        {
            buttonPick.interactable = false;
        }
        else
        {
            buttonPick.interactable = true;
        }

        if (cardAntiMateria.price > _player.counterCoins
            && cardFireBall.price > _player.counterCoins
            && cardColdBall.price > _player.counterCoins
            && cardToxicBall.price > _player.counterCoins
            && cardPlusAttackSpeed.price > _player.counterCoins
            && cardPlusHp.price > _player.counterCoins)
        {
            buttonPick.interactable = true;
        }
        
        
        
        if (maxXp <= 5)
        {
            currentXpBar.fillAmount = currentXp / 5;
            
            if (currentXpBar.fillAmount >= 1f)
            {
                currentXpBar.fillAmount = 0f;
                
                currentXp = 0f;
                maxXp += 5;
                
                nextLvl++;
                lvl.text = $"{nextLvl}";
                
                Pause();
                Cards();
            }
        }
        
        else if (maxXp <= 10)
        {
            currentXpBar.fillAmount = currentXp / 10;
            
            if (currentXpBar.fillAmount >= 1f)
            {
                currentXpBar.fillAmount = 0f;
                
                currentXp = 0f;
                maxXp += 5;
                
                nextLvl++;
                lvl.text = $"{nextLvl}";
                
                Pause();
                Cards();
            }
        }
        
        else if (maxXp <= 15)
        {
            currentXpBar.fillAmount = currentXp / 15;
            
            spawnMiniBoss.SetActive(true);
            
            if (currentXpBar.fillAmount >= 1f)
            {
                currentXpBar.fillAmount = 0f;
                
                currentXp = 0f;
                maxXp += 5;
                
                nextLvl++;
                lvl.text = $"{nextLvl}";
                
   

                Pause();
                Cards();
            }
            
        }
        
        else if (maxXp <= 20)
        {
            spawnMiniBoss.SetActive(false);
            currentXpBar.fillAmount = currentXp / 20;
            
            spawnMiniBoss.SetActive(true);
            
            if (currentXpBar.fillAmount >= 1f)
            {
                currentXpBar.fillAmount = 0f;
                
                currentXp = 0f;
                maxXp += 5;
                
                nextLvl++;
                lvl.text = $"{nextLvl}";
                
                Pause();
                Cards();
            }
            
        }
        
        else if (maxXp <= 25)
        {
            currentXpBar.fillAmount = currentXp / 25;

            spawnMiniBossScript.maxMiniBoss = 2;
            spawnMiniBoss.SetActive(true);
            
            if (currentXpBar.fillAmount >= 1f)
            {
                currentXpBar.fillAmount = 0f;
                
                currentXp = 0f;
                maxXp += 5;
                
                nextLvl++;
                lvl.text = $"{nextLvl}";
                
             
                
                Pause();
                Cards();
            }
            
        }
        
        else if (maxXp <= 30)
        {
            currentXpBar.fillAmount = currentXp / 30;
            
            spawnMiniBoss.SetActive(false);
            
            spawnBoss.SetActive(true);
            
            if (currentXpBar.fillAmount >= 1f)
            {
                currentXpBar.fillAmount = 0f;
                
                currentXp = 0f;
                maxXp += 5;
                
                nextLvl++;
                lvl.text = $"{nextLvl}";
                
                Pause();
                Cards();
            }
            
        }

    }
    
}


