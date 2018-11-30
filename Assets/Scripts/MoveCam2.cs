using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam2 : MonoBehaviour
{

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


        if (transform.position.x > 0.56f) // Parar camera no vetor x //
        {
            transform.position = new Vector3(0.54f, transform.position.y, 0);
        }
        if (transform.position.x < -5.06f) // Parar camera no vetor x //
        {
            transform.position = new Vector3(-5.04f, transform.position.y, 0);
        }
        if (transform.position.y < 1.350f) // Parar camera no vetor y //
        {
            transform.eulerAngles = new Vector3(transform.position.x, 1.351f, 0);
        }
        if (transform.position.y > 6.94f) // Parar camera no vetor y //
        {
            transform.eulerAngles = new Vector3(transform.position.x, 6.92f, 0);
        }
    }
}

