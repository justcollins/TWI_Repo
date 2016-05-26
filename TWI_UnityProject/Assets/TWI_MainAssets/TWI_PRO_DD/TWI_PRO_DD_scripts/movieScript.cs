using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(AudioSource))]

public class movieScript : MonoBehaviour
{

    public MovieTexture[] movie = new MovieTexture[4];
    private AudioSource audio;

    private enum moviePlayed { intro = 0, complete = 1, death = 2, credits = 3, none = 4 };
	public static int currentMovie = 4;
    public static bool videoCheck;

    private bool introSwitch;
    private bool successSwitch;
    private bool deathSwitch;
    private bool creditsSwitch;
    

	void Start () {
        audio = GetComponent<AudioSource>();

    }
        
	
	void Update () {

        /*=====================================VIDEO LOADING=====================================*/

        // When Intro Movie is loaded
        if (currentMovie == 0 && videoCheck == true)
        {
            movie[0].Stop();
            Debug.Log("Intro Movie played");
            GetComponent<RawImage>().texture = movie[0] as MovieTexture;
            audio = GetComponent<AudioSource>();
            audio.clip = movie[0].audioClip;
            movie[0].Play();
            audio.Play();

            introSwitch = true;
            successSwitch = false;
            deathSwitch = false;
            creditsSwitch = false;
            videoCheck = false;

            StartCoroutine("waitForMovieEnd");
        }

// When Success Movie is loaded
        else if (currentMovie == 1 && videoCheck == true)
        {
            movie[1].Stop();
            Debug.Log("Success Movie played");
            GetComponent<RawImage>().texture = movie[1] as MovieTexture;
            audio = GetComponent<AudioSource>();
            audio.clip = movie[1].audioClip;
            movie[1].Play();
            audio.Play();

            introSwitch = false;
            successSwitch = true;
            deathSwitch = false;
            creditsSwitch = false;
            videoCheck = false;

            StartCoroutine("waitForMovieEnd");
        }

        // When Death Movie is loaded
        else if (currentMovie == 2 && videoCheck == true)
        {
            movie[2].Stop();
            Debug.Log("Death Movie played");
            GetComponent<RawImage>().texture = movie[2] as MovieTexture;
            audio = GetComponent<AudioSource>();
            audio.clip = movie[2].audioClip;
            movie[2].Play();
            audio.Play();

            introSwitch = false;
            successSwitch = false;
            deathSwitch = true;
            creditsSwitch = false;
            videoCheck = false;

            StartCoroutine("waitForMovieEnd");
        }

        // When Credits Movie is loaded
        else if (currentMovie == 3 && videoCheck == true)
        {
            movie[3].Stop();
            Debug.Log("Credits played");
            GetComponent<RawImage>().texture = movie[3] as MovieTexture;
            audio = GetComponent<AudioSource>();
            audio.clip = movie[3].audioClip;
            movie[3].Play();
            audio.Play();

            introSwitch = false;
            successSwitch = false;
            deathSwitch = false;
            creditsSwitch = true;
            videoCheck = false;

            StartCoroutine("waitForMovieEnd");
        }

        /*======================================CONTROLS======================================*/

        // If Space is pressed while Intro Movie is playing...
        // ... skip movie and load 1st Level
        if (Input.GetKeyDown(KeyCode.Space) && currentMovie == 0){
            Application.LoadLevel("Level 1");
        }
        // ... skip movie and load 2nd Level
        else if (Input.GetKeyDown(KeyCode.Space) && currentMovie == 1){
            Application.LoadLevel("Level 2");
        }
        // ... skip movie and load Start Menu
        else if (Input.GetKeyDown(KeyCode.Space) && currentMovie == 2){
            Application.LoadLevel("Start Menu");
        }
        // ... skip movie and load Start Menu
        else if (Input.GetKeyDown(KeyCode.Space) && currentMovie == 3){
            Application.LoadLevel("Start Menu");
        }
	}

    IEnumerator waitForMovieEnd(){
        // While movies are playing, waits for the end of the frame...
        while (movie[0].isPlaying){
            yield return new WaitForEndOfFrame(); 
        }

        while (movie[1].isPlaying){
            yield return new WaitForEndOfFrame();
        }

        while (movie[2].isPlaying){
            yield return new WaitForEndOfFrame();
        }

        while (movie[3].isPlaying){
            yield return new WaitForEndOfFrame();
        }

        //... Then activate void onMovieEnded()
        onMovieEnded();
    }

    void onMovieEnded() {
        // Load 1st Level when Opening Cutscene ends
        if (introSwitch == true){
            Application.LoadLevel("Level 1");
        }

        // Load 2nd Level when Win Cutscene ends
        else if (successSwitch == true) {
            Application.LoadLevel("Level 2");
        }

        // Load Start Menu when Death Cutscene ends
        else if (deathSwitch == true) {
            Application.LoadLevel("Start Menu");
        }

        // Load Start Menu when Credits ends
        else if (creditsSwitch == true) {
            Application.LoadLevel("Start Menu");
        }
    }
}
