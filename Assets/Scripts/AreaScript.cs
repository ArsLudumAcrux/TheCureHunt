using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaScript : MonoBehaviour {

	Animator anim;
    public Text textoArea;
    public Text ShadowArea;


    // Use this for initialization
    void Start () {
		anim = GetComponent<Animator>();
		
	}


	// Update is called once per frame
	public IEnumerator ShowArea (string name) {
		anim.Play ("NomeMapa");
        textoArea.text = name;
        ShadowArea.text = name;

        yield return new WaitForSeconds (2f);
		anim.Play ("NomeMapa_Desaparecer");
		
	}
    public IEnumerator Morreu(string morreu)
    {
        anim.Play("NomeMapa");
        textoArea.text = morreu;
        ShadowArea.text = morreu;

        yield return new WaitForSeconds(2f);
        anim.Play("NomeMapa_Desaparecer");
    }
 

    public void ChamarCoroutine(string name)
    {
        StartCoroutine(ShowArea(name));
    }
}
