using UnityEngine;
using System.Collections;

/// <summary>
/// Creates a constant "flow" throughout the body that recognizes the area of the body the ship is in and exerts a constant physical force to move the ship along the body with blood.
/// Paired with Currently named "CompleteMovement_01" script that is the movement script for the ship. Will have to aptly rename these scripts later for streamlining, but hey, they work.
/// </summary>

public class BodyFlow : MonoBehaviour {

    public CompleteMovement_01 myShip;
    public int sectionNumber;
    public int blood;
    public int pressureChange;
    public int timeMax;
    private float curTime;
    

	// Use this for initialization
    void Start(){
        
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(myShip.Section);
	}

    void OnTriggerEnter(Collider shipCol)
    {
        Debug.Log(gameObject.name + " has hit " + shipCol.name);
        

    }


    void OnTriggerStay(Collider shipCol)
    {
        //Debug.Log("Inside");
        myShip.setSectionInt(sectionNumber);
        myShip.setBloodForce(blood);

        curTime += Time.deltaTime;
        if (curTime >= timeMax)
        {
            myShip.setCabinPressure(pressureChange);
            curTime = 0;
        }
    }

    void OnTriggerExit(Collider shipCol)
    {
        /*Debug.Log("Exit");
        Debug.Log(myShip.getSectionInt());
        if (myShip.getSectionInt() == 0)
        {
            myShip.setSectionInt(1);
            Debug.Log(myShip.getSectionInt());
        }
        if (myShip.getSectionInt() == 0)
            myShip.setSectionInt(0);*/
    }
}
