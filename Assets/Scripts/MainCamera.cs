using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

	Transform target;

    //public float tLX, tLY, bRX, bRY;

    public float smoothTime = 0.2f;
    [SerializeField]
    [Header("BOSQUE")]
    public float xMin1, xMax1, yMin1, yMax1;
    [Space(10)]
    [Header("CAVERNA")]
    public float xMin2, xMax2, yMin2, yMax2;
    [Space(10)]
    [Header("BOSQUE 2")]
    public float xMin3, xMax3, yMin3, yMax3;
    [Space(10)]
    [Header("Sala Chefe")]
    public float xMin4, xMax4, yMin4, yMax4;
    [Space(10)]

    public GameObject Bosque, Caverna, Bosque2, Chefe;

    public string MapaAtual;

	//Vector2 velocity; // necesario para el suavizado de cámara


    void Awake () { 
		target = GameObject.Find("Player").transform;

	}

    void Start()
    {
        //Forçar a resolução da tela em um valor fixo de 1280x720 e em tela cheia.
        Screen.SetResolution(1280, 720, true);
        MapaAtual = GameObject.FindObjectOfType<PlayerScript>().InitialMap.name;
        
    }

    private void FixedUpdate()
    {
        //Forçar a resolução caso ela não seja quadrada em tela cheia.
        if (!Screen.fullScreen || Camera.main.aspect != 1)
        {
            Screen.SetResolution(1280, 720, true);
        }
        //Permitir encerrar o jogo caso for pressionada a tecla ESC.

        if (Input.GetKey("escape")) Application.Quit();

        //Limitar a câmera às bordas da tela.
        //transform.position = new Vector3(
        //    Mathf.Clamp(target.position.x, tLX, bRX),
        //    Mathf.Clamp(target.position.y, bRY, tLY),
        //    transform.position.z
        //);

        ////v3 con límites y suavizado
        ////NOTA: REQUIERE LLAMAR FAST MOVE AL CAMBIAR DE MAPA 

        //float posX = Mathf.Round(
        //    Mathf.SmoothDamp(transform.position.x,
        //        target.position.x, ref velocity.x, smoothTime) * 100) / 100;
        //float posY = Mathf.Round(
        //    Mathf.SmoothDamp(transform.position.y,
        //        target.position.y, ref velocity.y, smoothTime) * 100) / 100;
        //transform.position = new Vector3(
        //    Mathf.Clamp(posX, tLX, bRX),
        //    Mathf.Clamp(posY, bRY, tLY),
        //    transform.position.z
        //);

    }

    //public void SetBound(GameObject map)
    //{
    //    Tiled2Unity.TiledMap config = map.GetComponent<Tiled2Unity.TiledMap>();
    //    float cameraSize = Camera.main.orthographicSize;

    //    tLX = map.transform.position.x + cameraSize;
    //    tLY = map.transform.position.y - cameraSize;
    //    bRX = map.transform.position.x + config.NumTilesWide - cameraSize;
    //    bRY = map.transform.position.y - config.NumTilesHigh + cameraSize;

    //    FastMove();
    //}

    //public void FastMove()
    //{
    //    transform.position = new Vector3(
    //        target.position.x,
    //        target.position.y,
    //        transform.position.z
    //    );
    //}
    void LateUpdate()
    {
        // TENTATIVA DE FAZER A CAMERA MUDAR DE MAPA, POREM FALHOU MISERAVELMENTE //
      //  MapaAtual = GameObject.FindObjectOfType<Warp>().targetMap.name;

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
    }

}