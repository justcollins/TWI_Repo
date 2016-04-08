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
	public GameObject shield;
	public LineRenderer laserline;
	private GameObject myShield;

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
		Shield ();
		Laser ();
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

	void Shield() {
		if (Input.GetMouseButtonDown (1)) {
			if (myShield == null) {
				myShield = Instantiate( shield, transform.position, transform.rotation ) as GameObject;
			}
		}
		if (myShield != null) {
			myShield.transform.position = transform.position;
		}
		if (Input.GetMouseButtonUp (1)) {
			GameObject.Destroy(myShield);
		}
	}

	void Laser() {
		if (Input.GetMouseButton (0)) {
			RaycastHit hit;
			Physics.Raycast(transform.localPosition, transform.forward, out hit);
			Debug.DrawLine (this.transform.localPosition, transform.forward);
			if (hit.transform.tag == "Enemy") {
				Debug.Log ("gotcha!!");
				EnemyHealth e = hit.transform.GetComponent<EnemyHealth>();
				switch((int)e.type) {
					case 1: //tagger
					Debug.Log ("Hit a guy!");
					GameObject.Destroy (e.gameObject);
					break;

					case 2:
					Debug.Log ("Hit a guy!");
					e.AddHealth( -1 );
					break;
				}
			}
		}
	}
}

/// <comment>
/// by Jose Pascua
/// Auxiliary script for testing purposes
/// </comment>