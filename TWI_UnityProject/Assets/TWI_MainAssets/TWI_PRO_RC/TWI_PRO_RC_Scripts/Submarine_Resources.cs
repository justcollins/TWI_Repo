using UnityEngine;
using System.Collections;

public class Submarine_Resources : MonoBehaviour {

    private float oxygenAdd;
    private float pressureAdd;
    private float cabinPressure;
    private float oxygenLevel;
    private float shipEnergy;
    public float maxPressure;
    public float maxOxygen;
    public float maxEnergy;

    public float energyRegain = 0;
    public float energyEngineOn = 0;
    public float energyEngineBoost = 0;
    public float energyHeadLights = 0;
    public float energyCockpitLights = 0;
    public float energyRadarFire = 0;
    public float energyLaserFire = 0;
    public float energyShieldFire = 0;


    private float curTime;
    private float maxTime = 1.0f;
    public SubControl subCon;
    public EngineMonitor monitor;

	// Use this for initialization
	void Start () {
        setEnergyLevel(maxEnergy);
        setOxygenLevel(maxOxygen);
        setCabinPressure(0.0f);
        setOxygenAdd(4.8f);
        curTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
       //Debug.Log(shipEnergy);
       //Debug.Log(curTime);
        Debug.Log("Oxygen Level: " + oxygenLevel);
        curTime += Time.deltaTime;
        if (curTime >= maxTime)
        {
            if (shipEnergy <= maxEnergy)
            {
                setEnergyLevel(energyRegain);

                if (subCon.getEngineOn())
                {
                    setEnergyLevel(energyEngineOn);
                }
                if (monitor.getSpotState() == 1)
                {
                    setEnergyLevel(energyHeadLights);
                }
                if (monitor.getInstState() == 1)
                {
                    setEnergyLevel(energyCockpitLights);
                }
            }
            else
                shipEnergy = maxEnergy; 
            if (oxygenLevel <= maxOxygen)
                setOxygenLevel(-.08f + oxygenAdd);
            else
                oxygenLevel = maxOxygen;

            if (cabinPressure > 0)
            {
                setCabinPressure(-1.0f + pressureAdd);
            }
            else
                cabinPressure = 0;
            curTime = 0;
            
        }

        
	}

    public void setCabinPressure(float newPressure)
    {
        cabinPressure += newPressure;
    }

    public void setPressureAdd(float newPressure)
    {
        pressureAdd = newPressure;
    }

    public float getCabinPressure()
    {
        return cabinPressure;
    }

    public void setOxygenAdd(float newOxy)
    {
        oxygenAdd = newOxy;
    }

    public void setOxygenLevel(float newOxy)
    {
        oxygenLevel += newOxy;
    }

    public float getOxygenLevel()
    {
        return oxygenLevel;
    }

    public void setEnergyLevel(float newEnergy)
    {
        shipEnergy += newEnergy;
    }

    public float getShipEnergy()
    {
        return shipEnergy;
    }

}
