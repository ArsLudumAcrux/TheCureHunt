﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSword : MonoBehaviour {

    public Sword sword;
    public Statistics stats;
    public float critico;
    public int calculo;
    ExpBar expbar;
    //ExpBar expBar;


    public void Start()
    {
        expbar = GameObject.FindGameObjectWithTag("ExpBar").GetComponent<ExpBar>();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        // Se a espada colidir com uma slime, vai dar dano //
        if (collision.CompareTag("Slime"))
        {
            critico = Random.Range(1, 101);
            Slime_Stats Slime = collision.GetComponent<Slime_Stats>();
            if (Slime.morreu == false)
            {
                DropCoin drop = collision.GetComponent<DropCoin>();

                if (critico <= sword.SwordCurrentCriticoChance)
                {
                    calculo = expbar.danoplayer + sword.Sword_CurrentDamage * 2;
                    Slime.Life_Slime -= calculo;

                }
                else
                {
                    calculo = expbar.danoplayer + sword.Sword_CurrentDamage;
                    Slime.Life_Slime -= calculo;
                }

                // Se a slime morrer, faz a animacao, conta mais uma morte no gamemanager e da um drop de item // 
                if (Slime.Life_Slime <= 0)
                {
                    Slime.morreu = true;
                    ExpBar expBar = collision.GetComponent<ExpBar>();
                    drop.ChanceCoinPotion();
                    //expBar.Experiencia();
                    GameManager gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
                    gamemanager.monstrosMortos++;
                    gamemanager.monstrosMortos2++;
                    gamemanager.monstrosMortos3++;

                }
                // Se a slime for atacada, faz animacao de hit //
                else
                {
                    Slime.gameObject.GetComponent<Animator>().SetTrigger("Hit");
                }
            }
        }
        // Se a espada colidir com o tronco, da dano nele //
        if (collision.CompareTag("Tronco"))
        {
            critico = Random.Range(1, 101);
            Tronco_Stats Tronco = collision.GetComponent<Tronco_Stats>();
            if (Tronco.morreu == false)
            {
                DropCoin drop = collision.GetComponent<DropCoin>();

                if (critico <= sword.SwordCurrentCriticoChance)
                {
                    Tronco.Life_Tronco -= sword.Sword_CurrentDamage * 2;
                }
                else
                    Tronco.Life_Tronco -= sword.Sword_CurrentDamage;

                // Se o tronco morrer, faz a animacao, conta mais uma morte no gamemanager e da um drop de item // 
                if (Tronco.Life_Tronco <= 0)
                {
                    Tronco.morreu = true;
                    ExpBar expBar = collision.GetComponent<ExpBar>();
                    drop.ChanceCoinPotion();
                    //expBar.Experiencia(Tronco.xpMin, Tronco.xpMax);
                    GameManager gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
                    gamemanager.monstrosMortos++;
                    gamemanager.monstrosMortos2++;
                    gamemanager.monstrosMortos3++;

                }
                // Se o tronco for atacado, faz animacao de hit //
                else
                {
                    Tronco.gameObject.GetComponent<Animator>().SetTrigger("Hit");
                }
            }
        }
        // Se a espada colidir com o chefe, da dano //
        if (collision.CompareTag("Boss"))
        {
            critico = Random.Range(1, 101);
            ScriptBoss boss = collision.GetComponent<ScriptBoss>();
            if (critico <= sword.SwordCurrentCriticoChance)
            {
                boss.Life -= sword.Sword_CurrentDamage * 2;
                boss.gameObject.GetComponent<Animator>().SetTrigger("hit");
            }
            // Animacao de dano no chefe //
            else
            {
                boss.Life -= sword.Sword_CurrentDamage;
                boss.gameObject.GetComponent<Animator>().SetTrigger("hit");
            }

        }

    }
}
