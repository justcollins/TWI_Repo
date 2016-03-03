using UnityEngine;
using System.Collections;

//*******************Notes about this script is locate at the very bottom**************************//

public class SubControl : MonoBehaviour {

    //For Turn on and off the engine
    public Submarine_Resources subRes;
    private KeyboardManager keyboard;
	
	float UpDownVelocity 			= 0.0f; 
	public float maxThrustValue 	= 10.0f;		//Max value to reach for thruster
    public float minThrustValue 	= -10.0f;       //Min value for the thruster
	public float thrust 			= 1.0f;			//For Debugging purpose otherwise should be private
    public float speed              = 1.0f;         //ship speed
    public float boost              = 5.0f;         //boost amount 

	float UpDownValue;
	float UpDown;
	float yUpDown;
	
	float Pitch;
	float UpDownTurn;
	float yUpDownTrun;
	
	float Yaw;
	float LeftRightTurn;
	float yLeftRightTurn;
	
	bool isEngineOn = false;

    bool ForBack = true;

    //BloodForce//
    private int sectionInt;
    public Rigidbody shipRB;
    private float bloodForce;
    private Vector3 worldForce;
    private int pressure;


    void Start()
    {
        keyboard = FindObjectOfType<KeyboardManager>();
    }

    void FixedUpdate()
    {
        
        if (sectionInt == 0)
            worldForce = new Vector3(0.0f, 0.0f, 1.0f);
        if (sectionInt == 1)
            worldForce = new Vector3(1.0f, 0.0f, 1.0f);
        if (sectionInt == 2)
            worldForce = new Vector3(0.0f, 0.0f, 0.5f);

        shipRB.AddForce(worldForce * bloodForce);

        ///////////////////////// ENGINE ON ////////////////////
       

	}

    //////////////////////////// STRAFE MOVEMENT /////////////////////////
	void StrafeMove ()
	{
		if (Input.GetKey (keyboard.Up))    //move up//
			transform.Translate (Vector3.up * 10f * Time.deltaTime);

		if (Input.GetKey (keyboard.Down))  //move down//
			transform.Translate (Vector3.down * 10f * Time.deltaTime);

		if (Input.GetKey (keyboard.Left))  //move left//
			transform.Translate (Vector3.left * 10f * Time.deltaTime);

		if (Input.GetKey (keyboard.Right)) //move right//
			transform.Translate (Vector3.right * 10f * Time.deltaTime);
	}
	
