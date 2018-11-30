using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Dmg : MonoBehaviour {

	private AudioSource source;

	public int Vida;
	public GameObject ObjectPlayer;
	public AudioClip DmgSound;

	void Awake () {

		Vida = 5;

	}

	void Start () {
		ObjectPlayer = GameObject.FindGameObjectWithTag("Player");
	}


	void OnTriggerEnter2D(Collider2D col) {
		/*if (col.CompareTag ("Player")) {
				Vida--;
			}*/

		if (col.gameObject.CompareTag ("Player"))
		{


			source.PlayOneShot(DmgSound, 5.0f);
			//Vida = ObjectPlayer.GetComponent<Statistics>().HP;
			Vida --;
			//ObjectPlayer.GetComponent<Statistics>().HP_Max = Vida;


		}

		}
	}