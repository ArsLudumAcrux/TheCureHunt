﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    SpawnMonsters spawnMonsters;
    public GameObject[] Warps;
    public int monstrosMortos;
    public int monstrosMortos2;
    public int monstrosMortos3;
    ScriptBoss boss;
   // public int monstrosMortos4;
    Hud_Menu HudMenu;

    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<ScriptBoss>();
        spawnMonsters = FindObjectOfType<SpawnMonsters>();
        HudMenu = GameObject.FindGameObjectWithTag("Area").GetComponent<Hud_Menu>();


        for (int i = 0; i < Warps.Length; i++)
        {
            Warps[i].GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    private void Update()
    {
        if (monstrosMortos >= spawnMonsters.maxMonstros)
        {
            Warps[0].GetComponent<BoxCollider2D>().isTrigger = true;
            Warps[1].GetComponent<BoxCollider2D>().isTrigger = true;
            monstrosMortos = 0;

            HudMenu.Placas[0].SetActive(false);
        }
        if (monstrosMortos2 >= spawnMonsters.maxMonstros2)
        {
            Warps[2].GetComponent<BoxCollider2D>().isTrigger = true;
            Warps[3].GetComponent<BoxCollider2D>().isTrigger = true;
            monstrosMortos2 = 0;
            HudMenu.Placas[1].SetActive(false);
        }
        if (monstrosMortos3 >= spawnMonsters.maxMonstros3)
        {
            Warps[4].GetComponent<BoxCollider2D>().isTrigger = true;           
            monstrosMortos3 = 0;
            HudMenu.Placas[2].SetActive(false);
        }
      // if(monstrosMortos4 >= spawnMonsters.maxMonstros4)
      // {
      //     Warps[5].GetComponent<BoxCollider2D>().isTrigger = true;
      //     Warps[6].GetComponent<BoxCollider2D>().isTrigger = true;
      //     monstrosMortos4 = 0;
      //
      // }
      if(boss.morreu == true)
        {
            Warps[5].GetComponent<BoxCollider2D>().isTrigger = true;
        }

    }

}
