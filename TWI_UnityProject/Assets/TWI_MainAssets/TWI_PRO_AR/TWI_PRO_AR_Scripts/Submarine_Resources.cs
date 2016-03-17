using UnityEngine;
using System.Collections;

public class Submarine_Resources : MonoBehaviour {

    public int cabinPressure;
    public float oxygenLevel;
    public float shipEnergy;

    private float curTime;
    public float oxyTimer;
    public SubControl subCon;
    

    //FOR TESTING VARIABLES
    public float energyTimer;

    public int shipSpeed;

	// Use this for initialization
	void Start () {
        shipEnergy = 100.0f;
        oxygenLevel = 15.0f;
        cabinPressure = 70;
        curTime = 0.0f;
        oxyTimer = 1.5f;
	}
	
	// Update is called once per frame
	void Update () {
        curTime += Time.deltaTime;
        if (curTime >= oxyTimer)
        {
            setOxygenLevel(3.0f);
            if (subCon.getEngineOn())
            {
                //setEnergyLevel(0.5f);
            }
            //setEnergyLevel(0.2f);
            setCabinPressure(subCon.getPressure());
            //setSpeed(5);
           // Debug.Log(getCabinPressure());
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

    public void setOxygenLevel(float newOxy)
    {
        oxygenLevel = oxygenLevel - newOxy;
    }

    public float getOxygenLevel()
    {
        return oxygenLevel;
    }

    public void setEnergyLevel(float newEnergy)
    {
        if (shipEnergy > 0)
            shipEnergy = shipEnergy - newEnergy;
        else
            shipEnergy = 0;
    }

    public float getShipEnergy()
    {
        return shipEnergy;
    }

    public void setSpeed(int newSpeed)
    {
        shipSpeed += newSpeed;
    }

    public int getSpeed()
    {
        return shipSpeed;
    }

}
