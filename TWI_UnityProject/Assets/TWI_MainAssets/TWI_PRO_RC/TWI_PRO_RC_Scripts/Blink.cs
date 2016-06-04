using UnityEngine;
using System.Collections;

using UnityEngine.UI;


public class Blink : MonoBehaviour {


public bool blinkArea = false;
public bool Image = false;

public Image flash;
public bool flashBool;

[Range(0.0f, 1.0f)]
public float alpha;

public float curTime = 0;
public float maxTime = 2;
private bool PlayCo = false;

private Wormhole Worm;

// Use this for initialization
void Start()
{

    flash.enabled = false; // image named flash is not enabled (not showing)
    Worm = FindObjectOfType<Wormhole>(); // defines worm in my script to access wormhole (andrijas script)
}

void Update()
{
    flashBool = flash.enabled; 
    if (blinkArea)
    {
        curTime += Time.deltaTime;
        if (curTime >= maxTime && PlayCo == false) //if time is more or equals max time and coroutine is not false 
        {        
            StartCoroutine(FlashImage()); //excecutes the coroutine
        }
    }
}

void OnTriggerEnter(Collider col)
{
    if (col.gameObject.tag == "Entry" || col.gameObject.tag == "Exit") // checks if the ship entered the collider named entry and exit 
    {
        blinkArea = true; //if that is true the trigger becomes the blink area  
    }
}

    IEnumerator FlashImage() 
    {
      
            
            flash.enabled = true;
            yield return new WaitForSeconds(0.5f);
            ImageAlpha(1.0f); //starts off with full opacity 

            yield return new WaitForSeconds(0.5f);           
            ImageAlpha(0.75f);                                  // fade starts with .75
            yield return new WaitForSeconds(0.5f);
            ImageAlpha(0.50f);                                  // fade to .5
        yield return new WaitForSeconds(0.5f);
            ImageAlpha(0.25f);                                  //fades to .25
                 
            flash.enabled = false;                              //image named flash becomes false 

            Worm.SetJump(true);                                 // the worm (andrijas script) jump image becomes true 
            ImageAlpha(1.0f);                                   //reset alphas     
            curTime = 0;                                        // the time also resets to 0
            PlayCo = true;// check for if coroutine is working 
        

    }

     void ImageAlpha (float iAlpha) // for accessing the alpha of my image
        {
            Color color = flash.color;
            color.a = iAlpha ;
            flash.color = color;
        }
}

