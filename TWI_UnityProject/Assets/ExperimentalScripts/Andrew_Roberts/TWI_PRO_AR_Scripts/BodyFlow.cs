using UnityEngine;
using System.Collections;

public class BodyFlow : MonoBehaviour {

    public PlayerStats myShip;

	// Use this for initialization
    void Start(){
        
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(myShip.Section);
	}

    void OnTriggerEnter(Collider shipCol)
    {
        
    }


    void OnTriggerStay(Collider shipCol)
    {
        //Debug.Log("Inside");
        
    }

    void OnTriggerExit(Collider shipCol)
    {
        //Debug.Log("Exit");
        if (myShip.Section == 0)
        {
            myShip.Section = 1;
        }
        if (myShip.Section == 1)
        {
            myShip.Section = 0;
        }
    }
}
