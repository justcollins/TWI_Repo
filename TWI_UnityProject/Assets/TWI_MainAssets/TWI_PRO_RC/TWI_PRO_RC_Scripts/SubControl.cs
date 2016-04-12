using UnityEngine;
using System.Collections;

//*******************Notes about this script is locate at the very bottom**************************//

public class SubControl : MonoBehaviour {

    //For Turn on and off the engine
    public Submarine_Resources subRes;
    public GameObject exteriorLights;
    public GameObject interiorLights;
    private KeyboardManager keyboard;
	
    [HeaderAttribute("ship values")]
	float UpDownVelocity 			= 0.0f; 
	public float maxThrustValue;		//Max value to reach for thruster
    private float minThrustValue;       //Min value for the thruster
	public float thrust;			//For Debugging purpose otherwise should be private
    public float speed;         //ship speed
    public float boost;         //boost amount 

	float UpDownValue;
	float UpDown;
	float yUpDown;
	
	float Pitch;
	float UpDownTurn;
	float yUpDownTrun;

    [HeaderAttribute("pitch values")]
    public float pitchMin  = -1.0f;
    public float pitchMax  =  1.0f;
    public float cPitchMin = 0.0f;
    public float cPitchMax = 0.0f;

    //yaw
    float yawMin    = -1.0f;
    float yawMax    =  1.0f;
    float cYawMin   =  0.0f;
    float cYawMax   =  0.0f;

	
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
    private float wfX, wfY, wfZ;

    private float curTime = 0.0f;
    private float maxTime = 1.0f;


    void Start()
    {
        keyboard = FindObjectOfType<KeyboardManager>();
        exteriorLights.SetActive(false);
        interiorLights.SetActive(false);
        minThrustValue = -maxThrustValue/2;
    }

    void FixedUpdate()
    {
        worldForce = new Vector3(wfX, wfY, wfZ);
        shipRB.AddForce(worldForce * bloodForce);
	}

    void setStrafeLimit() 
    {
        cPitchMin = Pitch;
        cPitchMax = Pitch;
        cYawMin = Yaw;
        cYawMax = Yaw;
    }


    //////////////////////////// STRAFE MOVEMENT /////////////////////////
	void StrafeMove ()
	{
        Pitch = Mathf.Clamp(Pitch, cPitchMin, cPitchMax);

        Yaw = Mathf.Clamp(Yaw, cYawMin, cYawMax);

		if (Input.GetKey (keyboard.Up))    //move up//
            transform.Translate(Vector3.up * 10f * Time.deltaTime);

		if (Input.GetKey (keyboard.Down))  //move down//
			transform.Translate (Vector3.down * 10f * Time.deltaTime);

        if (Input.GetKey(keyboard.Left))  //move left//
			transform.Translate (Vector3.left * 10f * Time.deltaTime);

		if (Input.GetKey (keyboard.Right)) //move right//
			transform.Translate (Vector3.right * 10f * Time.deltaTime);
	}

    ///////////////////////// ENGINE ON ////////////////////
	void Update ()
	{
        //Debug.Log(thrust);
        //FOR CHECKING IF ENGINE IS ON AND SHIP HAS ENERGY//
        if (Input.GetKeyDown(keyboard.EngineOn) && subRes.getShipEnergy() > 0)
            isEngineOn = !isEngineOn;

        ////////////////////////ACTIVATION OF LIGHTS INSIDE AND OUTSIDE////////////////////////
        if (subRes.getShipEnergy() > 0) {
            if(Input.GetKeyDown(keyboard.ExteriorLights)) {
                exteriorLights.SetActive(!exteriorLights.activeSelf);
            }

            if(Input.GetKeyDown(keyboard.InteriorLights)) {
                interiorLights.SetActive(!interiorLights.activeSelf);
            }
        }

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

        /////////////////////// BOOST ///////////////////////
        

        if (isEngineOn)
        {

            if (Input.GetKeyDown(keyboard.Boost))
            {
                thrust = thrust + boost;
                subRes.setEnergyLevel(-3.0f);
                
            }
			if(Input.GetKey(keyboard.Boost)){
				curTime += Time.deltaTime;
				if (curTime >= maxTime)
				{
					subRes.setEnergyLevel(-3.0f);
					curTime = 0.0f;
				}
			}

            else if (Input.GetKeyUp(keyboard.Boost))
            {
                thrust = thrust - boost;
				curTime = 0.0f;
            }

            if (Input.GetKeyDown(keyboard.MiddleMouse)) // middle mouse button click
            {
                ForBack = !ForBack;

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

            if(Input.GetKeyDown(keyboard.RightMouse)) {
                setStrafeLimit();
            }


            if (Input.GetKey(keyboard.RightMouse)) //if i press right mouse button 
            {
                StrafeMove(); // do strafe movements (bellow)
            }


            
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
            Pitch = Mathf.Clamp(Pitch, pitchMin, pitchMax);

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
            Pitch = Mathf.Clamp(Pitch, pitchMin, pitchMax);                                                                //ERRORERRORERRORERRORERRORERRORERROR

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
    public void setBloodForce(float newBlood)
    {
        bloodForce = newBlood;
    }

    public float getBloodForce()
    {
        return bloodForce;
    }
    public void setWorldForce(Vector3 bloodForce) {
        wfX = bloodForce.x;
        wfY = bloodForce.y;
        wfZ = bloodForce.z;
    }
    public float getMinThrust()
    {
        return minThrustValue;
    }
}
