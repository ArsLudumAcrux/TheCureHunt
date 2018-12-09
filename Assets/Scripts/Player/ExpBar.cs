using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour {

    public Statistics stat;//variavel referente ao script Statistics
    public Image experiencia_img;
    public float expCur;
    public int danoplayer;
    Slime_Stats slime_stats;
    PlayerScript playerScript;
    HealthBar HB;
    public bool PodeUparNivel;

    [Header("Text Status Atualizados")]
    public Text danoTxt;
    public Text speedTxt;

    public void Start()
    {
        PodeUparNivel = true;

        danoTxt.gameObject.SetActive(false);
        speedTxt.gameObject.SetActive(false);
        //experiencia_img.fillAmount = 0;
        stat = FindObjectOfType<Statistics>();//pegando o script Statistics
        playerScript = FindObjectOfType<PlayerScript>();
        experiencia_img = GetComponent<Image>();// Pegando a imagem pra utilizar o fillAmount   
        HB = FindObjectOfType<HealthBar>();
    }
    public void Update()
    {
        experiencia_img.fillAmount = stat.ExpAtual / stat.XPToNextLevel ;


        if (stat.ExpAtual >= stat.XPToNextLevel)//caso a experiencia atual seja igual xp necessario para o proximo nivel executa as ações abaixo
        {
            if (stat.Level <= 10 && PodeUparNivel == true) // Isso é um limitador, para que o level nao ultrapasse o maximo, que é 10 
            {
                stat.Level = stat.Level + 1;//level atual +1

                stat.LevelAtual(); // Aparecer para o jogador que o arthur upou de nivel


                stat.XPToNextLevel = Mathf.Round(stat.XPToNextLevel + stat.XPToNextLevelFixed);//xp necessario para o proximo nivel aumenta

                stat.HP_Max = Mathf.Round(stat.HP_Max * 1.10f);//Vida maxima aumenta 10% a cada nivel do player

                stat.strongh = Mathf.Round(stat.strongh * 1.02f);

                danoplayer += 2; // Adiciona +2 de dano ao player

                playerScript.speed = (playerScript.speed * 1.04f); // Adiciona 4% de velocidade ao player

                danoTxt.gameObject.SetActive(true); //Mostrar o dano recebido
                speedTxt.gameObject.SetActive(true); //Mostrar a velocidade recebida
                Invoke("StatusAtualizados", 4f); //Desativar os Txt de cima

                // playerScript.speed= Mathf.Round(stat.strongh * 1.01f);

                //source.PlayOneShot(LevelUp, 1.0f);

                stat.LevelText.text = stat.Level.ToString();//text do cavas atualiza a cada nivel e mostra o nivel atual

                stat.ExpAtual = 0; // Zerar a experiencia do arthur

                //Printa o level e vida maxima do player

                PlayerPrefs.SetInt("LevelDoPlayer", stat.Level);

                //Debug.Log("LevelDoPlayer" + stat.Level);
            }

        }
    }
    public void Experiencia(int xpMin, int xpMax)
    {
        int Exp = Random.Range(xpMin, xpMax); //Pegar o valor minimo e maximo, e pegar um valor randomico deles, e colocar na experiencia do arthur
        stat.ExpAtual += Exp;
        //experiencia_img.fillAmount = stat.ExpAtual / stat.ExpAtual;
    }
    public void StatusAtualizados()
    {
        danoTxt.gameObject.SetActive(false);
        speedTxt.gameObject.SetActive(false);
    }
}
