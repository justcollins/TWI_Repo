using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(AudioSource))]

public class introCutscene : MonoBehaviour {

    public MovieTexture movie;
    public MovieTexture successMovie;
    public MovieTexture deathMovie;
    private AudioSource audio;

	void Start () {
        GetComponent<RawImage>().texture = movie as MovieTexture;
        audio = GetComponent<AudioSource>();
        audio.clip = movie.audioClip;
        movie.Play();
        audio.Play();

        StartCoroutine("waitForMovieEnd");
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && movie.isPlaying) {
            Application.LoadLevel(1);
        }
	}

    IEnumerator waitForMovieEnd()
    {
        while (movie.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
        onMovieEnded();
    }

    void onMovieEnded() {
        Application.LoadLevel(1);
    }

}
