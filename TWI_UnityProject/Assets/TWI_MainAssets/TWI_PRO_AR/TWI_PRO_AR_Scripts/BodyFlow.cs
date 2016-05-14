using UnityEngine;
using System.Collections;

/// <summary>
/// Creates a constant "flow" throughout the body that recognizes the area of the body the ship is in and exerts a constant physical force to move the ship along the body with blood.
/// Paired with Currently named "SubControl.cs" script that is the movement script for the ship.
/// </summary>

public class BodyFlow : MonoBehaviour {

    public SubControl myShip;
    //public int sectionNumber;
    public float blood;
    public float pressureChange;
    public float oxygenChange;
    public Collider shipCol;
    //public BodyFlow[] adjacentSections;
    public float lightAngle;
    public float lightRange;
    public float lightIntensity;
    private ShipLights shipLights;
    public Color fogColor;
    public float fogDensity;
    private EnvironmentManager envManager;
    private Submarine_Resources subRes;
    public GameObject[] activeEnv;
<<<<<<< HEAD
   // private ActiveEnvironments envMan;
    public GameObject currentZone;
=======
    private ActiveEnvironments envMan;
    //public GameObject currentZone;
>>>>>>> master


    void Start()
    {

        shipLights = GameObject.FindObjectOfType<ShipLights>();
        envManager = GameObject.FindObjectOfType<EnvironmentManager>();
        subRes = GameObject.FindObjectOfType<Submarine_Resources>();
   //     envMan = GameObject.FindObjectOfType<ActiveEnvironments>();
        if (!shipCol)
        {
            shipCol = myShip.GetComponent<Collider>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            //myShip.setSectionInt(sectionNumber);
            subRes.setOxygenAdd(oxygenChange);
            myShip.setBloodForce(blood);
            subRes.setPressureAdd(pressureChange);
            myShip.setWorldForce(transform.forward);
            shipLights.ChangeExteriorLights(lightIntensity, lightRange, lightAngle);
            envManager.ChangeFog(fogDensity, fogColor);
            for (int i = 0; i < activeEnv.Length; i++)
            {
    //            envMan.addToActive(activeEnv[i], i);
            }
            //Debug.Log(myShip.getSectionInt());
        }
    }

   
}
