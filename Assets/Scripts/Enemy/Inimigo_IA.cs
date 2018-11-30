using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Inimigo_IA : MonoBehaviour {

    private GameObject Player;
    private NavMeshAgent NavMesh;

	// Update is called once per frame
	void Start () {

        Player = GameObject.FindWithTag("Player");
        NavMesh = GetComponent<NavMeshAgent>();
	}

    void OnTriggerEnter2D(Collider2D Coll){
        if (Coll.tag == "Player")
            NavMesh.destination = Player.transform.position;

    }
}
