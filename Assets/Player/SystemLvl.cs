using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class SystemLvl : MonoBehaviour
{
    private MenegmentXpBar menegmentXpBar;
    
    public GameObject choucePanel;
    public TextMeshProUGUI chouceText;

    private int lvl = 0;
    private bool isClicked;

    private void Start()
    {
        menegmentXpBar = GetComponent<MenegmentXpBar>();
    }

  

    public int GetLevel
    {
        get => lvl;
        set => lvl = value;
    }

}







