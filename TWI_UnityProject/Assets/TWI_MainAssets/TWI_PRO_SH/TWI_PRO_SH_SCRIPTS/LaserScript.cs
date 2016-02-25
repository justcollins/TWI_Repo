using UnityEngine;
using System.Collections;
/*
 Class specifically for the laser weapon
 2/17/16
 Sarah Ho
 script for firing the laser weapons. It will show a laser texture image, collide/hit other objects, and light up
 */
public class LaserScript : MonoBehaviour 
{
	LineRenderer line;
	Light light;
    CursorLockMode lockCursor;

	void Start () 
	{
		line = gameObject.GetComponent<LineRenderer>();
		line.enabled = false; //the pink square is gone at end of line
        light = gameObject.GetComponent<Light>();
		light.enabled = false;

        lockCursor = CursorLockMode.Locked;
        Cursor.visible = false;
	}

	void Update () 
	{
		if(Input.GetButtonDown("Fire1")) //GetButtonDown means when the button is clicked once
		{
			StopCoroutine("FireLaser"); //this is a fail-safe (but coroutine should auto stop)
			StartCoroutine("FireLaser");
		}
	}

	IEnumerator FireLaser() //coroutine needed to keep laser on
	{
		line.enabled = true; //make the line visible
		light.enabled = true;

		while(Input.GetButton ("Fire1")) //GetButton down means while the button is pushed
		{
			//line.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, Time.time);

            Ray ray = new Ray(transform.position, transform.forward); //.position gets the position of the gun, .forward means wherever the x-axis is facing
			RaycastHit hit; //so laser will stop if it collides w/something

			line.SetPosition (0,ray.origin); //line starts at the tip of gun

			//this if-statment determines if the ray hits an object
            if (Physics.Raycast(ray, out hit, 100)) //if it does, make the laser ray STOP at the point of collision
			{
				line.SetPosition(1, hit.point);
				if(hit.rigidbody) //it'll automatically be False and NOT go thro this statment if it doesn't detect a rigidbody
				{
					//hit the obj w/rigidbody with 5 units of force AT obj's arm,head,feet, etc
					hit.rigidbody.AddForceAtPosition(transform.forward * 5, hit.point);
				}
			}
			else//if not, let the laser ray go on until it reaches the limit we put 
			{
				line.SetPosition (1,ray.GetPoint (100)); //line ends at 100 units from gun tip
			}

			yield return null; //every frame it'll keep looping itself while button is pushed
		}
		line.enabled = false; //once unpushed, make the line invisible again
		light.enabled = false;
	}

}
