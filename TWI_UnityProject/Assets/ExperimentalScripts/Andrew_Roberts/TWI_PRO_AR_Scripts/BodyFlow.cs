using UnityEngine;
using System.Collections;


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
