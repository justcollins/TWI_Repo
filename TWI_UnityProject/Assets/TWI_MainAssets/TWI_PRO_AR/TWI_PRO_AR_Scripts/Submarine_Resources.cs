using UnityEngine;
using System.Collections;

public class Submarine_Resources : MonoBehaviour {

    public int cabinPressure;
    public int oxygenLevel;
    public int shipEnergy;

    private float curTime;
    public float oxyTimer;

    //FOR TESTING VARIABLES
    public float energyTimer;

	// Use this for initialization
	void Start () {
        shipEnergy = 100;
        oxygenLevel = 100;
        cabinPressure = 0;
        curTime = 0.0f;
        oxyTimer = 1.5f;
	}
	
	// Update is called once per frame
	void Update () {
        curTime += Time.deltaTime;
        if (curTime >= oxyTimer)
        {
            setOxygenLevel(1);
            setEnergyLevel(1);
            curTime = 0;
            
        }

        
	}

    public void setCabinPressure(int newPressure)
    {
        cabinPressure = cabinPressure + newPressure;
    }

    public int getCabinPressure()
    {
        return cabinPressure;
    }

    public void setOxygenLevel(int newOxy)
    {
        oxygenLevel = oxygenLevel - newOxy;
    }

    public int getOxygenLevel()
    {
        return oxygenLevel;
    }

    public void setEnergyLevel(int newEnergy)
    {
        shipEnergy = shipEnergy - newEnergy;
    }

    public int getShipEnergy()
    {
        return shipEnergy;
    }
}
