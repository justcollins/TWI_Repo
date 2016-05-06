using UnityEngine;
using System.Collections;

//*******************Notes about this script is locate at the very bottom**************************//

public class SubControlRigidbody : MonoBehaviour
{

    //For Turn on and off the engine
    public Submarine_Resources subRes;
    public GameObject exteriorLights;
    public GameObject interiorLights;
    private KeyboardManager keyboard;

    private Rigidbody rigidbody;
    //public Rigidbody rb;

    [HeaderAttribute("ship values")]    
    float UpDownVelocity = 0.0f;
    public float maxThrustValue;		//Max value to reach for thruster
    private float minThrustValue;       //Min value for the thruster
    public float thrust;			//For Debugging purpose otherwise should be private
    public float speed;         //ship speed
    public float boost;         //boost amount 

    private float UpDownValue;
    private float UpDown;
    private float yUpDown;

    private float Pitch;
    private float UpDownTurn;
    private float yUpDownTrun;

    [HeaderAttribute("pitch values")]
    public float pitchMin = -1.0f;
    public float pitchMax = 1.0f;
    public float cPitchMin = 0.0f;
    public float cPitchMax = 0.0f;

    //yaw
    private float yawMin = -1.0f;
    private float yawMax = 1.0f;
    private float cYawMin = 0.0f;
    private float cYawMax = 0.0f;


    private float Yaw;
    private float LeftRightTurn;
    private float yLeftRightTurn;


    private bool isEngineOn = false;

    private bool ForBack = true;

    //BloodForce//
    private int sectionInt;
    //public Rigidbody shipRB;
    private float bloodForce;
    private Vector3 worldForce;
    private int pressure;
    private float wfX, wfY, wfZ;

    private float curTime = 0.0f;
    private float maxTime = 1.0f;
    public float maxAV = 10;
    private bool keys;

    private void Awake() {

        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        //for keyboard manager, lights, min thrust, and max angular velocity 
        keyboard = FindObjectOfType<KeyboardManager>();
        exteriorLights.SetActive(false);
        interiorLights.SetActive(false);
        minThrustValue = -maxThrustValue / 2; 
        rigidbody.maxAngularVelocity = maxAV;
    }

    void Update()
    {
        ShipValueUpdate();

        if (keys == false)
        {   
            //This is where the ship rotate should stop
            rigidbody.angularVelocity = Vector3.zero;

        }

        if (Input.GetKeyUp(keyboard.Up) &&
            Input.GetKeyUp(keyboard.Down) &&
            Input.GetKeyUp(keyboard.Left) &&
            Input.GetKeyUp(keyboard.Right))
        {
            keys = false;


        }
    }

    void FixedUpdate()
    {
        ShipRigidbodyUpdate();
        worldForce = new Vector3(wfX, wfY, wfZ);
        rigidbody.AddForce(worldForce * bloodForce);
    }

    //strafe clamping
    void setStrafeLimit()
    {
        cPitchMin = Pitch;
        cPitchMax = Pitch;
        cYawMin = Yaw;
        cYawMax = Yaw;
    }


    //////////////////////////// STRAFE MOVEMENT /////////////////////////
    void StrafeMove()
    {
        Pitch = Mathf.Clamp(Pitch, cPitchMin, cPitchMax);

        Yaw = Mathf.Clamp(Yaw, cYawMin, cYawMax);

        if (Input.GetKey(keyboard.Up)) {    //move up//
            //transform.Translate(Vector3.up * 10f * Time.deltaTime);
            rigidbody.AddForce(Vector3.up * 1.0f, ForceMode.VelocityChange);
        }

        if (Input.GetKey(keyboard.Down)) {  //move down//
            //transform.Translate(Vector3.down * 10f * Time.deltaTime);
            rigidbody.AddForce(Vector3.down * 1.0f, ForceMode.VelocityChange);
        }

        if (Input.GetKey(keyboard.Left)) {  //move left//
            //transform.Translate(Vector3.left * 10f * Time.deltaTime);
            rigidbody.AddForce(Vector3.left * 1.0f, ForceMode.VelocityChange);
        }

        if (Input.GetKey(keyboard.Right)) {//move right//
            //transform.Translate(Vector3.right * 10f * Time.deltaTime);
            rigidbody.AddForce(Vector3.right * 1.0f, ForceMode.VelocityChange);
        }
    }

    /*
     * KeyCode A or B is just the button press
     * Value is the passed in will be the new update value
     * _float is the value you want to increase or decrease by
     */ 
    float KeyValue(KeyCode A, KeyCode B, float Value, float yValue, float _float, float SmoothTime)
    {
        if (Input.GetKey(A))
        {
            Value -= Time.deltaTime * _float;
        }
        else if (Input.GetKey(B))
        {
            Value += Time.deltaTime * _float;
        }
        else
        {
            Debug.Log("This happenes");
            Value = Mathf.SmoothDamp(Value, 0, ref yValue, SmoothTime);
            Value = Value * (-1);
            //rigidbody.angularVelocity = Vector3.zero;
        }

        Value = Mathf.Clamp(Value, -1, 1);
        keys = true;
        return Value;
    }

