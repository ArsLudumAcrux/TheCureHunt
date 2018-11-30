using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEspadaBorrada : MonoBehaviour {

    public string borracha;
    public string nomeSwrd;

    Animator anim;
    string Borrada;
    public void BtnSword(string borracha)
    {
        borracha = nomeSwrd;
        StartCoroutine(Animacao(nomeSwrd));
    }
    IEnumerator Animacao(string nomeSwrd)
    {


        //yield return new WaitForSecondsRealtime(0.1f);
        //anim.SetBool("EspadaBorrada_FadeOut", true);
        //anim.SetBool("EspadaBorrada_Esconder", false);

        //yield return new WaitForSecondsRealtime(2f);
        //anim.SetBool("EspadaBorrada_FadeOut", false);
        //anim.SetBool("EspadaBorrada_Esconder", true);
        if (nomeSwrd == "Sword1")
        {
            anim.Play("Espada_Borrada1_FadeOut");
            yield return new WaitForSecondsRealtime(2f);
            anim.Play("Espada_Borrada1_Esconder");
        }

        if (nomeSwrd == "Sword2")
        {
            anim.Play("Espada_Borrada2_FadeOut");
            yield return new WaitForSecondsRealtime(2f);
            anim.Play("Espada_Borrada2_Esconder");
        }
        if (nomeSwrd == "Sword3")
        {
            anim.Play("Espada_Borrada3_FadeOut");
            yield return new WaitForSecondsRealtime(2f);
            anim.Play("Espada_Borrada3_Esconder");
        }
        if (nomeSwrd == "Sword4")
        {
            anim.Play("Espada_Borrada4_FadeOut");
            yield return new WaitForSecondsRealtime(2f);
            anim.Play("Espada_Borrada4_Esconder");
        }
    }
}
