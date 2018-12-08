using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixDepth : MonoBehaviour {

	public bool fixEveryFrame;
	SpriteRenderer spr;
    MeshRenderer meshRender;

	void Start () {
		spr = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        //meshRender = GetComponent<MeshRenderer>();
        //spr.sortingLayerName = "Player";
       //spr.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);

        
        if(transform.name != "Player")
        {
            meshRender = GetComponent<MeshRenderer>();
            meshRender.sortingLayerName = "Player";
            meshRender.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
        }
        
    }

	void Update () {
		if (fixEveryFrame) {
            spr.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
           //meshRender.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
        }
	}
}