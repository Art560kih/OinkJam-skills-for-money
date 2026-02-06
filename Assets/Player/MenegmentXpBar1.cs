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
    public float minXp = 0f,  maxXp = 5f;
    
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
    public CardPlusAttackSpeed cardPlusAttackSpeed;
    public CardPlusHp cardPlusHp;
    
    public ManegementHpBar manegementHpBar;

    public bool isLevelMiniBoss = false;

    public Bullet bullet;

    public GameObject spawnMiniBoss;
    
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
    }
    
    public void Continue()
    {
        isPaused = false;
        choucePanel.SetActive(false);
        
        
        Destroy(cloneFirstCard);
        Destroy(cloneSecondCard);
        Destroy(cloneThirdCard);
    }

    public void ChouceHpBar()
    {
        if (cardPlusHp.chouce) cardPlusHp.isPlusHp = true;

        if (cardPlusHp.isPlusHp)
        {
            manegementHpBar.maxHp += 10;
            manegementHpBar.currentHp += 10;
            
            Debug.Log(manegementHpBar.currentHp +"?" + manegementHpBar.maxHp);
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
       
        }
    }
    
    
    public void ChouceFireBall()
    {
        if (cardFireBall.chouce)
        {
                        
            if (cardFireBall != null) cardFireBall.isFireBall = true;
            if (cardColdBall != null)
            {
                cardColdBall.isColdBall = false;
            }
            if (cardToxicBall != null)
            {
                cardToxicBall.isToxicball = false;
            }
            
            Debug.Log("ChouceFireBall");

        }
    }
    

    public void ChouceToxicBall()
    {
        if (cardToxicBall.chouce)
        {

            if (cardFireBall != null)
            {
                cardFireBall.isFireBall = false;
            }

            if (cardColdBall != null)
            {
                cardColdBall.isColdBall = false;
            }
            if (cardToxicBall != null) cardToxicBall.isToxicball = true;
            
            Debug.Log("ChouceToxicBall");
 
        }
    }

    public void ChouceColdBall()
    {
        if (cardColdBall.chouce)
        {
            if (cardFireBall != null)
            {
                cardFireBall.isFireBall = false;
            }

            if (cardColdBall != null) cardColdBall.isColdBall = true;
            if (cardToxicBall != null) 
            {
                cardToxicBall.isToxicball = false;
            }
            
            Debug.Log("ChouceColdBall");

        }
    }

    private void Pause()
    {
        isPaused = true;
        choucePanel.SetActive(true);
        
        cardFireBall.isFireBall = false;
        cardColdBall.isColdBall = false;
        cardToxicBall.isToxicball = false;
    }
    
    void Update()
    {
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
           
            currentXpBar.fillAmount = currentXp / 20;
            
            if (currentXpBar.fillAmount >= 1f)
            {
                currentXpBar.fillAmount = 0f;
                
                currentXp = 0f;
                maxXp += 5;
                
                nextLvl++;
                lvl.text = $"{nextLvl}";
                
                spawnMiniBoss.SetActive(false);
                
                Pause();
                Cards();
            }
            
        }
        
        else if (maxXp <= 25)
        {
           
            currentXpBar.fillAmount = currentXp / 25;
            
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
            
            if (currentXpBar.fillAmount >= 1f)
            {
                currentXpBar.fillAmount = 0f;
                
                currentXp = 0f;
                maxXp += 5;
                
                nextLvl++;
                lvl.text = $"{nextLvl}";
                
                spawnMiniBoss.SetActive(true);
                
                Pause();
                Cards();
            }
            
        }
        
        else if (maxXp <= 35)
        {
           
            currentXpBar.fillAmount = currentXp / 35;
            
            if (currentXpBar.fillAmount >= 1f)
            {
                currentXpBar.fillAmount = 0f;
                
                currentXp = 0f;
                maxXp += 5;
                
                nextLvl++;
                lvl.text = $"{nextLvl}";
                
                spawnMiniBoss.SetActive(false);
                
                Pause();
                Cards();
            }
            
        }
        
        else if (maxXp <= 40)
        {
           
            currentXpBar.fillAmount = currentXp / 40;
            
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
        
        else if (maxXp <= 45)
        {
           
            currentXpBar.fillAmount = currentXp / 45;
            
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
        else if (maxXp <= 50)
        {
           
            currentXpBar.fillAmount = currentXp / 50;
            
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


