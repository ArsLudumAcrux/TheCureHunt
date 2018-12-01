using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sword : MonoBehaviour {

    [Tooltip("Dano Atual")]
    public int Sword_CurrentDamage;
    [Tooltip("Espadas")]
    public Sprite[] Swords;
    [Tooltip("Dano Espadas")]
    public int[] Sword_Damage;
    [Tooltip("Botao Espadas")]
    public Button[] Espadas;
    [Tooltip("Espada Atual")]
    public int Sword_Current;
    public Button[] EspadasBorrada;
    public Image[] Cadeado;
    [Tooltip("Chance de critico")]
    public float[] SwordCriticoChance;
    [Tooltip("Chance de critico atual")]
    public float SwordCurrentCriticoChance;

    [Header("Descricao Espadas")]
    public Image[] DescricaoEspadaImg;
    public GameObject[] TxtDescricaoEspada;

    [Space]
    [Tooltip("Borda espada atual")]
    public Image[] BordaEspada;


    // Use this for initialization
    void Start () {
        //Swords[0] = false;
        for (int i = 0; i < DescricaoEspadaImg.Length; i++)
        {
            DescricaoEspadaImg[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < TxtDescricaoEspada.Length; i++)
        {
            TxtDescricaoEspada[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < BordaEspada.Length; i++)
        {
            BordaEspada[i].gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    // Essa void, serve para o jogador poder escolher qual espada ele quer jogar dar 4 opçoes, cada uma com seu dano diferente (podemos colocar o alcance maior em cada uma tambem) //
    public void SwordEscolhida(string Sword)
    {
        if (Sword == "Sword1")
        {
            BordaEspada[0].gameObject.SetActive(true);
            BordaEspada[1].gameObject.SetActive(false);
            BordaEspada[2].gameObject.SetActive(false);
            BordaEspada[3].gameObject.SetActive(false);
            Sword_CurrentDamage = Sword_Damage[0];
            Sword_Current = 0;
            SwordCurrentCriticoChance = SwordCriticoChance[0];
        }
        if (Sword == "Sword2")
        {
            BordaEspada[0].gameObject.SetActive(false);
            BordaEspada[1].gameObject.SetActive(true);
            BordaEspada[2].gameObject.SetActive(false);
            BordaEspada[3].gameObject.SetActive(false);
            Sword_CurrentDamage = Sword_Damage[1];
            Sword_Current = 1;
            SwordCurrentCriticoChance = SwordCriticoChance[1];
        }
        if (Sword == "Sword3")
        {
            BordaEspada[0].gameObject.SetActive(false);
            BordaEspada[1].gameObject.SetActive(false);
            BordaEspada[2].gameObject.SetActive(true);
            BordaEspada[3].gameObject.SetActive(false);
            Sword_CurrentDamage = Sword_Damage[2];
            Sword_Current = 2;
            SwordCurrentCriticoChance = SwordCriticoChance[2];
        }
        if (Sword == "Sword4")
        {
            BordaEspada[0].gameObject.SetActive(false);
            BordaEspada[1].gameObject.SetActive(false);
            BordaEspada[2].gameObject.SetActive(false);
            BordaEspada[3].gameObject.SetActive(true);
            Sword_CurrentDamage = Sword_Damage[3];
            Sword_Current = 3;
            SwordCurrentCriticoChance = SwordCriticoChance[3];
        }
    }
    public void SwordBorrada(string Borrada)
    {

    }
    public void DescricaoEspada(string descricao)
    {
        if (descricao == "Descricao1")
        {
            DescricaoEspadaImg[0].gameObject.SetActive(true);
            DescricaoEspadaImg[1].gameObject.SetActive(false);
            DescricaoEspadaImg[2].gameObject.SetActive(false);
            DescricaoEspadaImg[3].gameObject.SetActive(false);

            TxtDescricaoEspada[0].gameObject.SetActive(true);
            TxtDescricaoEspada[1].gameObject.SetActive(false);
            TxtDescricaoEspada[2].gameObject.SetActive(false);
            TxtDescricaoEspada[3].gameObject.SetActive(false);
        }
        else if (descricao == "Descricao2")
        {
            DescricaoEspadaImg[0].gameObject.SetActive(false);
            DescricaoEspadaImg[1].gameObject.SetActive(true);
            DescricaoEspadaImg[2].gameObject.SetActive(false);
            DescricaoEspadaImg[3].gameObject.SetActive(false);

            TxtDescricaoEspada[0].gameObject.SetActive(false);
            TxtDescricaoEspada[1].gameObject.SetActive(true);
            TxtDescricaoEspada[2].gameObject.SetActive(false);
            TxtDescricaoEspada[3].gameObject.SetActive(false);
        }
        else if (descricao == "Descricao3")
        {
            DescricaoEspadaImg[0].gameObject.SetActive(false);
            DescricaoEspadaImg[1].gameObject.SetActive(false);
            DescricaoEspadaImg[2].gameObject.SetActive(true);
            DescricaoEspadaImg[3].gameObject.SetActive(false);

            TxtDescricaoEspada[0].gameObject.SetActive(false);
            TxtDescricaoEspada[1].gameObject.SetActive(false);
            TxtDescricaoEspada[2].gameObject.SetActive(true);
            TxtDescricaoEspada[3].gameObject.SetActive(false);
        }
        else if (descricao == "Descricao4")
        {
            DescricaoEspadaImg[0].gameObject.SetActive(false);
            DescricaoEspadaImg[1].gameObject.SetActive(false);
            DescricaoEspadaImg[2].gameObject.SetActive(false);
            DescricaoEspadaImg[3].gameObject.SetActive(true);

            TxtDescricaoEspada[0].gameObject.SetActive(false);
            TxtDescricaoEspada[1].gameObject.SetActive(false);
            TxtDescricaoEspada[2].gameObject.SetActive(false);
            TxtDescricaoEspada[3].gameObject.SetActive(true);
        }
    }
}
