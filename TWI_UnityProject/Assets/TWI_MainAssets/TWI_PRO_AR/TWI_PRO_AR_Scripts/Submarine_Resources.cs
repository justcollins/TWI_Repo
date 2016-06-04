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

	[Range(-5.0f, 0.0f)] public float engineDraw;
	[Range(-5.0f, 0.0f)] public float spotDraw;
	[Range(-5.0f, 0.0f)] public float instDraw;
	[Range(0.0f, 5.0f)] public float passiveEnergyGain;

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
                setEnergyLevel(passiveEnergyGain);

                if (subCon.getEngineOn())
                {
                    setEnergyLevel(engineDraw);
                }
                if (monitor.getSpotState() == 1)
                {
                    setEnergyLevel(spotDraw);
                }
                if (monitor.getInstState() == 1)
                {
                    setEnergyLevel(instDraw);
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
