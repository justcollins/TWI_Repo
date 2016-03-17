using UnityEngine;
using System.Collections;

public class Submarine_Resources : MonoBehaviour {

    private float cabinPressure;
    private float oxygenLevel;
    private float shipEnergy;
    public float maxPressure;
    public float maxOxygen;
    public float maxEnergy;

    private float curTime;
    public float oxyTimer;
    public SubControl subCon;
    

    //FOR TESTING VARIABLES
    public float energyTimer;

    public int shipSpeed;

	// Use this for initialization
	void Start () {
        setEnergyLevel(maxEnergy);
        setOxygenLevel(maxOxygen);
        setCabinPressure(0.0f);
        curTime = 0.0f;
        oxyTimer = 1.5f;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("maxOxy " + maxOxygen);
        curTime += Time.deltaTime;
        if (curTime >= oxyTimer)
        {
            setOxygenLevel(-3.0f);
            setCabinPressure(4.0f);
            if (subCon.getEngineOn())
            {
                //setEnergyLevel(0.5f);
            }
            //setEnergyLevel(0.2f);
            //setCabinPressure(subCon.getPressure());
            //setSpeed(5);
           // Debug.Log(getCabinPressure());
            curTime = 0;
            
        }

        
	}

    public void setCabinPressure(float newPressure)
    {
        cabinPressure = cabinPressure + newPressure;
    }

    public float getCabinPressure()
    {
        return cabinPressure;
    }

    public void setOxygenLevel(float newOxy)
    {
        oxygenLevel += newOxy;
        Debug.Log("OxyLevel " + oxygenLevel);
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
