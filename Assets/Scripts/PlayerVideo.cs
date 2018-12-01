using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AnimationCutscene : MonoBehaviour {

    public Animation _cutScene;

    private void Awake()
    {
        _cutScene =this.gameObject.GetComponent<Animation>();
        
    }
    public void PlayAnimation()
    {
        _cutScene.Play("Cutscene Test 2");
    }
    public void Stop()
    {
        _cutScene.Stop("Cutscene Test 2");
    }
    public void Button()
    {
        PlayAnimation();
    }
}
