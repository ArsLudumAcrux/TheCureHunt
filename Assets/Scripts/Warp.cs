using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Warp : MonoBehaviour
{
	// Para armazenar o ponto de destino
	public GameObject target;
	// Para armazenar o mapa de destino
	public GameObject targetMap;

    //Para pegar a camera do minimap
    GameObject miniMap;
    

    // Para controlar se começa ou não a transição
	bool start = false;
    //Para controlar se a transição é de entrada ou saída.
	bool isFadeIn = false;
    // Opacidade inicial do quadro de transição
	float alpha = 0;
    //Transição de 1 segundo
	float fadeTime = 0.8f;


    public PlayerScript player;

	// Area
	 GameObject area;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    void Awake ()
	{
		// Assim nos asseguramos que o TARGET foi estabelecido ou lançaremos o EXCEPT
		Assert.IsNotNull(target);

		// Se quisermos podemos esconder o DEBUG dos WARPS
		GetComponent<SpriteRenderer> ().enabled = false;
		transform.GetChild (0).GetComponent<SpriteRenderer> ().enabled = false;

		Assert.IsNotNull (targetMap);

		// Buscamos a área para mostrar o texto
		area = GameObject.FindGameObjectWithTag("Area");

        //miniMap = GameObject.FindGameObjectWithTag("MiniMapCamera");

	}


	//método para ativar a transição
	IEnumerator OnTriggerEnter2D (Collider2D col) {
		
		// Ao jogador chocar contra o WARP comprovamos se é o jogador e o transportamos  
		if (col.CompareTag ("Player")) {

			//Começamos a transição FADE IN e desativamos os controles de animação e movimento.
			col.GetComponent<Animator> ().enabled = false;
			col.GetComponent<PlayerScript> ().enabled = false;



			FadeIn ();

			//Esperamos o tempo que dura a transição
			yield return new WaitForSeconds (fadeTime);

			// Atualizamos a posição e camera, desfazemos a transição e reativamos os controles.
			col.transform.position = target.transform.GetChild (0).transform.position;

			//Camera.main.GetComponent<MainCamera>().SetBound(targetMap);
            //miniMap.GetComponent<MinMapCamera>().SetBound(targetMap);                
                          
            FadeOut ();

            var myPlayer = FindObjectOfType<PlayerScript>().GetComponent<Transform>();
            Camera.main.transform.position = new Vector3(myPlayer.position.x, myPlayer.position.y, -1);

            col.GetComponent<Animator> ().enabled = true;
            FindObjectOfType<PlayerScript>().speed = 5;
            col.GetComponent<PlayerScript>().enabled = true;

            // Por último mostramos o nome da zona em 2 segundos
            //area.GetComponent<AreaScript>().StartCoroutine(ShowArea(targetMap.name));
            area.GetComponent<AreaScript>().ChamarCoroutine(targetMap.name);
        }
	}

	// Desenhamos um quadrado com opacidade emcima da tela simulando uma transição
	void OnGUI () {

		// Se a transição não começa, saímos do bloco imediatamente.
		if (!start)
			return;

		// Se já começamos, criamos um bloco com opacidade inicial de valor 0.
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);

		// Criamos uma textura temporária para cobrir toda a tela.
		Texture2D tex;
		tex = new Texture2D (1, 1);
		tex.SetPixel (0, 0, Color.black);
		tex.Apply ();

		// Desenhamos a textura sobre toda a tela.
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), tex);

		// Controlamos a transparência.
		if (isFadeIn) {
			// Se for para aparecer a textura, nós somamos opacidade a ela.
			alpha = Mathf.Lerp (alpha, 1.1f, fadeTime * (Time.deltaTime*2.5f));
		} else {
			// Se for para desaparecer a textura, nós reduzimos a opacidade dela.
			alpha = Mathf.Lerp (alpha, -0.1f, fadeTime * (Time.deltaTime*3.5f));

			// Se a opacidade chegar ao valor de 0, desativamos a transição.
			if (alpha < 0) start = false;
		}

	}
    public IEnumerator FadeMorreu()
    {
        string morreu = "Morreu, Game Over!";
        FadeIn();
        var area = FindObjectOfType<AreaScript>();
        area.StartCoroutine(area.Morreu(morreu));
        yield return new WaitForSeconds(fadeTime);
        GameObject.FindGameObjectWithTag("Content").GetComponent<HealthBar>().HPFull();


        if (player.UltimoMapa == "mapa1")
        {
            player.player.transform.position = player.Respawn1.transform.position;
            player.playersprite.enabled = true;
            FadeOut();
        }
        else if (player.UltimoMapa == "mapa2")
        {
            player.transform.position = player.Respawn2.transform.position;
            player.player.SetActive(true);
            FadeOut();
        }
        else if (player.UltimoMapa == "mapa3")
        {
            player.transform.position = player.Respawn3.transform.position;
            player.player.SetActive(true);
            FadeOut();
        }
        else if (player.UltimoMapa == "mapa4")
        {
            player.transform.position = player.Respawn4.transform.position;
            player.player.SetActive(true);
            FadeOut();
        }
       else if (player.UltimoMapa == "mapa5")
       {
           player.transform.position = player.Respawn5.transform.position;
           player.player.SetActive(true);
           FadeOut();
       }
    }
	//método para ativar a transição de entrada.
	public void FadeIn () {
		start = true;
		isFadeIn = true;
	}

	//método para desativar a transição.
	public void FadeOut () {
		isFadeIn = false;
	}
}









/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Warp : MonoBehaviour {

	//Para armazenar o ponto de destino
	public GameObject target;

	//Para armazenar o mapa de destino
	public GameObject targetMap;


	void Awake()
	{
		//Asseguraremos que o alvo foi estabelecido ou lançaremos except
		Assert.IsNotNull(target);

		//Se quisermos podemos esconder as imagens dos Warps
		GetComponent<SpriteRenderer>().enabled = false;
		transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;

		Assert.IsNotNull (targetMap);
	}

	void OnTriggerEnter2D(Collider2D col){

		//Atualizamos a posição
		col.transform.position = target.transform.GetChild (0).transform.position;
		/*if(other.tag == "Player") {
			other.transform.position = target.transform.GetChild(0).transform.position;
		}

		Camera.main.GetComponent<MainCamera>().SetBound(targetMap);
		}

}

*/