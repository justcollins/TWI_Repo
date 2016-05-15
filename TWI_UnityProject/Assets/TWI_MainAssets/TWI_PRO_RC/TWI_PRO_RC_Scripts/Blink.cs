using UnityEngine;
using System.Collections;

using UnityEngine.UI;


public class Blink : MonoBehaviour {


public bool blinkArea = false;
public bool Image = false;

public Image eyes;
public bool eyebool;

public float curTime = 0;
public float maxTime = 2;

void OnTriggerEnter(Collider col)
{
    if (col.gameObject.tag == "Player")
    {
        blinkArea = true;
        Debug.Log("EnterTrigger");
    }
}

void OnTriggerExit(Collider col)
{
    if (col.gameObject.tag == "Player")
    {
        blinkArea = false;
        Debug.Log("ExitTrigger");
    }
}

void OnTriggerStay()
{
    //blinkArea = 
}

	// Use this for initialization
	void Start () {

        eyes.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        eyebool = eyes.enabled;
        if(blinkArea)
        {
            Debug.Log("I Blinked");
            curTime +=Time.deltaTime;
            Debug.Log("CurTime: " + curTime);
            Debug.Log("MaxTime: " + maxTime);
            if(curTime >= maxTime)
            {
                Debug.Log("ITS WORKING");
                Image = true;
                StartCoroutine(FlashImage());
            }
            
         }
     }

    IEnumerator FlashImage() 
    {
        Debug.Log("were in the corutine");
        if (Image)
        {
            eyes.enabled = true;
            Debug.Log("were waiting");
            yield return new WaitForSeconds(1);
            Debug.Log("were done with the corutine");
            eyes.enabled = false;
            Image = false;
            curTime = 0;
        }

    }
	}

