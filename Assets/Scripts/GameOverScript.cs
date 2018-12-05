using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {

    bool podeirmenu;
    public Text continuar;
    public Image continuarimg;
    public AudioSource risada;


    // Para controlar se começa ou não a transição
    bool start = false;
    //Para controlar se a transição é de entrada ou saída.
    bool isFadeIn = false;
    // Opacidade inicial do quadro de transição
    float alpha = 0;
    //Transição de 1 segundo
    float fadeTime = 0.8f;


    // Use this for initialization
    void Start () {
        continuar.enabled = false;
        continuarimg.enabled = false;
        podeirmenu = false;
        StartCoroutine(ContarTempo());
        Invoke("ComecarRisada", 2f);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey && podeirmenu)
        {
            FadeIn();
        }
	}
    void ComecarRisada()
    {
        risada.Play();
        Invoke("LoopRisada", 16f);
    }
    void LoopRisada()
    {
        risada.Play();
        Invoke("ComecarRisada", 16f);
    }


    void OnGUI()
    {

        // Se a transição não começa, saímos do bloco imediatamente.
        if (!start)
            return;

        // Se já começamos, criamos um bloco com opacidade inicial de valor 0.
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);

        // Criamos uma textura temporária para cobrir toda a tela.
        Texture2D tex;
        tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, Color.black);
        tex.Apply();

        // Desenhamos a textura sobre toda a tela.
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), tex);

        // Controlamos a transparência.
        if (isFadeIn)
        {
            // Se for para aparecer a textura, nós somamos opacidade a ela.
            alpha = Mathf.Lerp(alpha, 1.1f, fadeTime * (Time.deltaTime * 2.5f));
        }
        else
        {
            // Se for para desaparecer a textura, nós reduzimos a opacidade dela.
            alpha = Mathf.Lerp(alpha, -0.1f, fadeTime * (Time.deltaTime * 3.5f));

            // Se a opacidade chegar ao valor de 0, desativamos a transição.
            if (alpha < 0) start = false;
        }

    }
    public void FadeIn()
    {
        start = true;
        isFadeIn = true;
        StartCoroutine(FadeTime());
    }

    //método para desativar a transição.
    public void FadeOut()
    {
        
        isFadeIn = false;
        SceneManager.LoadScene(0);
    }

    IEnumerator FadeTime()
    {
        yield return new WaitForSeconds(3f);
        FadeOut();
    }
    IEnumerator ContarTempo()
    {
        yield return new WaitForSeconds(10f);
        podeirmenu = true;
        continuar.enabled = true;
        continuarimg.enabled = true;
    }
}
