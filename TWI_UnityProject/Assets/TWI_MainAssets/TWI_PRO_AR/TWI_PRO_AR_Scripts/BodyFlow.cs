using UnityEngine;
using System.Collections;

/// <summary>
/// Creates a constant "flow" throughout the body that recognizes the area of the body the ship is in and exerts a constant physical force to move the ship along the body with blood.
/// Paired with Currently named "SubControl.cs" script that is the movement script for the ship.
/// </summary>

public class BodyFlow : MonoBehaviour {

    public SubControl myShip;
    public int sectionNumber;
    public int blood;
    public int pressureChange;
    public Collider shipCol;
    public BodyFlow[] adjacentSections;
    public float forceX, forceY, forceZ;

    void Start()
    {
        if (!shipCol)
        {
            shipCol = myShip.GetComponent<Collider>();
        }
    }

    void Update()
    {
        OnTriggerStay(shipCol);
    }

    void OnTriggerStay(Collider shipCol)
    {
        myShip.setSectionInt(sectionNumber);
        myShip.setBloodForce(blood);
        myShip.setPressure(pressureChange);
        Debug.Log(myShip.getSectionInt());
    }

   
}
