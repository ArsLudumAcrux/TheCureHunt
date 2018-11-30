using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToWards : MonoBehaviour {

    Transform Target;
    public float RotateSpeed;
    Vector3 upDirection;

	// Use this for initialization
	void Start () {
        upDirection = transform.forward * -1;
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        Vector3 NextDirection = new Vector3(Target.position.x, Target.position.y, transform.position.z) - transform.position;
        Vector3 NewDirection = Vector3.RotateTowards(transform.forward, NextDirection, RotateSpeed, 0);
        transform.rotation = Quaternion.LookRotation(NewDirection, upDirection);

	}
}
