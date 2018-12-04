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
    public bool playerishere;

	// Use this for initialization
	void Start () {
        Tempo = 10f;
        Conjuracao.fillAmount = 0f;
        warp = GameObject.FindGameObjectWithTag("Warp").GetComponent<Warp>();
        Canvas.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        if (RunTime >= Time.time && playerishere)
        {
            warp.FadeIn();
            StartCoroutine(Comecartransicao());
        }
        fillvaluetest = Mathf.Clamp(RunTime - Time.time, 0, Tempo);
        if (playerishere)
        {
            Conjuracao.fillAmount = 1 - (fillvaluetest / Tempo);
       
        }
        else
        {
            Conjuracao.fillAmount = fillvaluetest / Tempo;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerishere = true;

            Canvas.gameObject.SetActive(true);
            RunTime = Tempo + Time.time;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerishere = false;
            Canvas.gameObject.SetActive(false);
        }
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
