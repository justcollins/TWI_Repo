using UnityEngine;
using System.Collections;
//Notes about this script is loocate at the very bottom
public class SubmarineControl : MonoBehaviour {

	public KeyCode UP;
	public KeyCode DOWN;
	public KeyCode LEFT;
	public KeyCode RIGHT;

	public KeyCode ENGINE_ON;		//For Turn on and off the engine
	
	float UpDownVelocity 			= 0.0f; 
	public float maxThrustValue 	= 10.0f;		//Max value to reach for thruster
    public float minThrustValue 	= 0.0f;
	public float thrust 			= 1.0f;			//For Debugging purpose otherwise should be private
	
	float UpDownValue;
	float UpDown;
	float yUpDown;
	
	//Rotate Value
	float Pitch;
	float UpDownTurn;
	float yUpDownTrun;
	
	float Yaw;
	float LeftRightTurn;
	float yLeftRightTurn;
	
	bool isEngineOn = false;

	void FixedUpdate()
	{
		if (isEngineOn)
		{
			//Spacebar = Thrust
			//NOTE:
			//Still need to implement the a certain amount of time
			//If power runs out then the thrust must power off.
			if(Input.GetButton("Jump"))
			{
				if(thrust <= maxThrustValue)
					thrust += .1f;
				else
					thrust = maxThrustValue;
			}

			//Left Control = Drag
			if(Input.GetButton("Fire1"))
			{
				if(thrust > minThrustValue)
					thrust -= .1f;
				else
					thrust = minThrustValue;
			}

			transform.position += transform.forward * Time.fixedDeltaTime * thrust;


			UpDown = KeyValue(UP,DOWN , UpDown, yUpDown, 1.5f, 0.1f);

			UpDownTurn = KeyValue(UP, DOWN, UpDownTurn, yUpDownTrun, 1.5f, 0.1f);
			LeftRightTurn = KeyValue(LEFT, RIGHT, LeftRightTurn, yLeftRightTurn, 1.5f, 0.1f);
		
			//Pitch Value
			Pitch += UpDownTurn * Time.fixedDeltaTime;
			Pitch = Mathf.Clamp(Pitch, -1.2f, 1.2f);
			
			//Yaw Value
			Yaw += LeftRightTurn * Time.fixedDeltaTime;

			transform.rotation = 
				Quaternion.Slerp(transform.rotation, 
			              Quaternion.EulerRotation(Pitch, Yaw, 0), Time.fixedDeltaTime * 1.5f);
		}
		else
		{
			if(thrust > minThrustValue)
				thrust -= .001f;
			else
				thrust = minThrustValue;

			transform.position += transform.forward * Time.fixedDeltaTime * thrust;

			UpDown = KeyValue(UP,DOWN , UpDown, yUpDown, .5f, 0.1f);
			
			UpDownTurn = KeyValue(UP, DOWN, UpDownTurn, yUpDownTrun, .5f, 0.1f);
			LeftRightTurn = KeyValue(LEFT, RIGHT, LeftRightTurn, yLeftRightTurn, .5f, 0.1f);
			
			//Pitch Value
			Pitch += UpDownTurn * Time.fixedDeltaTime;
			Pitch = Mathf.Clamp(Pitch, -.2f, .2f);
			
			//Yaw Value
			Yaw += LeftRightTurn * Time.fixedDeltaTime;
			
			transform.rotation = 
				Quaternion.Slerp(transform.rotation, 
				                 Quaternion.EulerRotation(Pitch, Yaw, 0), Time.fixedDeltaTime * .5f);
		}

	}
	
	void Update ()
	{
		if(Input.GetKeyDown(ENGINE_ON))
		   isEngineOn = !isEngineOn;
	}
	
	float KeyValue(KeyCode A,KeyCode B, float Value , float yValue , float _float , float SmoothTime)
	{
		if(Input.GetKey(A))
			Value -= Time.deltaTime * _float;
		else if (Input.GetKey(B))
			Value += Time.deltaTime * _float;
		else
			Value = Mathf.SmoothDamp(Value, 0, ref yValue, SmoothTime);
		
		Value = Mathf.Clamp(Value, -1, 1);
		return Value;
	}
	
	void MotorVelocityContorl()
	{
		if (isEngineOn)
		{
			float Hovering = (Mathf.Abs(GetComponent<Rigidbody>().mass * Physics.gravity.y) / UpDownValue); //for maintain altitude.
			
			if (UpDown != 0.0f)
				UpDownVelocity += UpDown * 0.1f; //if Input Up/Down Axes, Increace UpDownVelocity for Increace altitude.
			else
				UpDownVelocity = Mathf.Lerp(UpDownVelocity, Hovering, Time.deltaTime); //if not input Up/Down Axes, Hovering.
		}
		CheckUpDownVelocity();
	}
	
	void CheckUpDownVelocity()
	{
		if (UpDownVelocity > 1.0f)
			UpDownVelocity = 1.0f;
		else if (UpDownVelocity < 0.1f)
			UpDownVelocity = 0.1f;
	}
	
	

}
