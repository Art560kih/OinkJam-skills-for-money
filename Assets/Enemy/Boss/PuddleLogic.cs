using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuddleLogic : MonoBehaviour
{
    private float damage = 1f;
    private PlayerLogic _playerLogic;
    private ManegementHpBar _manegementHpBar;

    void Start()
    {
        _playerLogic = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLogic>();
        _manegementHpBar = GameObject.FindGameObjectWithTag("Slider").GetComponent<ManegementHpBar>();
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
             StartCoroutine(Poisoning());
        }
    }

    private IEnumerator Poisoning()
    {
        for (int i = 0; i  < 5; i++)
        {
            yield return new WaitForSeconds(0.5f);
            
            _playerLogic.spriteRenderer.color = new Color(34, 145, 0, 2);
            _playerLogic.health -= damage;
            _manegementHpBar.CurrentHp -= damage;
            yield return new WaitForSeconds(0.5f);
            _playerLogic.spriteRenderer.color = Color.white;
        }
    }

}
