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
 
    private float LeftBack;
    private float RightBack;
    private float LeftFront;
    private float RightFront;


	void Start () 
    {
        subThrust = FindObjectOfType<SubControl>();
        thrust = subThrust.thrust;
        wind = GetComponent<WindZone>();
        LeftBack = Transform.localRotation.x; 
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
        //like a car driving in traffic, the car speeds up and the rest seems to 
        //slow down & the car slows down and the cars around it seem to speed up.
        //The ship and particles needs to be able to reproduce this situtation. 
      
        //If players forward direction is inbetween these two points then 
        //increase "main" speed of Windzone
 
        //If players forward direction is inbetween these two points then 
        //decrease "main" speed of Windzone

        //For the front... X is greater than 270 and X is less than 90
        //For the back... X is less than 270 and X is greater than 90



       
    }
	
}
