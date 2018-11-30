using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script para crear un objeto destruible
public class Destroyable : MonoBehaviour {

	// Variable para guardar el nombre del estado de destruccion
	public string destroyState;
	// Variable con los segundos a esperar antes de desactivar la colisión
	public float timeForDisable;

	public AudioClip Som;
	private AudioSource source;

	public bool DropaMoeda;
	public int MaxDropChance;
	private int DropChance;
	private int DropType;
	public GameObject Moeda1;
	public GameObject Moeda2;
	public GameObject Moeda3;
	public GameObject Moeda4;
	public GameObject Moeda5;

	// Animador para controlar la animación
	Animator anim;

	void Start () {
		anim = GetComponent<Animator>();
		source = GetComponent<AudioSource>();
	}

	// Detectamos la colisión con una corrutina
	IEnumerator OnTriggerEnter2D (Collider2D col) {

		// Si es un ataque
		if (col.tag == "Attack") {

			// Reproducimos la animación de destrucción y esperamos
			anim.Play(destroyState);
			source.PlayOneShot(Som, 1.0f);
			yield return new WaitForSeconds(timeForDisable);


			// Pasados los segundos de espera desactivamos los colliders 2D
			foreach(Collider2D c in GetComponents<Collider2D>()){
				c.enabled = false;
			}

			if (DropaMoeda == true) {

				DropChance = Random.Range (0, MaxDropChance);
				DropType = Random.Range (1, 100);
					if (DropChance == 0)
					{
						
					if (DropType >= 95) {
						Instantiate (Moeda5, transform.position, transform.rotation);
					} else {
						if (DropType >= 80) {
							Instantiate (Moeda4, transform.position, transform.rotation);
						} else {
							if (DropType >= 60) {
								Instantiate (Moeda3, transform.position, transform.rotation);
							} else {
								if (DropType >= 30) {
									Instantiate (Moeda2, transform.position, transform.rotation);
								} else {
									if (DropType >= 1) {
										Instantiate (Moeda1, transform.position, transform.rotation);
									}

								}

							}

						}

					}

				}

			}

		}

	}




	void Update () {

		// "Destruir" el objeto al finalizar la animación de destrucción
		// El estado debe tener el atributo 'loop' a false para no repetirse
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

		if (stateInfo.IsName(destroyState) && stateInfo.normalizedTime >= 1) {
			Destroy(gameObject);
			// En el futuro podríamos almacenar la instancia y su transform
			// para crearlos de nuevo después de un tiempo
		}

	}


}