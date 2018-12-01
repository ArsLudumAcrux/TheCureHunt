using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

    public float Damage;

    Slime_Stats slimestats;
    Tronco_Stats troncostats;
    ScriptBoss boss;
    // Use this for initialization
    void Start()
    {
        Invoke("DestroyOrbe", 3f);
        slimestats = GameObject.FindGameObjectWithTag("Slime").GetComponent<Slime_Stats>();
        troncostats = GameObject.FindGameObjectWithTag("Tronco").GetComponent<Tronco_Stats>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<ScriptBoss>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Slime"))
        {
            slimestats.Life_Slime -= Damage;
            DestroyFireball();
        }
        if (collision.gameObject.CompareTag("Tronco"))
        {
            troncostats.Life_Tronco -= Damage;
            DestroyFireball();
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
