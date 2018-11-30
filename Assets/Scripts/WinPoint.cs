using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class WinPoint: MonoBehaviour {


	void OnTriggerEnter2D (Collider2D col) {

		if (col.CompareTag ("Player")) {
			SceneManager.LoadScene ("5-YouWin");
		}
	}

}
