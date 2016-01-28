using UnityEngine;
using System.Collections;

/// <summary>
/// This is the Ship's base class, not named to the best conventions. Allows for the movement of the ship based on Kien's initial design.
/// Also works in conjunction with "BodyFlow.cs" to make use of the worlds blood flow for forced movement of the ship through the body.
/// Also contains the variables for which the game will recognize the ships stats: Cabin Pressure and Oxygen. Will be used with energy as well.
/// </summary>
 
public class CompleteMovement_01 : MonoBehaviour
{

    private int sectionInt;
    public Rigidbody shipRB;
    private float bloodForce;
    private Vector3 worldForce;

    public int cabinPressure;
    private int oxygenLevel;

    private float curTime;
    public float oxyTimer;

    public KeyCode UP;
    public KeyCode DOWN;
    public KeyCode LEFT;
    public KeyCode RIGHT;
    public KeyCode ENGINE_ON;		//For Turn on and off the engine

    float UpDownVelocity = 0.0f;
    public float maxThrustValue = 10.0f;		//Max value to reach for thruster
    public float minThrustValue = 0.0f;
    public float thrust = 0.0f;			//For Debugging purpose otherwise should be private

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


    void Start()
    {
        sectionInt = 0;
        cabinPressure = 0;
        curTime = 0;
        oxygenLevel = 100;
    }
    void FixedUpdate()
    {
        //Blood Flow Before checking if Engine is on, since it is Irrelevant
        if (sectionInt == 0)
            worldForce = new Vector3(0.0f, 0.0f, 1.0f);
        if (sectionInt == 1)
            worldForce = new Vector3(-1.0f, 0.0f, 0.0f);
        if (sectionInt == 2)
            worldForce = new Vector3(-1.0f, 0.0f, 0.5f);

        shipRB.AddForce(worldForce*bloodForce);


        if (isEngineOn)
        {
            //Spacebar = Thrust
            //NOTE:
            //Still need to implement the a certain amount of time
            //If power runs out then the thrust must power off.





            if (Input.GetButton("Jump"))
            {
                if (thrust <= maxThrustValue)
                    thrust += .1f;
                else
                    thrust = maxThrustValue;
            }

            //Left Control = Drag
            if (Input.GetButton("Fire1"))
            {
                if (thrust > minThrustValue)
                    thrust -= .1f;
                else
                    thrust = minThrustValue;
            }

            transform.position += transform.forward * Time.fixedDeltaTime * thrust;


            UpDown = KeyValue(UP, DOWN, UpDown, yUpDown, 1.5f, 0.1f);

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
            if (thrust > minThrustValue)
                thrust -= .001f;
            else
                thrust = minThrustValue;

            transform.position += transform.forward * Time.fixedDeltaTime * thrust;

            UpDown = KeyValue(UP, DOWN, UpDown, yUpDown, .5f, 0.1f);

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

    void Update()
    {
        if (Input.GetKeyDown(ENGINE_ON))
            isEngineOn = !isEngineOn;


        curTime += Time.deltaTime;
        if (curTime >= oxyTimer)
        {
            setOxygenLevel(1);
            curTime = 0;
        }
        //Debug.Log(sectionInt);
        Debug.Log(oxygenLevel);
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

    public void setCabinPressure(int newPressure)
    {
        cabinPressure = cabinPressure + newPressure;
    }

    public int getCabinPressure()
    {
        return cabinPressure;
    }

    public void setOxygenLevel(int newOxy)
    {
        oxygenLevel = oxygenLevel - newOxy;
    }

    public int getOxygenLevel()
    {
        return oxygenLevel;
    }
    
}

