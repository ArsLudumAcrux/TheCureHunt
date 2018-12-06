using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayerVideo : MonoBehaviour {

    //public Animator cutScene;

    //private void Awake()
    //{
    //    cutScene = GetComponent<Animator>();
    //}
    public void PlayAnim()
    {
        //cutScene.Play("Cutscene Test 2");
        Time.timeScale = 1f;
    }
    public void StopAnim()
    {
        Time.timeScale = 0f;
    }
    public void Button()
    {
        PlayAnim();
    }
}
