using UnityEngine;
using System.Collections;

public class ActiveEnvironments : MonoBehaviour {

    private GameObject[] actives;
    public GameObject startingZone;
	// Use this for initialization
	void Start () {
        actives = new GameObject[4];
        startingZone.SetActive(true);
        actives[1] = startingZone;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
