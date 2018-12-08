using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CoolDown : MonoBehaviour {

    public Image cooldown;
    public float TempoCoolDown;
    public float runtimeCoolDown;
    public bool PodeUsar;
    public float fillValueTest;

    [Header("Fire ball config")]
    public Transform FireballEmitor;
    public GameObject FireballPrefab;
    public float FireballSpeed;
    public Transform FireballRotator;
    float DamageFireball;
    [Header("Escudo Config")]
    public Image[] EscudoImage;
    public int EscudosRestante;

    [Space(30)]
    PlayerScript playerscript;

	// Use this for initialization
	void Start () {
        DamageFireball = 30f;
        playerscript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        TempoCoolDown = 15.0f;
        PodeUsar = true;
        cooldown.fillAmount = 0.0f;

        for (int i = 0; i < EscudoImage.Length; i++)
        {
            EscudoImage[i].enabled = false;
        }

	}
    public void Update()
    {
        // if (cooldown.fillAmount <= 0.0f)
        // {
        //     PodeUsar = true;
        // }
        // else
        //     PodeUsar = false;
        for (int i = 0; i < EscudoImage.Length; i++)
        {
            if(i > EscudosRestante - 1)
            {
                EscudoImage[i].enabled = false;
            }
        }

        if (runtimeCoolDown <= Time.time)
        {
            PodeUsar = true;
        }

        fillValueTest = Mathf.Clamp(runtimeCoolDown - Time.time, 0, TempoCoolDown);

        cooldown.fillAmount = fillValueTest / TempoCoolDown;


    }
    // Update is called once per frame
    public void CoolDownMagia(string magiaAtual)
    {
        if (magiaAtual == "Fogo" && PodeUsar)
        {
            GameObject tempFireball = Instantiate(FireballPrefab, FireballEmitor.position, FireballRotator.rotation);
            Rigidbody2D tempRB2D = tempFireball.GetComponent<Rigidbody2D>();
            tempRB2D.AddForce(FireballEmitor.forward * FireballSpeed);
            tempFireball.GetComponent<FireBall>().Damage = DamageFireball;

        } else if (magiaAtual == "Floresta" && PodeUsar)
        {
            EscudosRestante = 3;

            for (int i = 0; i < EscudoImage.Length; i++)
            {
                EscudoImage[i].enabled = true;
            }

        }

        runtimeCoolDown = (TempoCoolDown * playerscript.TimePotionMult ) + Time.time;
        PodeUsar = false;

       //     cooldown.fillAmount = 1.0f;
       // PodeUsar = false;
       // TempoCoolDown -= Time.deltaTime;
       // cooldown.fillAmount = TempoCoolDown; 
       // print("c");

    }
    public void Escudo()
    {
        EscudosRestante--;
    }
}
