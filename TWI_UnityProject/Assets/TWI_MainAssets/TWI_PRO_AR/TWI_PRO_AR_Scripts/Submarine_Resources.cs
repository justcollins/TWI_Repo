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
		Cursor.visible = false;
        curTime += Time.deltaTime;
        if (curTime >= maxTime)
        {
            if (shipEnergy <= maxEnergy)
            {
                setEnergyLevel(1.75f);

                if (subCon.getEngineOn())
                {
                    setEnergyLevel(-2.0f);
                }
                if (monitor.getSpotState() == 1)
                {
                    setEnergyLevel(-1.0f);
                }
                if (monitor.getInstState() == 1)
                {
                    setEnergyLevel(-0.5f);
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
		if(shipEnergy < 0)
			shipEnergy = 0;

        
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
