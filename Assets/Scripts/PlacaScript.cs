using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacaScript : MonoBehaviour {

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
            print("Timescale: " + Time.timeScale.ToString());
            StartCoroutine("TempoTexto");
        }
    }
    IEnumerator TempoTexto()
{
        print("Timescale 2: " + Time.timeScale.ToString());
        yield return new WaitForSecondsRealtime(5f);
        PlacaText.gameObject.SetActive(false);
        Hud.gameObject.SetActive(true);
        Time.timeScale = 1;
        print("Timescale 3: " + Time.timeScale.ToString());
    }

}
