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
    private new Rigidbody rigidbody;
    public Rigidbody rb;

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
    private float maxAV = 2;
    private bool keys;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
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
            rigidbody.angularVelocity = Vector3.zero;

        }
        if (Input.GetKeyUp(keyboard.Up))
        {
            keys = false;


        }

        if (Input.GetKeyUp(keyboard.Down))
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

    float KeyValue(KeyCode A, KeyCode B, float Value, float yValue, float _float, float SmoothTime)
    {
        if (Input.GetKey(A))
            Value -= Time.deltaTime * _float;
        else if (Input.GetKey(B))
            Value += Time.deltaTime * _float;
        else
            Value = Mathf.SmoothDamp(Value, 0, ref yValue, SmoothTime);

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
    Vector3 CalcWheelVelocity(Vector3 localWheelPos)
    {
        return rigidbody.GetPointVelocity(transform.TransformPoint(localWheelPos));
    }
   
    private void ShipRigidbodyUpdate() {
        if (isEngineOn)
        {

           
            Vector3 velo = CalcWheelVelocity(transform.position);

            
           //note to self up/down = x axis, left/right = y

            /////////////////////////// FORWARD MOVEMENT ///////////////////////
            //transform.position += transform.forward * Time.fixedDeltaTime * thrust; // for moving forward
            rigidbody.AddForce(transform.forward * Time.fixedDeltaTime * thrust, ForceMode.Acceleration); //*erase comment* added time.fixeddeltatime  

           
            UpDown = KeyValue(keyboard.Up, keyboard.Down, UpDown, yUpDown, 1.5f, 0.1f);
            


            



            UpDownTurn = KeyValue(keyboard.Up, keyboard.Down, UpDownTurn, yUpDownTrun, 1.5f, 0.1f);
            LeftRightTurn = KeyValue(keyboard.Left, keyboard.Right, LeftRightTurn, yLeftRightTurn, 1.5f, 0.1f);
            //Pitch//
            Pitch += UpDownTurn; //* Time.fixedDeltaTime //same as yaw comment
            Pitch = Mathf.Clamp(Pitch, pitchMin, pitchMax);

            //Yaw//
            Yaw += LeftRightTurn; //* Time.fixedDeltaTime; doesnt have it normally so if it doesnt work take out time....

            //Rotation//
            //transform.rotation =
            //    Quaternion.Slerp(transform.rotation,
            //              Quaternion.Euler(Pitch, Yaw, 0.0f), Time.fixedDeltaTime * 1.5f);
            //rigidbody.AddTorque(transform.up * Pitch * 20, ForceMode.Force);
            //rigidbody.AddTorque(transform.right * Yaw * 20, ForceMode.Force);
            //rigidbody.AddTorque(Pitch, Yaw, 0.0f, ForceMode.Force);
            rigidbody.AddTorque(Vector3.up * Time.fixedDeltaTime * (Yaw / 10.0f), ForceMode.Force); //*erase comment* added time.fixeddeltatime //force *CONTINUOUS MOVEMENT IN A CIRCLE *
            rigidbody.AddTorque(Vector3.right * 100.0f * Pitch, ForceMode.Force); //horizontal works *NEED TO DO A IF BUTTON LET GO 
            Debug.Log("turning");
        }

        //////////////////////////////////ENGINE OFF/////////////////////////////
        else
        {

            //transform.position += transform.forward * Time.fixedDeltaTime * thrust;
            rigidbody.AddForce(transform.forward * Time.fixedDeltaTime * thrust, ForceMode.Acceleration); //*erase comment* added time.fixeddeltatime

            UpDown = KeyValue(keyboard.Up, keyboard.Down, UpDown, yUpDown, 0.5f, 0.1f);

            UpDownTurn = KeyValue(keyboard.Up, keyboard.Down, UpDownTurn, yUpDownTrun, 0.5f, 0.1f); //.5
            LeftRightTurn = KeyValue(keyboard.Left, keyboard.Right, LeftRightTurn, yLeftRightTurn, 0.5f, 0.1f);

            Vector3 myTorque = new Vector3(Pitch, Yaw, 0.0f);
            rigidbody.AddTorque(myTorque, ForceMode.Force);
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
