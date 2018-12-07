using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{

    Transform target;

    public float smoothTime = 0.2f;
    [SerializeField]
    [Header("BOSQUE")]
    public float xMin1, xMax1, yMin1, yMax1;
    [Header("CAVERNA")]
    public float xMin2, xMax2, yMin2, yMax2;
    [Header("BOSQUE 2")]
    public float xMin3, xMax3, yMin3, yMax3;
    [Header("Sala do Chefe")]
    public float xMin4, xMax4, yMin4, yMax4;
    [Header("Fogo")]
    public float xMin5, xMax5, yMin5, yMax5;
    public GameObject Bosque, Caverna, Bosque2, Chefe, Fogo;

    public string MapaAtual;

    public Camera miniMapCan;

    void Awake()
    {
        target = GameObject.Find("Player").transform;
    }

    void Start()
    {
        //Forçar a resolução da tela em um valor fixo de 1280x720 e em tela cheia.
       // Screen.SetResolution(300, 300, true);
        MapaAtual = GameObject.FindObjectOfType<PlayerScript>().InitialMap.name;

    }

    private void FixedUpdate()
    {
        //Forçar a resolução caso ela não seja quadrada em tela cheia.
       // if (!Screen.fullScreen || miniMapCan.aspect != 1)
       // {
       //     Screen.SetResolution(300, 300, true);
       // }
        //Permitir encerrar o jogo caso for pressionada a tecla ESC.

        if (Input.GetKey("escape")) Application.Quit();

    }


    void LateUpdate()
    {
        // TENTATIVA DE FAZER A CAMERA MUDAR DE MAPA, POREM FALHOU MISERAVELMENTE //
        //MapaAtual = GameObject.FindObjectOfType<Warp>().targetMap.name;

        if (MapaAtual == ("Bosque"))
        {
            transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin1, xMax1), Mathf.Clamp(target.position.y, yMin1, yMax1), transform.position.z);
        }
        if (MapaAtual == ("Caverna"))
        {
            transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin2, xMax2), Mathf.Clamp(target.position.y, yMin2, yMax2), transform.position.z);
        }
        if (MapaAtual == ("Bosque2"))
        {
            transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin3, xMax3), Mathf.Clamp(target.position.y, yMin3, yMax3), transform.position.z);
        }
        if (MapaAtual == ("Chefe"))
        {
            transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin4, xMax4), Mathf.Clamp(target.position.y, yMin4, yMax4), transform.position.z);
        }
        if (MapaAtual == ("Fogo"))
        {
            transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin5, xMax5), Mathf.Clamp(target.position.y, yMin5, yMax5), transform.position.z);
        }
    }

}
