using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vinhas : MonoBehaviour {

    HealthBar HB;
    PlayerScript player;
    CoolDown cooldown;
    Slime_Stats Slime;
    public int Damage;


    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerScript>();
        HB = GameObject.FindGameObjectWithTag("Content").gameObject.GetComponent<HealthBar>();
        cooldown = GameObject.FindGameObjectWithTag("CoolDown").gameObject.GetComponent<CoolDown>();
        Slime = GameObject.FindGameObjectWithTag("Slime").gameObject.GetComponent<Slime_Stats>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        DropCoin drop = other.GetComponent<DropCoin>();
        if (other.CompareTag("Player") && cooldown.EscudosRestante > 0)
        {
            cooldown.Escudo();
        }
        else if (other.CompareTag("Player"))
        {
            HB.HP_Current -= Mathf.RoundToInt(Damage * player.ShieldPotionMult);
        }
        if (HB.HP_Current <= 0)
        {
            player.GetComponent<Animator>().SetTrigger("Death");
            player.PlayerMorreu = true;
        }
        if (other.CompareTag("Slime"))
        {
            if (Slime.morreu == false)
            {
                Slime_Stats Slime = other.GetComponent<Slime_Stats>();
                Slime.Life_Slime -= Damage;
            }
            if (Slime.Life_Slime <= 0)
            {
                Slime.morreu = true;
                ExpBar expBar = other.GetComponent<ExpBar>();
                drop.ChanceCoinPotion();
            }
        }
    }
}

