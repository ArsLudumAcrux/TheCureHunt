using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MesmaPosicaoEspada : MonoBehaviour {

	public Transform Espada;

	void Update () {

		Espada.transform.position = transform.position;
		
	}
}
