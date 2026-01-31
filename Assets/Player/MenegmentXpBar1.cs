using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenegmentXpBar : MonoBehaviour
{
    public Slider currentSlider;
    public Image currentXpBar;
    private PlayerLogic player;
    
    public float currentXp = 0f;
    public float minXp = 0f,  maxXp = 5f;
    
    public SpriteRenderer spriteRendererXp;
    public TMPro.TextMeshProUGUI xpbarText;

    public int nextLvl = 0;
    public TMPro.TextMeshProUGUI lvl;
    

    public float CurrentXp
    {
        get => currentXp;
        set => currentXp = value;
    }

    void Start()
    {
        spriteRendererXp.enabled = false;
        xpbarText.enabled = false;
        
        currentSlider.maxValue = maxXp;
        currentSlider.minValue = minXp;
        
    }

    void Update()
    {
        if (currentXp <= 10 && maxXp <= 5)
        {
            currentXpBar.fillAmount = currentXp / 5;
            
            if (currentXpBar.fillAmount >= 1f)
            {
                currentXpBar.fillAmount -= 1f;
                
                currentXp = 0f;
                maxXp += 5;
                
                nextLvl++;
                lvl.text = $"{nextLvl}";
            }
        }
        
        else if (currentXp <= 10 && maxXp <= 10)
        {
            currentXpBar.fillAmount = currentXp / 10;
            
            if (currentXpBar.fillAmount >= 1f)
            {
                currentXpBar.fillAmount -= 1f;
                
                currentXp = 0f;
                maxXp += 5;
                
                nextLvl++;
                lvl.text = $"{nextLvl}";
            }
        }
        
        else if (currentXp <= 10 && maxXp <= 15)
        {
           
            currentXpBar.fillAmount = currentXp / 15;
            
            if (currentXpBar.fillAmount >= 1f)
            {
                currentXpBar.fillAmount -= 1f;
                
                currentXp = 0f;
                maxXp += 5;
                
                nextLvl++;
                lvl.text = $"{nextLvl}";
            }
            
        }
        
        else if (currentXp <= 10 && maxXp <= 20)
        {
           
            currentXpBar.fillAmount = currentXp / 20;
            
            if (currentXpBar.fillAmount >= 1f)
            {
                currentXpBar.fillAmount -= 1f;
                
                currentXp = 0f;
                maxXp += 5;
                
                nextLvl++;
                lvl.text = $"{nextLvl}";
            }
            
        }
        
        else if (currentXp <= 10 && maxXp <= 25)
        {
           
            currentXpBar.fillAmount = currentXp / 25;
            
            if (currentXpBar.fillAmount >= 1f)
            {
                currentXpBar.fillAmount -= 1f;
                
                currentXp = 0f;
                maxXp += 5;
                
                nextLvl++;
                lvl.text = $"{nextLvl}";
            }
            
        }
        
        else if (currentXp <= 10 && maxXp <= 30)
        {
           
            currentXpBar.fillAmount = currentXp / 30;
            
            if (currentXpBar.fillAmount >= 1f)
            {
                currentXpBar.fillAmount -= 1f;
                
                currentXp = 0f;
                maxXp += 5;
                
                nextLvl++;
                lvl.text = $"{nextLvl}";
            }
            
        }
        
        else if (currentXp <= 10 && maxXp <= 35)
        {
           
            currentXpBar.fillAmount = currentXp / 35;
            
            if (currentXpBar.fillAmount >= 1f)
            {
                currentXpBar.fillAmount -= 1f;
                
                currentXp = 0f;
                maxXp += 5;
                
                nextLvl++;
                lvl.text = $"{nextLvl}";
            }
            
        }
        
        else if (currentXp <= 10 && maxXp <= 40)
        {
           
            currentXpBar.fillAmount = currentXp / 40;
            
            if (currentXpBar.fillAmount >= 1f)
            {
                currentXpBar.fillAmount -= 1f;
                
                currentXp = 0f;
                maxXp += 5;
                
                nextLvl++;
                lvl.text = $"{nextLvl}";
            }
            
        }
        
        else if (currentXp <= 10 && maxXp <= 45)
        {
           
            currentXpBar.fillAmount = currentXp / 45;
            
            if (currentXpBar.fillAmount >= 1f)
            {
                currentXpBar.fillAmount -= 1f;
                
                currentXp = 0f;
                maxXp += 5;
                
                nextLvl++;
                lvl.text = $"{nextLvl}";
            }
            
        }
        else if (currentXp <= 10 && maxXp <= 50)
        {
           
            currentXpBar.fillAmount = currentXp / 50;
            
            if (currentXpBar.fillAmount >= 1f)
            {
                currentXpBar.fillAmount -= 1f;
                
                currentXp = 0f;
                maxXp += 5;
                
                nextLvl++;
                lvl.text = $"{nextLvl}";
            }
            
        }

    }
}
