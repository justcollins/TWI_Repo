using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour 
{
	//Open to USER To Set their own Key prefrence, Ideal Keys
	public KeyCode TiltUpward;			// W 		- 
	public KeyCode TiltDownward;		// S 		-
	public KeyCode RotateLeft;			// A 		-
	public KeyCode RotateRight;			// D 		-
	public KeyCode DragStop;			// ShiftL 	-
	public KeyCode ThrustForward;		// Spacebar	-
	public KeyCode EngineSwitchOnOff;	// Enter	- The button for turning on and off the Engine

	public bool EngineOn;				// Turning on and off the engine

	public float ShipConstantSpeed;		// The Ship Constant Speed
	public float ThrustSpeed;			// The Ship Thrust Value


	// Use this for initialization
	void Start () 
	{
		EngineOn = false;
	
	}
	
	// Update is called once per frame
	private void Update () 
	{

		EngineControl ();	//Checks to See if the Engine is on or off
	}

	private void FixUpdate()
	{
		if (EngineOn) 
		{
			//GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * UpDownValue * UpDownVelocity);
			GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * UpDownValue * UpDownVelocity);

			//This cap the submarine from going any higher than 100
			if(transform.position.y > 100)
				//GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * UpDownValue * UpDownVelocity);
				GetComponent<Rigidbody>().AddRelativeForce(-Vector3.up * UpDownValue * UpDownVelocity);

			UpDown = Key
		
		}

	}

	/**
	 * This function will just change Tilt Value depending on what the user
	 * Define their control values for.
	 * Right now it just set to whatever default we have
	 * That would be the TiltUpward and TiltDownward depending how you pass it in
	 * A - TiltUpward
	 * B - TiltDownward
	 */ 

	private float KeyValue(KeyCode A, KeyCode B, float Value, float yValue, float _float, float SmoothTime)
	{
		if (Input.GetKey (A))
			//Do Something
			Value -= Time.deltaTime * _float;
			//Debug.Log ("You pressed " + A);
		else if (Input.GetKey (B))
			//Do the reverse
			Value += Time.deltaTime * _float;
			//Debug.Log ("You pressed " + B);
		else 
		{ //Mean if not no key or non register key press	//Do something
			Value = Mathf.SmoothDamp(Value, 0, ref yValue, SmoothTime);
		}

		Value = Mathf.Clamp (Value, -1, 1);
		return Value;
	}

	private void EngineControl()
	{
		//Switching the Engine on or off when key is pressed
		if (Input.GetKey (EngineSwitchOnOff))
			EngineOn = !EngineOn;

		//In this area is how we control the ship movement speed
		if(EngineOn)
		{
			//Move faster if the thrust is pressed
			if(Input.GetKey (ThrustForward))
			{
				//move faster
			}
		}
		else
		{
			//This should be the place to have the ship return to normal state like no tilt
			//or roatation, return to 0 state
			//Move the ship at constant speed
		}

	}
}
