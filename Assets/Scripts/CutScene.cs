﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : MonoBehaviour {

    public Animation anim;

    public void Awake()
    {
        anim=GetComponent<Animation>();
    }

    public void PlayAnimation()
    {
        //anim.Play("Cutscene Test 2");
        Time.timeScale = 1f;
        Debug.Log("Time scale = 1");
    }
    public void StopAnimation()
    {
        //anim.Stop();
        Time.timeScale = 0f;
        Debug.Log("Time scale = 0");
    }
}
