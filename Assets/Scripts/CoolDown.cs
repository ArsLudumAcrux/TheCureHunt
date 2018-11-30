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

    PlayerScript playerscript;

	// Use this for initialization
	void Start () {

        playerscript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        TempoCoolDown = 7.0f;
        PodeUsar = true;
        cooldown.fillAmount = 0.0f;

	}
    public void Update()
    {
       // if (cooldown.fillAmount <= 0.0f)
       // {
       //     PodeUsar = true;
       // }
       // else
       //     PodeUsar = false;

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
        if (magiaAtual == "Fogo")
        {
            //lógica de fogo;

        } else if (magiaAtual == "Floresta")
        {
            //logica de mato

        }

        runtimeCoolDown = (TempoCoolDown * playerscript.TimePotionMult ) + Time.time;
        PodeUsar = false;

       //     cooldown.fillAmount = 1.0f;
       // PodeUsar = false;
       // TempoCoolDown -= Time.deltaTime;
       // cooldown.fillAmount = TempoCoolDown; 
       // print("c");

    }
}
