using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

    public AudioSource audiomenu;
    public float Vel;
    public bool posicao;
    public GameObject slime;
    public Animator anim;
    public Transform TranformWayPoint;

    // Use this for initialization
    void Start () {
        Invoke("ComecarMusica", 1.5f);
        StartCoroutine(Esquerda());
    }
    void ComecarMusica()
    {
        audiomenu.Play();
        Invoke("LoopMusica", 32f);
    }
    void LoopMusica()
    {
        audiomenu.Play();
        Invoke("ComecarMusica", 32f);
    }


    void Update()
    {
        if (posicao == true) // Fazer a slime andar para a direita
        {
           slime.transform.Translate(Vector2.right * Vel * Time.deltaTime);
            anim.SetFloat("MovY", TranformWayPoint.position.y);

        }
        if (posicao == false) // Fazer a slime andar para a esquerda
        {
            slime.transform.Translate(Vector2.left * Vel * Time.deltaTime);
            anim.SetFloat("MovY", -TranformWayPoint.position.y);
        }
    }



    IEnumerator Direita() // Tempo para mudar direcao
    {
        posicao = true;
        yield return new WaitForSeconds(12f);
        StartCoroutine(Esquerda());
    }

    IEnumerator Esquerda() // Tempo para mudar direcao
    {
        posicao = false;
        yield return new WaitForSeconds(12f);
        StartCoroutine(Direita());
    }


}
