using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToWardsFireball : MonoBehaviour
{

    public Transform Target;
    public float RotateSpeed;
    Vector3 upDirection;
    Vector2 Mov;
    public Transform player;

    // Use this for initialization
    void Start()
    {
        upDirection = transform.forward * -1;


    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 NextDirection = new Vector3(Target.position.x, Target.position.y, transform.position.z) - transform.position;
        Vector3 NewDirection = Vector3.RotateTowards(transform.forward, NextDirection, RotateSpeed, 0);
        transform.rotation = Quaternion.LookRotation(NewDirection, upDirection);


        Mov = new Vector2(
    Input.GetAxisRaw("Horizontal"),
    Input.GetAxisRaw("Vertical"));

        if (Mov != Vector2.zero)
        {
            Target.position = player.position + new Vector3(Mov.x, Mov.y, 0);
        }
    }
}
