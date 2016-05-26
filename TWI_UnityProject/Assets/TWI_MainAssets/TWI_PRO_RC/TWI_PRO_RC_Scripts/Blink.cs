using UnityEngine;
using System.Collections;

using UnityEngine.UI;


public class Blink : MonoBehaviour {


public bool blinkArea = false;
public bool Image = false;

public Image flash;
public bool flashBool;

public float curTime = 0;
public float maxTime = 2;

// Use this for initialization
void Start()
{

    flash.enabled = false;
}

// Update is called once per frame
void Update()
{
    flashBool = flash.enabled;
    if (blinkArea)
    {
        Debug.Log("I Blinked");
        curTime += Time.deltaTime;
        Debug.Log("CurTime: " + curTime);
        Debug.Log("MaxTime: " + maxTime);
        if (curTime >= maxTime)
        {
            Debug.Log("ITS WORKING");
            Image = true;
            StartCoroutine(FlashImage());
        }

    }
}

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


    IEnumerator FlashImage() 
    {
        Debug.Log("were in the corutine");
        if (Image)
        {
            flash.enabled = true;
            Debug.Log("were waiting");
            yield return new WaitForSeconds(0.05f);
            Debug.Log("were done with the corutine");
            flash.enabled = false;
            Image = false;
            curTime = 0;
        }

    }
	}

