using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {

    public float Tempo;
    public float fillvaluetest;
    public float RunTime;
    public Image Conjuracao;
    Warp warp;
    public GameObject Canvas;
    public GameObject Teleporte;

	// Use this for initialization
	void Start () {
        Tempo = 10f;
        Conjuracao.fillAmount = 0f;
        warp = GameObject.FindGameObjectWithTag("Warp").GetComponent<Warp>();
        Canvas.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

       // if(RunTime >= Time.time)
       // {
       //     warp.FadeIn();
       //     StartCoroutine(Comecartransicao());
       // }


        fillvaluetest = Mathf.Clamp(RunTime - Time.time, Tempo, 0);
        Conjuracao.fillAmount = fillvaluetest / Tempo;
	}

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Canvas.gameObject.SetActive(true);
            RunTime = Tempo + Time.time;
        }
        else
            RunTime = Tempo - Time.time;
    }
    IEnumerator Comecartransicao()
    {
        yield return new WaitForSeconds(5f);
        PlayerScript player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        player.transform.position = Teleporte.transform.position;
        GameObject.FindGameObjectWithTag("Content").GetComponent<HealthBar>().HPFull();
        warp.FadeOut();
    }
}
