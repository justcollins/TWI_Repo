using UnityEngine;
using System.Collections;

/// <summary>
/// Creates a constant "flow" throughout the body that recognizes the area of the body the ship is in and exerts a constant physical force to move the ship along the body with blood.
/// Paired with Currently named "SubControl.cs" script that is the movement script for the ship.
/// </summary>

public class BodyFlow : MonoBehaviour {

    public SubControl myShip;
    //public int sectionNumber;
    public int blood;
    public int pressureChange;
    public Collider shipCol;
    //public BodyFlow[] adjacentSections;
    public float forceX, forceY, forceZ;
    public float lightAngle;
    public float lightRange;
    public float lightIntensity;
    private ShipLights shipLights;
    public Color fogColor;
    public float fogDensity;
    private EnvironmentManager envManager;


    void Start()
    {

        shipLights = GameObject.FindObjectOfType<ShipLights>();
        envManager = GameObject.FindObjectOfType<EnvironmentManager>();
        if (!shipCol)
        {
            shipCol = myShip.GetComponent<Collider>();
        }
    }

    void Update()
    {
        OnTriggerEnter(shipCol);
    }

    void OnTriggerEnter(Collider other)
    {
        //myShip.setSectionInt(sectionNumber);
        myShip.setBloodForce(blood);
        myShip.setPressure(pressureChange);
        myShip.setWorldForce(forceX, forceY, forceZ);
        shipLights.ChangeExteriorLights(lightIntensity, lightRange, lightAngle);
        envManager.ChangeFog(fogDensity, fogColor);
        //Debug.Log(myShip.getSectionInt());
    }

   
}
