using UnityEngine;
using System.Collections;

/*
Jonathan Harrington

laser looks at laserguide

*/


public class LaserLookAt : MonoBehaviour {

    public Transform target;
    


	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {

        transform.LookAt(target);
        
	
	}
}
