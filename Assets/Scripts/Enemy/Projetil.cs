using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetil : MonoBehaviour {

    public float Damage;

    public HealthBar HB;

    CoolDown cooldown;

	// Use this for initialization
	void Start () {
        Invoke("DestroyProjetil", 5.5f);
        HB = GameObject.FindGameObjectWithTag("Content").GetComponent<HealthBar>();
        cooldown = GameObject.FindGameObjectWithTag("CoolDown").GetComponent<CoolDown>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && cooldown.EscudosRestante > 0)
        {
            cooldown.Escudo();
            DestroyProjetil();
        }
        else if(collision.gameObject.CompareTag("Player"))
        {
            PlayerScript player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
            HB.HP_Current -= Mathf.RoundToInt(Damage * player.ShieldPotionMult);
            DestroyProjetil();
        }

    }
    void DestroyProjetil()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
