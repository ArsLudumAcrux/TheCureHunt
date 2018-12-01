using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbe : MonoBehaviour
{

    public float Damage;

    public HealthBar HB;
    // Use this for initialization
    void Start()
    {
        Invoke("DestroyOrbe", 3f);
        HB = GameObject.FindGameObjectWithTag("Content").GetComponent<HealthBar>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerScript player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
            HB.HP_Current -= Mathf.RoundToInt(Damage * player.ShieldPotionMult);
            DestroyOrbe();
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