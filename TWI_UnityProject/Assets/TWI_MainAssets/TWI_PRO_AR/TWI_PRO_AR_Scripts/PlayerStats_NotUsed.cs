using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    private int sectionInt;
    public Rigidbody shipRB;
    public float bloodForce;

    private float maxForSpeed = 5;
    private float maxTurnSpeed = 20;
    private Vector3 accelForce;
    private Vector3 turnForce;
    private Vector3 tiltForce;

    private Vector3 worldForce;

	// Use this for initialization
	void Start () {
        sectionInt = 0;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void FixedUpdate()
    {
        if (sectionInt == 0)
        {
            worldForce = new Vector3(0.0f, 0.0f, 1.0f) * bloodForce;
            getInput(worldForce);
        }
        if (sectionInt == 1)
        {
            worldForce = new Vector3(1.0f, 0.0f, 0.0f) * bloodForce;
            getInput(worldForce);
        }
        
    }

    public int Section
    {
        get
        {
            return sectionInt;
        }
        set 
        {
           sectionInt = value; 
        }
    }

    void getInput(Vector3 force)
    {
        accelForce = new Vector3(Input.GetAxis("Vertical") * maxForSpeed, 0, 0);
        //Debug.Log(accelForce);
        turnForce = new Vector3(0, Input.GetAxis("Horizontal") * maxTurnSpeed * Time.deltaTime, 0);
        //Debug.Log(turnForce);

        if (accelForce == Vector3.zero)
            shipRB.velocity -= shipRB.velocity / 2;
        if (turnForce == Vector3.zero)
            shipRB.rotation = new Quaternion(0, 0, 0, 1);

        shipRB.AddForce(accelForce + worldForce);
        shipRB.AddTorque(turnForce);
    }
}
