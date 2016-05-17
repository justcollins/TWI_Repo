/* PROGRAMMER	:	Kien Ngo
 * FILE			:	RandomFlashImage.cs
 * DATE			:	April 28, 2016
 * PURPOSE		:	This will display a random image within a certain time.
 * 
 * NOTE:
 * Remember to change the imported images
 * Texture Type = Spirite(2D and UI)
 * because the images will be place on a canvas with images link to them
 * 
 * INSTRUCTION:
 * 1. In the Hierarchy Right-Mouse-Button click --> UI --> Image
 *      This should create the Canvas too.
 * 2. Canvas
 *      Canvas --> Render Mode = Screen Space - Overlay
 *      Canvas Scaler(Script) --> UI Scale Mode = Scale with Screen Size
 *                                Reference Resolution 
 *                                      x = 1280 or whatever the width will be
 *                                      y = 720  or whatever the height will be
 *                                Match
 *                                      Play around with this one. 
 *                                      0.5 to 1.0
 * 3. Image
 *      Change the Width and Height to the image resolution or Canvas Reference Resolution
 *          1280 X 720
 *      Image(Script) Drag and drop the image into it
 * 
 * 4. Adding more image object into the canvas and Repeat step 3
 * 
 * 5. When you have all the image then go to the object that has this script.
 * 6. If there isn't an AudioSource componet then add one.
 * 7. Drag & Drop all the Images from the Canvas and drop it into the Random Image Section.
 * 8. Change the values under "Delay Time" and "Timer"
 * 9. Add the sound file and that it.
 */ 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RandomFlashImage : MonoBehaviour 
{
	[Header("Delay Time")]//////////////////////////////////////////////////////
	public float minDelay;
	public float maxDelay;
	
    private float delayValue;

    [Header("Timer")]//////////////////////////////////////////////////////
    [Tooltip("The amount of time it take to fade in")]
	public float fadeInTime;

    [Tooltip("The amount of time it will stay in display")]
	public float StayTime;
    
    [Tooltip("The amount of time it take to fade out")]
	public float fadeOutTime;

    [Header("Images")]//////////////////////////////////////////////////////
	[Tooltip("Drag and drop the Images from the canvas in to here")]
	public List <Image> randomImages = new List<Image>();

    [Header("Audio Sounds")] //////////////////////////////////////////////////////
	[Tooltip("The sound you wish to play")]
	public AudioClip sound;
	
    /*
     * Below are Debugging variable purposes
     * Uncomment which ever you need.
     */ 

	//[Header("DEBUGGER")]
    //[SerializeField]
    private float waitTime;
    //[SerializeField]
	private int currentImage;
    //[SerializeField]
	private int nextImage;
    //[SerializeField]
	private float timer;
    //[SerializeField]
	private float delayTimer;
    //[SerializeField]
    private AudioSource playSnd;
    //[SerializeField]
    private bool fadeOutDone;
    //[SerializeField]
	private Color color;            //This is for opacity changes
	
	
	// Use this for initialization
	void Start () 
	{
        //We need to get an AudioSource from our object
        playSnd = GetComponent<AudioSource>();

		fadeOutDone = false;
        timer = 0.0f;
        delayTimer = 0.0f;
        delayValue = Random.Range(minDelay, maxDelay);
		
		waitTime = fadeInTime + StayTime + fadeOutTime;
		
		currentImage = Random.Range (0,randomImages.Count-1);
		
		if (currentImage == nextImage)
			nextImage++;
		
		//Overcome the IndexOutOfBound
		if (nextImage == randomImages.Count)
			nextImage = 0;
		
		//To ensure that all the images in the canvase are turn off and not opaque
		for (int i = 0; i < randomImages.Count; i++)
		{
			randomImages[i].enabled = false;
			//Need to move into the loop
			color = randomImages[i].color;
			color.a = 0.0f;
			randomImages[i].color = color;
		}
		
		////////////////////////////////////////////
		randomImages [currentImage].enabled = true;
		
		//Plays the sound file
        playSnd.clip = sound;
		playSnd.Play();
		
	}//End of Start Method
	
	// Update is called once per frame
	void Update () 
	{
		delayTimer += Time.deltaTime;
		
		if (delayTimer < delayValue)
		{
			if (fadeOutDone == false)
			{
				timer += Time.deltaTime;
				
				//Fade In
				if (timer < fadeInTime)
				{
					color = randomImages[currentImage].color;
					color.a += 0.05f;
					randomImages[currentImage].color = color;
				}
				//Stay
				else if (timer > fadeInTime && timer < StayTime + fadeInTime)
				{
					color = randomImages[currentImage].color;
					color.a = 1.0f;
					randomImages[currentImage].color = color;
				}
				
				//Fade out
				else if (timer > StayTime && timer < waitTime)
				{
					color = randomImages[currentImage].color;
					color.a -= 0.05f;
					randomImages[currentImage].color = color;
				}
				
				//Reset
				//This is total time
				if (timer > waitTime)
				{
					//Turn current Image off
					randomImages[currentImage].enabled = false;
					color = randomImages[currentImage].color;
					color.a = 0.0f;
					randomImages[currentImage].color = color;
					playSnd.Stop();

					//Set the currentImage to the next image;
					currentImage = nextImage;

					//Turn it back on
					randomImages[currentImage].enabled = true;
					
					nextImage = Random.Range(0, randomImages.Count - 1);
					
					if (currentImage == nextImage)
						nextImage++;
					
					//Overcome the IndexOutOfBound
					if (nextImage == randomImages.Count)
						nextImage = 0;
					
					//Reset Timer
					timer = 0.0f;
					fadeOutDone = true;
				}
			}
		}
		else //if (delayTimer > delayValue)
		{
			delayTimer = 0.0f;
			delayValue = Random.Range(minDelay, maxDelay);
			fadeOutDone = false;
			playSnd.Play();
		}
		
	} //End of Update Method
	
}//End of class RandomFlashImage
