using UnityEngine;
using System.Collections;

/**
 *   Enemy Movement Class
 *   3 Feb 2016
 *   Jose Pascua
 * 
 *   Auxiliary script for testing purposes
 */

[System.Serializable]
public struct PilotInputs {

	public KeyCode forwards;
	public KeyCode backwards;
	public KeyCode left;
	public KeyCode right;
	public KeyCode up;
	public KeyCode down;

}

public class TestPilot : MonoBehaviour {

	public float speed = 12f;
	public float rSpeed = 12f;
	public PilotInputs pilotInputs;
	public bool defaultInputs;

	void Start () {
		if (defaultInputs) {
			pilotInputs.forwards = KeyCode.W;
			pilotInputs.backwards = KeyCode.S;
			pilotInputs.left = KeyCode.A;
			pilotInputs.right = KeyCode.D;
			pilotInputs.up = KeyCode.Q;
			pilotInputs.down = KeyCode.E;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Movement ();
	}

	void Movement() {
		if (Input.GetKey (pilotInputs.forwards)) {
			transform.position += transform.forward.normalized * speed * Time.deltaTime;
		}
		
		if (Input.GetKey (pilotInputs.backwards)) {
			transform.position += transform.forward.normalized * -speed * Time.deltaTime;
		}
		
		if (Input.GetKey (pilotInputs.left)) {
			transform.Rotate( new Vector3 ( 0, -rSpeed * Time.deltaTime, 0 ) );
		}
		
		if (Input.GetKey (pilotInputs.right)) {
			transform.Rotate( new Vector3 ( 0, rSpeed * Time.deltaTime, 0 ) );
		}
		
		if (Input.GetKey (pilotInputs.up)) {
			transform.position += new Vector3 ( 0, speed * Time.deltaTime, 0 );
		}
		
		if (Input.GetKey (pilotInputs.down)) {
			transform.position += new Vector3 ( 0, -speed * Time.deltaTime, 0 );
		}
	}
}

/// <comment>
/// by Jose Pascua
/// Auxiliary script for testing purposes
/// </comment>