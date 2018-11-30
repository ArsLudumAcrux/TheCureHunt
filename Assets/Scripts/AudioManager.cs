using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instancia;
    AudioSource source;

    public AudioClip attackClip;

	// Use this for initialization
	void Start () {
		if(instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }

        source = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    public void PlaySFX(AudioClip sfxToPlay) {
        source.PlayOneShot(sfxToPlay);
    }

}
