using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Stats : MonoBehaviour {

    public int Life_Slime;
    public int Atk_Slime;
     Animator anim;
    public bool morreu;
    public int xpMin, xpMax;

    public EnemySlime inimigo;
    ExpBar experiencia;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        inimigo = GetComponent<EnemySlime>();
        experiencia = GameObject.FindGameObjectWithTag("ExpBar").GetComponent<ExpBar>();
    }
    public void Update()
    {     
        anim.SetBool("Morrer", morreu);
        if (morreu)
        {
            
            velocidade(0);
        }
    }
    void velocidade(float speed)
    {
     
            inimigo.speed = speed;
 
    }
    void DestroyObject()
    {
        experiencia.Experiencia(xpMin, xpMax);
        Destroy(gameObject);
    }
}
