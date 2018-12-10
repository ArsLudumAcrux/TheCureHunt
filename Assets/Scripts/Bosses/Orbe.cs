using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbe : MonoBehaviour
{
    CoolDown cooldown;
    public float Damage;
    PlayerScript player;
    public HealthBar HB;
    // Use this for initialization
    void Start()
    {
        Invoke("DestroyOrbe", 3f);
        HB = GameObject.FindGameObjectWithTag("Content").GetComponent<HealthBar>();
        cooldown = GameObject.FindGameObjectWithTag("CoolDown").GetComponent<CoolDown>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && cooldown.EscudosRestante > 0)
        {
            cooldown.Escudo();
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            PlayerScript player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
            HB.HP_Current -= Mathf.RoundToInt(Damage); //* player.ShieldPotionMult);
            DestroyOrbe();
        }
        if (HB.HP_Current <= 0)
        {
            player.GetComponent<Animator>().SetTrigger("Death");
        }

    }
    void DestroyOrbe()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {

    }
}