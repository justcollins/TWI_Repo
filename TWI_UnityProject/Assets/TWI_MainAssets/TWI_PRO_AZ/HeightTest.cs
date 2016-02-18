using UnityEngine;
using System.Collections;

public class HeightTest : MonoBehaviour {


	public GameObject Virus;



	// Use this for initialization
	void Start () {



	}
	
	// Update is called once per frame
	void Update () {
		Vector3 RadarOrigin = transform.position;
		float myPosition = transform.position.y;
		float virusPosition = Virus.transform.position.y;

		int myPosInt = (int) Mathf.Round (myPosition);
		int virusPosInt = (int) Mathf.Round (virusPosition);
		//Prints the position to the Console.
		if (myPosInt < virusPosInt) {

			Debug.Log (RadarOrigin);
		}


	
	}

}

