using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

    public float Damage;

    Slime_Stats slimestats;
    Tronco_Stats troncostats;
    ScriptBoss boss;
    ExpBar expbar;
    // Use this for initialization
    void Start()
    {
        Invoke("DestroyFireball", 3f);
        expbar = GameObject.FindGameObjectWithTag("ExpBar").GetComponent<ExpBar>();

        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<ScriptBoss>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Slime"))
        {
            
            Slime_Stats Slime = collision.GetComponent<Slime_Stats>();
            if (Slime.morreu == false)
            {
                DropCoin drop = collision.GetComponent<DropCoin>();
                Slime.Life_Slime -= Damage;
                DestroyFireball();


                if (Slime.Life_Slime <= 0)
                {
                    Slime.morreu = true;
                    ExpBar expBar = collision.GetComponent<ExpBar>();
                    drop.ChanceCoinPotion();
                    GameManager gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
                    gamemanager.monstrosMortos++;
                    gamemanager.monstrosMortos2++;
                    gamemanager.monstrosMortos3++;
                    DestroyFireball();

                }
            }
            else
            {
                Slime.gameObject.GetComponent<Animator>().SetTrigger("Hit");
                DestroyFireball();
            }
        }
    
        if (collision.gameObject.CompareTag("Tronco"))
        {
            Tronco_Stats Tronco = collision.GetComponent<Tronco_Stats>();

            if (Tronco.morreu == false)
            {
                DropCoin drop = collision.GetComponent<DropCoin>();

                Tronco.Life_Tronco -= Damage;
                DestroyFireball();

                if (Tronco.Life_Tronco <= 0)
                {
                    Tronco.morreu = true;
                    drop.ChanceCoinPotion();
                    GameManager gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
                    gamemanager.monstrosMortos++;
                    gamemanager.monstrosMortos2++;
                    gamemanager.monstrosMortos3++;
                    DestroyFireball();

                }
                else
                {
                    Tronco.gameObject.GetComponent<Animator>().SetTrigger("Hit");
                    DestroyFireball();
                }
            }
        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            boss.Life -= Damage;
            DestroyFireball();
        }

    }
    void DestroyFireball()
    {
        Destroy(gameObject);
    }
}
