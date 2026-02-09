using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManegementHpBar : MonoBehaviour
{
    public Slider currentSlider;
    public Image currentHpBar;
    private PlayerLogic player;
    
    public float currentHp = 100f;
    public float minHp = 0f,  maxHp = 100f;

    public float CurrentHp
    {
        get => currentHp;
        set => currentHp = value;
    }

    void Start()
    {
        currentSlider.maxValue = maxHp;
        currentSlider.minValue = minHp;
    }

    void Update()
    {
        switch (maxHp)
        {
            case 100:
                currentHpBar.fillAmount = currentHp / 100;
                break;
            case 110:
                currentHpBar.fillAmount = currentHp / 110;
                break;
            case 120:
                currentHpBar.fillAmount = currentHp / 120;
                break;
            case 130:
                currentHpBar.fillAmount = currentHp / 130;
                break;
            case 140:
                currentHpBar.fillAmount = currentHp / 140;
                break;
            case 150:
                currentHpBar.fillAmount = currentHp / 150;
                break;
        }
    
        
    }
    
}
