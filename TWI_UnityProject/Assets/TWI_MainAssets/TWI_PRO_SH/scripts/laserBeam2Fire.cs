using UnityEngine;
using System.Collections;

public class laserBeam2Fire : MonoBehaviour 
{
    public GameObject laserObj;

	void Start () 
    {
        laserObj.SetActive(false);
	}

	void Update () 
    {
	    if(Input.GetKey("space"))
        {
            laserObj.SetActive(true);
        }
        else
        {
            laserObj.SetActive(false);
        }
     }
}
