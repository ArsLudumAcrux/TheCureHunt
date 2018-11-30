using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour {
    Vector2 Mov;
    public Sprite shadow;
    public CircleCollider2D shadowCollider;
    //GameObject player = GameObject.FindGameObjectWithTag("Player");
    

    // Update is called once per frame
    void Update () {
        if (Mov != Vector2.zero) shadowCollider.offset= new Vector2(Mov.x / 3, Mov.y / 3);
        
       
    }

}
