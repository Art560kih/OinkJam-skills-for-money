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
        currentHpBar.fillAmount = currentHp / 100;
    }
    
}
