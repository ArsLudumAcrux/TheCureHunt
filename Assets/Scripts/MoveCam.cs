using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour {

    public Transform TransformPlayer;
    Vector3 offset;


    // Use this for initialization
    void Start()
    {

        offset = transform.position - TransformPlayer.transform.position;
    }

    void Update()
    {
        transform.position = TransformPlayer.position + offset;
    
       
        if (transform.position.x > 0.770f) // Parar camera no vetor x //
        {
            transform.position = new Vector3(0.769f, transform.position.y, 0);
        }
        if (transform.position.x < -5.270f) // Parar camera no vetor x //
        {
            transform.position = new Vector3(-5.268f, transform.position.y, 0);
        }
        if (transform.position.y < 0.380f) // Parar camera no vetor y //
        {
            transform.eulerAngles = new Vector3(transform.position.x, 0.381f, 0);
        }
        if (transform.position.y > 7.970f) // Parar camera no vetor y //
        {
            transform.eulerAngles = new Vector3(transform.position.x, 7.969f, 0);
        }
    }

}