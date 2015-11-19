using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipMovement : MonoBehaviour {

    public Rigidbody shipRB;
    public Transform shipT;
    private float maxForSpeed = 40;
    private float maxTurnSpeed = 20;
    private Vector3 accelForce;
    private Vector3 turnForce;
    private Vector3 tiltForce;



	// Use this for initialization
	void Start () {
        if (!shipRB)
            shipRB = GetComponent <Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        /*accelForce = new Vector3(Input.GetAxis("Vertical") * maxForSpeed, 0, 0);
        Debug.Log(accelForce);
        turnForce = new Vector3(0, Input.GetAxis("Horizontal") * maxTurnSpeed, 0);
        Debug.Log(turnForce);
        shipRB.AddRelativeForce(accelForce);
        shipRB.AddRelativeTorque(turnForce);
         */

        getInput();

    }


    void getInput()
    {
        accelForce = new Vector3(Input.GetAxis("Vertical") * maxForSpeed, 0, 0);
        //Debug.Log(accelForce);
        turnForce = new Vector3(0, Input.GetAxis("Horizontal") * maxTurnSpeed *Time.deltaTime, 0);
        //Debug.Log(turnForce);

        if (accelForce == Vector3.zero)
            shipRB.velocity -= shipRB.velocity / 2;
        if (turnForce == Vector3.zero)
            shipRB.rotation = new Quaternion(0, 0, 0, 1);

        shipRB.AddForce(accelForce);
        shipRB.AddTorque(turnForce);
    }
}
