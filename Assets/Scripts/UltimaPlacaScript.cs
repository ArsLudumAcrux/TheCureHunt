using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UltimaPlacaScript : MonoBehaviour {

    public Image PlacaText;
    public GameObject Hud;


    private void Start()
    {
        PlacaText.gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlacaText.gameObject.SetActive(true);
            Hud.gameObject.SetActive(false);
            Time.timeScale = 0;
            StartCoroutine("TempoTexto");
        }
    }
    IEnumerator TempoTexto()
{
        yield return new WaitForSecondsRealtime(3f);
        //PlacaText.gameObject.SetActive(false);
        // Hud.gameObject.SetActive(true);
        //Time.timeScale = 1;
        if (Input.anyKey)
        {
            SceneManager.LoadScene(0);
        }

    }

}
