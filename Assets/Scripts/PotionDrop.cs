using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PotionDrop : MonoBehaviour {

    public PotionScriptableObj[] potionInfo;
    SpriteRenderer thisSpriteRenderer;

	// Use this for initialization
	void Start () {

        thisSpriteRenderer = GetComponent<SpriteRenderer>();

        if (potionInfo[0] != null)
        thisSpriteRenderer.sprite = potionInfo[0].artwork;
    }
	
	// Update is called once per frame
	void Update () {
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerScript>().AddPotion(potionInfo);
            GameObject.FindGameObjectWithTag("Area").GetComponent<Hud_Menu>().UpdateListItens();
            Destroy(gameObject);
        }
    }
}