	void Update ()
	{
        Debug.Log(thrust);
        //FOR CHECKING IF ENGINE IS ON AND SHIP HAS ENERGY//
        if (Input.GetKeyDown(keyboard.EngineOn) && subRes.getShipEnergy() > 0)
            isEngineOn = !isEngineOn;

        //SHIP ENERGY//
        if (subRes.getShipEnergy() <= 0)
        {
            isEngineOn = false;
            if (thrust > 0)
                thrust -= 0.5f;
            else if (thrust < 0)
                thrust += 0.5f;
            else if (thrust <= 1.0f || thrust >= -1.0f)
                thrust = 0;

        }

        if (Input.GetKeyDown(keyboard.Boost))
        {
            thrust = thrust + boost;
        }

        else if (Input.GetKeyUp(keyboard.Boost))
        {
            thrust = thrust - boost;
        }

        if (isEngineOn)
        {

            if (Input.GetKeyDown(keyboard.MiddleMouse)) // middle mouse button click
            {
                ForBack = !ForBack;

                //thrust = 0.0f;
            }

            //smooth braking
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

            //////////////////////// STRAFE /////////////////////

            if (Input.GetKey(keyboard.RightMouse)) //if i press right mouse button 
            {
                StrafeMove(); // do strafe movements (bellow)
            }

            /////////////////////// BOOST ///////////////////////
            //May change button - Error exists that it doesn't always seem to recognize GetKeyUp on the key.





            //////////////////////// SHIP MOVEMENT ////////////////////

            /*
            USING THE MIDDLE MOUSE SCROLL FOR SPEED OF THE SHIP 
            */
            float VerMoveScroll = Input.GetAxis("Mouse ScrollWheel");

            if (ForBack)
            {

                if (VerMoveScroll > 0f) // for forward //
                {
                    if (thrust <= maxThrustValue)
                    {
                        thrust += speed;

                    }
                    else
                    {
                        thrust = maxThrustValue;

                    }
                }
                else if (VerMoveScroll < 0f) //to slow down// 
                {
                    if (thrust >= 0f)
                    {
                        thrust -= speed / 4;

                    }
                    else
                    {
                        thrust = 0f;

                    }
                }
            }

            else
            {

                if (VerMoveScroll > 0f)//for reverse//
                {
                    if (thrust >= minThrustValue)
                        thrust -= speed;
                    else
                        thrust = minThrustValue;
                }
                else if (VerMoveScroll < 0f) // to slow down in reverse// 
                {
                    if (thrust <= 0f)
                        thrust += speed / 4;
                    else
                        thrust = 0f;
                }
            }



            /////////////////////////// FORWARD MOVEMENT ///////////////////////
            transform.position += transform.forward * Time.fixedDeltaTime * thrust; // for moving forward


            UpDown = KeyValue(keyboard.Up, keyboard.Down, UpDown, yUpDown, 1.5f, 0.1f);

            UpDownTurn = KeyValue(keyboard.Up, keyboard.Down, UpDownTurn, yUpDownTrun, 1.5f, 0.1f);
            LeftRightTurn = KeyValue(keyboard.Left, keyboard.Right, LeftRightTurn, yLeftRightTurn, 1.5f, 0.1f);

            //Pitch//
            Pitch += UpDownTurn * Time.fixedDeltaTime;
            Pitch = Mathf.Clamp(Pitch, -1.2f, 1.2f);

            //Yaw//
            Yaw += LeftRightTurn * Time.fixedDeltaTime;

            //Rotation//
            transform.rotation =
                Quaternion.Slerp(transform.rotation,
                          Quaternion.EulerRotation(Pitch, Yaw, 0.0f), Time.fixedDeltaTime * 1.5f);
        }

       //////////////////////////////////ENGINE OFF/////////////////////////////
        else
        {
            if (thrust > minThrustValue)
                thrust -= 0.001f;
            else
                thrust = minThrustValue;

            transform.position += transform.forward * Time.fixedDeltaTime * thrust;

            UpDown = KeyValue(keyboard.Up, keyboard.Down, UpDown, yUpDown, 0.5f, 0.1f);

            UpDownTurn = KeyValue(keyboard.Up, keyboard.Down, UpDownTurn, yUpDownTrun, 0.5f, 0.1f);
            LeftRightTurn = KeyValue(keyboard.Left, keyboard.Right, LeftRightTurn, yLeftRightTurn, 0.5f, 0.1f);

            //Pitch Value engine off//
            Pitch += UpDownTurn * Time.fixedDeltaTime;
            Pitch = Mathf.Clamp(Pitch, -0.2f, 0.2f);

            //Yaw engine off//
            Yaw += LeftRightTurn * Time.fixedDeltaTime;

            //rotation engine off//
            transform.rotation =
                Quaternion.Slerp(transform.rotation,
                                 Quaternion.EulerRotation(Pitch, Yaw, 0), Time.fixedDeltaTime * 0.5f);
        }
        
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

    public bool getForBack()
    {
        return ForBack;
    }

    public bool getEngineOn()
    {
        return isEngineOn;
    }

    public void setSectionInt(int newSect)
    {
        sectionInt = newSect;
    }

    public int getSectionInt()
    {
        return sectionInt;
    }

    public void setBloodForce(float newBlood)
    {
        bloodForce = newBlood;
    }

    public float getBloodForce()
    {
        return bloodForce;
    }

    public void setPressure(int pres)
    {
        pressure = pres;
    }

    public int getPressure()
    {
        return pressure;
    }
}
