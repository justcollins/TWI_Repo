using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_CreatePath : MonoBehaviour {

    public float moveSpeed;
    public List<string> Barriers;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit[] hit;


        //hit = Physics.SphereCast(new Ray(transform.position, transform.forward), 10, Vector3.down, 50.0);
	}
}