    private void ShipValueUpdate()
    {
        //FOR CHECKING IF ENGINE IS ON AND SHIP HAS ENERGY//
        if (Input.GetKeyDown(keyboard.EngineOn) && subRes.getShipEnergy() > 0)

            isEngineOn = !isEngineOn;

        ////////////////////////ACTIVATION OF LIGHTS INSIDE AND OUTSIDE////////////////////////
        if (subRes.getShipEnergy() > 0)
        {
            if (Input.GetKeyDown(keyboard.ExteriorLights))
            {
                exteriorLights.SetActive(!exteriorLights.activeSelf);
            }

            if (Input.GetKeyDown(keyboard.InteriorLights))
            {
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

            if (Input.GetKey(keyboard.Boost))
            {
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

            //smooth braking for movement//
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

            if (Input.GetKeyDown(keyboard.RightMouse))
            {
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
        } else {
            if (thrust > minThrustValue)
                thrust -= 0.001f;
            else
                thrust = minThrustValue;
        }
    }
    //////////////////// TO TRY TO GET SPEED OF THE SHIP ROTATION USING WHEEL VELOCITY (TEST)/////////////
    
    Vector3 CalcWheelVelocity(Vector3 localWheelPos)
    {
        return rigidbody.GetPointVelocity(transform.TransformPoint(localWheelPos));
    }
   
    private void ShipRigidbodyUpdate() {
        if (isEngineOn)
        {

           //////////////// VELO GETS THE SPEED OF THE ROTATION  ////////////////
            Vector3 velo = CalcWheelVelocity(transform.position);

            
           //note to self up/down = x axis, left/right = y

            /////////////////////////// FORWARD MOVEMENT ///////////////////////
            //transform.position += transform.forward * Time.fixedDeltaTime * thrust; // for moving forward
            rigidbody.AddRelativeForce(transform.forward * Time.fixedDeltaTime * thrust, ForceMode.Acceleration); //*erase comment* added time.fixeddeltatime  

           //UPDOWN//
           // UpDown = KeyValue(keyboard.Up, keyboard.Down, UpDown, yUpDown, 1.5f, 1.0f);
            
            //UPDOWN'S TURNING WHEN YOU PRESS UP OR DOWN//
            UpDownTurn = KeyValue(keyboard.Up, keyboard.Down, UpDownTurn, yUpDownTrun, 1.5f, 0.1f);
            Pitch += UpDownTurn * Time.fixedDeltaTime;
            Pitch = Mathf.Clamp(Pitch, pitchMin, pitchMax);
            //LEFT RIGHTS TURNING WHEN YOU PRESS LEFT OR RIGHT//
            LeftRightTurn = KeyValue(keyboard.Left, keyboard.Right, LeftRightTurn, yLeftRightTurn, 1.5f, 0.1f);
            Yaw += LeftRightTurn * Time.fixedDeltaTime;
            
            //Pitch ***CONTROLS THE UP AND DOWN***//
            //Pitch = Pitch + UpDownTurn;
            //* Time.fixedDeltaTime //same as yaw comment
            //CLAMPS THE PITCH ***CONTROLLS THE LEFT// 
            

            //Yaw//
           //* Time.fixedDeltaTime; doesnt have it normally so if it doesnt work take out time....

           

            //CALCULATES TORQUE FOR PITCH AND YAW (UP DOWN LEFT RIGHT)
            Vector3 myTorque = new Vector3(Pitch, Yaw, 0.0f);
            Debug.Log(myTorque);

            
            //FORCE ON THE TORQUE//
            rigidbody.AddRelativeTorque(myTorque, ForceMode.Force);
            
           
            
        }

        //////////////////////////////////ENGINE OFF/////////////////////////////
        else
        {

            //transform.position += transform.forward * Time.fixedDeltaTime * thrust;

            //FORWARD FORCE FOR DURING ENGINE OFF 
            rigidbody.AddRelativeForce(transform.forward * Time.fixedDeltaTime * thrust, ForceMode.Acceleration); //*erase comment* added time.fixeddeltatime

            //KEYVALUE FOR UP AND DOWN//
            UpDown = KeyValue(keyboard.Up, keyboard.Down, UpDown, yUpDown, 0.5f, 0.1f);

            //KEYVALUE UP AND DOWN TURN//
            UpDownTurn = KeyValue(keyboard.Up, keyboard.Down, UpDownTurn, yUpDownTrun, 0.5f, 0.1f); //.5

            //KEYVALUE LEFT AND RIGHT TURN//
            LeftRightTurn = KeyValue(keyboard.Left, keyboard.Right, LeftRightTurn, yLeftRightTurn, 0.5f, 0.1f);

            //CALCULATES TORQUE FOR PITCH AND YAW (UP DOWN LEFT RIGHT)
            Vector3 myTorque = new Vector3(Pitch, Yaw, 0.0f);
            
            
            //FORCE ON THE TORQUE//
            rigidbody.AddRelativeTorque(myTorque, ForceMode.Force);
        }
    }

    public bool getEngineOn()
    {
        return isEngineOn;
    }
    public float getMinThrust()
    {
        return minThrustValue;
    }
}
