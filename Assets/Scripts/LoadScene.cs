using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour {

    public GameObject panel;
    public GameObject Botao;
	// Use this for initialization
	void Start () {
        panel.SetActive(false);
        Botao.SetActive(false);
    }
	
	// Update is called once per frame
	public void CarregarCena(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void AbrirPanel()
    {
        panel.SetActive(true);
        Invoke("AparecerBotao",4f);
    }
    public void FecharPanel()
    {
        panel.SetActive(false);
    }
    public void AparecerBotao()
    {
        Botao.SetActive(true);
    }
}
