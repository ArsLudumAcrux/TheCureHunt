using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayerVideo : MonoBehaviour {

    Animation cutScene;

    private void Awake()
    {
        cutScene = GetComponent<Animation>();
    }
    public void _Play()
    {
        cutScene.Play();
    }
    public void _Stop()
    {
        cutScene.Stop();
    }
    public void Button()
    {
        _Play();
    }
}
