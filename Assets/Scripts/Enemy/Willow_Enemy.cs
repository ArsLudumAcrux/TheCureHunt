using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Willow_Enemy : MonoBehaviour {

	// Variables para gestionar el radio de visión y velocidad
	public float visionRadius;
	public float speed;

	public GameObject ObjectPlayer;
	public int WillowHP;


	public AudioClip WillowDamage;
	private AudioSource source;

	// Variable para guardar la posición inicial
	Vector3 actualPosition;

	void Start () {

		// Recuperamos al jugador gracias al Tag
		ObjectPlayer = GameObject.FindGameObjectWithTag("Player");
		WillowHP = 2;

	}

	void Update () {

		//Vida = ObjectPlayer.GetComponent<Statistics>().HP;

		// Por defecto nuestro objetivo siempre será nuestra posición actual
		Vector3 target = actualPosition;


		// Pero si la distancia hasta el jugador es menor que el radio de visión el objetivo será él
		float dist = Vector3.Distance(ObjectPlayer.transform.position, transform.position);
		if (dist < visionRadius) target = ObjectPlayer.transform.position;

		// Finalmente movemos al enemigo en dirección a su target
		float fixedSpeed = speed*Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target, fixedSpeed);

		// Y podemos debugearlo con una línea
		Debug.DrawLine(transform.position, target, Color.green);

		actualPosition = transform.position;

	}

	// Podemos dibujar el radio de visión sobre la escena dibujando una esfera
	void OnDrawGizmos() {

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, visionRadius);

	}

	void OnTriggerEnter2D(Collider2D col) {
        
		if (col.gameObject.CompareTag ("Attack")) {
			
			source.PlayOneShot(WillowDamage, 1.0f);
			Destroy (gameObject);

			WillowHP --;
			if (WillowHP == 0){Destroy (gameObject);}

		}
	}
}