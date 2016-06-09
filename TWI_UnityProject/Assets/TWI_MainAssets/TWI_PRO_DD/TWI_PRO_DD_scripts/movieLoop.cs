using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class movieLoop : MonoBehaviour {

    public MovieTexture loopedMovie;
    private AudioSource audio;
    private bool loop = true;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        GetComponent<RawImage>().texture = loopedMovie as MovieTexture;
        audio = GetComponent<AudioSource>();
        audio.clip = loopedMovie.audioClip;
        loopedMovie.Play();
        audio.Play();
        loopedMovie.loop = true;
	}
}
