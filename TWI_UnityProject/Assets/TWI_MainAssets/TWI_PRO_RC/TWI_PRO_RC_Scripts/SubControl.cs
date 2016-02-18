using UnityEngine;
using System.Collections;
//Notes about this script is loocate at the very bottom
public class SubControl : MonoBehaviour {

	public KeyCode UP;
	public KeyCode DOWN;
	public KeyCode LEFT;
	public KeyCode RIGHT;

	public KeyCode ENGINE_ON;		//For Turn on and off the engine
	
	float UpDownVelocity 			= 0.0f; 
	public float maxThrustValue 	= 10.0f;		//Max value to reach for thruster
    public float minThrustValue 	= -10.0f;       //Min value for the thruster
	public float thrust 			= 1.0f;			//For Debugging purpose otherwise should be private
    public float speed              = 1.0f;
    public float boost              = 5.0f;

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

    bool ForBack = true;

    bool Boost = true;


	void FixedUpdate()
	{
		if (isEngineOn) //when the engine is on
		{
            //Left Control = Drag

            if (Input.GetMouseButtonDown(2)) // middle mouse button click
            {
                ForBack = !ForBack;

                //thrust = 0.0f;
            }

            //slowly braking
              if (ForBack)
                {
                    if (thrust <= 0f)
                        thrust += 0.3f;
                    
                }
                else
                {
                    if (thrust >= 0f)
                        thrust -= 0.3f;
                   
                }

            //for strife

				if(Input.GetMouseButton (1)) //if i press right mouse button 
				{
					StrifeMove (); // do strife movements (bellow)
				}

                if(Input.GetKeyDown(KeyCode.LeftControl))
                {
                    Boost = !Boost;

                    Debug.Log("CTRL is pressed");

                    if (Boost == true)
                    {
                        thrust = thrust + boost;
                    }
                    else
                    {
                        thrust = thrust - boost;
                    }
                }

            //scrollwheel movement
                float VerMoveScroll = Input.GetAxis("Mouse ScrollWheel");

                if (ForBack)
                {

                    if (VerMoveScroll > 0f) // for forward
                    {
                        if (thrust <= maxThrustValue)
                            thrust += speed;
                        else
                            thrust = maxThrustValue;
                    }
                    else if (VerMoveScroll < 0f) //to stop
                    {
                        if (thrust >= 0f)
                            thrust -= speed/4;
                        else
                            thrust = 0f;
                    }
                }

                else
                {

                    if (VerMoveScroll > 0f)//for reverse
                    {
                        if (thrust >= minThrustValue) //for forward on reverse
                            thrust -= speed;
                        else
                            thrust = minThrustValue;
                    }
                    else if (VerMoveScroll < 0f) // to stop 
                    {
                        if (thrust <= 0f)
                            thrust += speed/4;
                        else
                            thrust = 0f;
                    }
                }


			//Spacebar = Thrust
			//NOTE:
			//Still need to implement the a certain amount of time
			//If power runs out then the thrust must power off.


			transform.position += transform.forward * Time.fixedDeltaTime * thrust; // for moving forward


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

	void StrifeMove ()// strife movements
	{
		if (Input.GetKey (UP))//move up
			transform.Translate (Vector3.up * 10f * Time.deltaTime);

		if (Input.GetKey (DOWN))//move down
			transform.Translate (Vector3.down * 10f * Time.deltaTime);

		if (Input.GetKey (LEFT))//move left
			transform.Translate (Vector3.left * 10f * Time.deltaTime);

		if (Input.GetKey (RIGHT))//move right
			transform.Translate (Vector3.right * 10f * Time.deltaTime);
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
