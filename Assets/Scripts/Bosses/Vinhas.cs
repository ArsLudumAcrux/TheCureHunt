using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vinhas : MonoBehaviour {

    HealthBar HB;
    PlayerScript player;
    CoolDown cooldown;
    public int Damage;
    bool PodeReceberDano;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerScript>();
        HB = GameObject.FindGameObjectWithTag("Content").gameObject.GetComponent<HealthBar>();
        cooldown = GameObject.FindGameObjectWithTag("CoolDown").gameObject.GetComponent<CoolDown>();

        PodeReceberDano = true;
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && cooldown.EscudosRestante > 0 && PodeReceberDano == true)
        {
            print("NaoRecebeuDano");
            cooldown.Escudo();
            PodeReceberDano = false;
            Invoke("PodeReceber", 1f);
        }
        else if (other.CompareTag("Player") && PodeReceberDano == true)
        {
            print("RecebeuDano");
            HB.HP_Current -= Mathf.RoundToInt(Damage * player.ShieldPotionMult);
            PodeReceberDano = false;
            Invoke("PodeReceber",1f);
        }
    }
    public void PodeReceber()
    {
        PodeReceberDano = true;
    }
}
