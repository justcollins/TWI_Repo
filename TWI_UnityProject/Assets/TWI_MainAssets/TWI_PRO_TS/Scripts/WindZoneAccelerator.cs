using UnityEngine;
using System.Collections;


/*
This is going to be applied to the wind zone to change the 
  "main" (how fast the particles move) in relation to how
  fast the ship (player)is moving. 
 * 
 * The ship moves by: the ship thrust varible
 * The wind zone speed is: main
*/



public class WindZoneAccelerator : MonoBehaviour {

    private float thrust;
    private SubControl subThrust;
    private WindZone wind; 
    private float forwardleft;
    private float forwardright;


	void Start () 
    {
        subThrust = FindObjectOfType<SubControl>();
        thrust = subThrust.thrust;
        wind = GetComponent<WindZone>();
        /*forwardleft = transform.localRotation + 90;*/
        //wind.windMain = 5.0f;
	}

    void Update()
    {
        //For the particles matching the ship movement/speed
        thrust = subThrust.thrust;
        Debug.Log("Thrust: " + thrust);
        wind.windMain = thrust;     

        //For the effect of the particles moving more realistically 
        transform.localRotation = 
    }
	
}
