using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vinhas : MonoBehaviour {

    HealthBar HB;
    PlayerScript player;
    CoolDown cooldown;
    Slime_Stats Slime;
    public int Damage;
    bool PodeReceberDano;


    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerScript>();
        HB = GameObject.FindGameObjectWithTag("Content").gameObject.GetComponent<HealthBar>();
        cooldown = GameObject.FindGameObjectWithTag("CoolDown").gameObject.GetComponent<CoolDown>();
        Slime = GameObject.FindGameObjectWithTag("Slime").gameObject.GetComponent<Slime_Stats>();

        PodeReceberDano = true;
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        DropCoin drop = other.GetComponent<DropCoin>();
        if (other.CompareTag("Player") && cooldown.EscudosRestante > 0 && PodeReceberDano == true)
        {
            cooldown.Escudo();
            PodeReceberDano = false;
            Invoke("PodeReceber", 1f);
        }
        else if (other.CompareTag("Player") && PodeReceberDano == true)
        {
            HB.HP_Current -= Mathf.RoundToInt(Damage * player.ShieldPotionMult);
            PodeReceberDano = false;
            Invoke("PodeReceber", 1f);
        }
        if (HB.HP_Current <= 0)
        {
            player.GetComponent<Animator>().SetTrigger("Death");
            player.PlayerMorreu = true;
        }
        if (other.CompareTag("Slime"))
        {
            Slime_Stats Slime = other.GetComponent<Slime_Stats>();
            Slime.Life_Slime -= Damage;
            print(Slime.Life_Slime -= Damage);
        }
        if (Slime.Life_Slime <= 0)
        {
            Slime.morreu = true;
            ExpBar expBar = other.GetComponent<ExpBar>();
            drop.ChanceCoinPotion();
        }
    }
    public void PodeReceber()
    {
        PodeReceberDano = true;
    }
}
