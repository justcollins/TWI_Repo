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
 
    private float LeftCircle;
    private float RightCircle;


	void Start () 
    {
        subThrust = FindObjectOfType<SubControl>();
        thrust = subThrust.thrust;
        wind = GetComponent<WindZone>();
        LeftCircle = transform.localEulerAngles.y - 90;
        RightCircle = transform.localEulerAngles.y + 90;
        Debug.Log("Right Point: " + RightCircle);
        Debug.Log("Left Point: " + LeftCircle);
        wind.windMain = 5.0f;
        
	}

    void Update()
    {
        //For the particles matching the ship movement/speed
        thrust = subThrust.thrust;
        //Debug.Log("Thrust: " + thrust);
        wind.windMain = thrust;
        Debug.Log("Local Rotation: " + subThrust.gameObject.transform.localEulerAngles.y);
        

        //For the effect of the particles moving more realistically 
        //like a car driving in traffic, the car speeds up and the rest seems to 
        //slow down & the car slows down and the cars around it seem to speed up.
        //The ship and particles needs to be able to reproduce this situtation. 
        
        //needs to be done by tomorrow night at the latest tuesday 

        //If players forward direction is inbetween these two points then 
        //increase "main" speed of Windzone
 
        //If players forward direction is inbetween these two points then 
        //decrease "main" speed of Windzone

        //For the front... X is greater than (>=) 270 and X is less than 90(<=)
        //For the back... X is less than 270 and X is greater than 90 

        //Front Part
        //if(RightCircle > LeftCircle) {
        //    Debug.Log("Right is greater than Left");
        //    if (subThrust.gameObject.transform.localEulerAngles.y <= LeftCircle && subThrust.gameObject.transform.localEulerAngles.y <= RightCircle)
        //    {
        //        Debug.Log("Facing Forward");
        //        wind.windMain = 200;
        //    }

        //    //Back Part 
        //    else if (subThrust.gameObject.transform.localEulerAngles.y >= LeftCircle && RightCircle <= subThrust.gameObject.transform.localEulerAngles.y)
        //    {
        //        Debug.Log("Facing Backward");
        //        wind.windMain = 1;
        //    }

        //    else
        //    {
        //        Debug.Log("Well Shit");
        //    }
        //} else {
        //if (RightCircle > LeftCircle)
        //{
        //    Debug.Log("why");
        //    float temp = LeftCircle;
        //    LeftCircle = RightCircle;
        //    RightCircle = temp;
        //}
            //Debug.Log("Left is greater than Right");
            //if the ships rotation is greater than the left point IE: > 270 and the ships rotation is lesser than the right point IE: < 90
        if ((subThrust.gameObject.transform.localEulerAngles.y >= LeftCircle && subThrust.gameObject.transform.localEulerAngles.y >= RightCircle) 
            || (subThrust.gameObject.transform.localEulerAngles.y <= LeftCircle && subThrust.gameObject.transform.localEulerAngles.y <= RightCircle))
            {
                Debug.Log("Facing Forward");
                wind.windMain = 5; 
            }

            //Back Part
            //if the ships rotation is lesser than the left point IE: < 270 and the ships rotation is lesser than the right point IE: < 90
        else if (subThrust.gameObject.transform.localEulerAngles.y >= LeftCircle && subThrust.gameObject.transform.localEulerAngles.y <= RightCircle)
            {
                Debug.Log("Facing Backward");
                wind.windMain = 5;
            }

            else
            {
                Debug.Log("Well Shit");
                Debug.Log("Right Point: " + RightCircle);
                Debug.Log("Left Point: " + LeftCircle);
            }
        //}

    }

    void OnTriggerEnter(Collider Environment)
    {
        if (Environment.gameObject.layer == 8)
        {
            Debug.Log("We hit a thing");
            transform.localEulerAngles = Environment.transform.forward;
            LeftCircle = transform.localEulerAngles.y - 90;//mess wit these local vs not
            RightCircle = RightCircle = transform.localEulerAngles.y + 90;
            Debug.Log("Right Point: " + RightCircle);
            Debug.Log("Left Point: " + LeftCircle);
        }
    }

    public void setWindDir(float wX, float wY, float wZ)
    {
        wind.transform.rotation = new Quaternion(wX, wY, wZ, 1);
    }

}


