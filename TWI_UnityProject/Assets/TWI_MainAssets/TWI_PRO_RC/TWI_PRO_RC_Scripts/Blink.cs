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

    flash.enabled = false;
    Worm = FindObjectOfType<Wormhole>();
}

// Update is called once per frame
void Update()
{
    flashBool = flash.enabled;
    if (blinkArea)
    {
       
        curTime += Time.deltaTime;
        if (curTime >= maxTime && PlayCo == false)
        {        
            StartCoroutine(FlashImage());
        }

    }
}



void OnTriggerEnter(Collider col)
{
    if (col.gameObject.tag == "Entry" || col.gameObject.tag == "Exit")
    {
        blinkArea = true;
        Debug.Log("EnterTrigger");
    }
}



    IEnumerator FlashImage() 
    {
      
            
            flash.enabled = true;            
            yield return new WaitForSeconds(0.5f);           
            ImageAlpha(0.75f);
            yield return new WaitForSeconds(0.5f);
            ImageAlpha(0.50f);
            yield return new WaitForSeconds(0.5f);
            ImageAlpha(0.25f);          
            flash.enabled = false;
            Worm.SetJump(true);
            ImageAlpha(1.0f); //sets up the alpha back to 1          
            curTime = 0;
            PlayCo = true;
        

    }

     void ImageAlpha (float iAlpha)
        {
            Color color = flash.color;
            color.a = iAlpha ;
            flash.color = color;
        }
}

