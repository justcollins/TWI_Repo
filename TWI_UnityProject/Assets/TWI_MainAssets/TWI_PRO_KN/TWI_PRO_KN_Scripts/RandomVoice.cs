/* PROGRAMMER	:	Kien Ngo
 * FILE			:	RandomVoice.cs
 * DATE			:	May 14, 2016
 * PURPOSE		:	This will play random voice at a certain interval
 * 
 * INSTRUCTION:
 * 1. Create and Empty GameObject (I called it "Voices")
 * 2. Create Empty Child GameObect under "Voices"
 * 3. Add The AudioSource Component
 * 4. Then look for the AudioClip which is the first one on Top in Audio Source
 * 5. Drag and drop the voice into it.
 * 6. Repeat Step 2-6 until you have all the voices you want.
 * 7. When you are done Select all the GameObject with voices and drag & Drop them into this script "Voices" Section 
 * 8. Chnage the Wait Time as this will be the interval delay time.
 * 9. Parent the GameObject from Step one into the player.
 *      That way the audio file will always be with the player.
 */


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomVoice : MonoBehaviour 
{
    [Tooltip("This is the delay value")]
    public float waitTime;

    [Tooltip("Drag and drop the game objects that has the AudioSource on them into here")]
	public List<GameObject> voices = new List<GameObject>();
	
    private int currentVoice;
	private float timer;

	// Use this for initialization
	void Start () 
	{
		currentVoice = Random.Range ( 0, voices.Count - 1);
        
        //We turn off all the game object for the sound so that it does not play all at once when start
        for (int i = 0; i < voices.Count; i++)
            voices[i].SetActive(false);

	}//End of Start Method

    // Update is called once per frame
    void Update() 
	{
		timer += Time.deltaTime;

        //When the timer is reach. Enable the game object and play the sound.
		if (timer > waitTime) 
		{
            voices[currentVoice].SetActive(true);
			voices[currentVoice].GetComponent<AudioSource>().Play();
			timer = 0.0f;
			currentVoice = Random.Range (0, voices.Count - 1);
		}
	}//End of Update
}
