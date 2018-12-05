using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEspadaBorrada : MonoBehaviour {

    public string borracha;
    public string nomeSwrd;

    Statistics playerstats;
    Animator anim;
    string Borrada;

    public Animator[] EspadasBorradas;


    public void Start()
    {
        playerstats = GameObject.FindGameObjectWithTag("Player").GetComponent<Statistics>();
        anim = GetComponent<Animator>();
        for (int i = 0; i < EspadasBorradas.Length; i++)
        {
            EspadasBorradas[i] = GetComponent<Animator>();
        }
    }


    public void BtnSword()
    {
        StartCoroutine(EspadaBorradaAnim());
    }
    IEnumerator EspadaBorradaAnim()
    {
        if (playerstats.Level == 1)
        {
            EspadasBorradas[0].Play("Espada_Borrada1_FadeOut");
            yield return new WaitForSecondsRealtime(2f);
            EspadasBorradas[0].Play("Espada_Borrada1_Esconder");
        }else if (playerstats.Level == 2)
        {
            EspadasBorradas[0].Play("Espada_Borrada1_FadeOut");
            EspadasBorradas[1].Play("Espada_Borrada2_FadeOut");
            yield return new WaitForSecondsRealtime(2f);
            EspadasBorradas[0].Play("Espada_Borrada1_Esconder");
            EspadasBorradas[1].Play("Espada_Borrada2_Esconder");
        }else if (playerstats.Level == 3)
        {
            EspadasBorradas[0].Play("Espada_Borrada1_FadeOut");
            EspadasBorradas[1].Play("Espada_Borrada2_FadeOut");
            EspadasBorradas[2].Play("Espada_Borrada3_FadeOut");
            yield return new WaitForSecondsRealtime(2f);
            EspadasBorradas[0].Play("Espada_Borrada1_Esconder");
            EspadasBorradas[1].Play("Espada_Borrada2_Esconder");
            EspadasBorradas[2].Play("Espada_Borrada3_Esconder");
        }else if (playerstats.Level == 4)
        {
            EspadasBorradas[0].Play("Espada_Borrada1_FadeOut");
            EspadasBorradas[1].Play("Espada_Borrada2_FadeOut");
            EspadasBorradas[2].Play("Espada_Borrada3_FadeOut");
            EspadasBorradas[3].Play("Espada_Borrada4_FadeOut");
            yield return new WaitForSecondsRealtime(2f);
            EspadasBorradas[0].Play("Espada_Borrada1_Esconder");
            EspadasBorradas[1].Play("Espada_Borrada2_Esconder");
            EspadasBorradas[2].Play("Espada_Borrada3_Esconder");
            EspadasBorradas[3].Play("Espada_Borrada4_Esconder");
        }
    }
}
