
using UnityEngine;
using UnityEngine.UI;

public class HpBarBoss : MonoBehaviour
{
    public Slider currentSlider;
    public Image currentHpBar;
    
    public float currentHp = 500f;
    public float minHp = 0f,  maxHp = 500f;

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
        if (CurrentHp <= maxHp)
        {
            currentHpBar.fillAmount = CurrentHp / maxHp;
        }
    }
}
