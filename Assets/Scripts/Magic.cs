using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Magic : MonoBehaviour {

    [Header ("Gemas")]
    public Button GemaFloresta;
    public Button GemaFogo;
    [Header("Gema Atual")]
    public Image GemaAtual;
    public Sprite GemaVerde;
    public Sprite GemaVerm;
    [Header("Gemas Bloqueadas")]
    public Button[] GemasBloqueadas;
    public Text[] Text;
    [Header("Magia Atual")]
    public string MagiaAtual;
    [Header("Magia HUD")]
    public Sprite BordaFogo;
    public Sprite BordaFloresta;
    public Sprite BordaMagia;
    public Image BordaAtual;

    public bool GemaDesbloq;

    // Use this for initialization





    public void Start()
    {
        GemaDesbloq = false;

        GemaFloresta.gameObject.SetActive(false);
        GemaFogo.gameObject.SetActive(false);

        for (int i = 0; i < GemasBloqueadas.Length; i++)
        {
            GemasBloqueadas[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < Text.Length; i++)
        {
            Text[i].gameObject.SetActive(false);
        }

        BordaAtual.gameObject.SetActive(true);
        
    }
    public void Update()
    {

    }
    //Função de clique para o botão, para que mostre o texto //
    public void GemasBloqText(string name)
    {
        if(name == "Bloq1")
        {
            Text[0].gameObject.SetActive(true);
            StartCoroutine(TimeScaleGema());
        }
        if (name == "Bloq2")
        {
            Text[1].gameObject.SetActive(true);
            StartCoroutine(TimeScaleGema());
        }
        if (name == "Bloq3")
        {
            Text[2].gameObject.SetActive(true);
            StartCoroutine(TimeScaleGema());
        }
        if (name == "Bloq4")
        {
            Text[3].gameObject.SetActive(true);
            StartCoroutine(TimeScaleGema());
        }
        if (name == "Bloq5")
        {
            Text[4].gameObject.SetActive(true);
            StartCoroutine(TimeScaleGema());
        }
        if (name == "Bloq6")
        {
            Text[5].gameObject.SetActive(true);
            StartCoroutine(TimeScaleGema());
        }
    }
    //Invoke para desativar o texto //
    //public void HideText()
    //{
    //    for (int i = 0; i < Text.Length; i++)
    //    {
    //        Text[i].gameObject.SetActive(false);
    //    }
    //}
    public void DisableGemaBloq()
    {
        GemaFogo.gameObject.SetActive(true);
        GemaDesbloq = true;
        PlayerScript player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerScript>();
    }
    public void Gema(string name)
    {
        if (name == "Floresta")
        {
            GemaAtual.GetComponent<Image>().sprite = GemaVerde;
            MagiaAtual = "Floresta";
            BordaAtual.GetComponent<Image>().sprite = BordaFloresta;
        }
        else if (name == "Fogo")
        {
            GemaAtual.GetComponent<Image>().sprite = GemaVerm;
            MagiaAtual = "Fogo";
            BordaAtual.GetComponent<Image>().sprite = BordaFogo;
        }
        else
        {
            MagiaAtual = null;
            BordaAtual.GetComponent<Image>().sprite = BordaMagia;
        }
    }
    
    IEnumerator TimeScaleGema()
    {
        yield return new WaitForSecondsRealtime(2f);
        for (int i = 0; i < Text.Length; i++)
        {
            Text[i].gameObject.SetActive(false);
        }
    }
}
