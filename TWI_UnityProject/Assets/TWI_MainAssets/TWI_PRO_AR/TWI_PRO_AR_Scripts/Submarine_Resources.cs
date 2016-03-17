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
    private float maxTime = 1.0f;
    public SubControl subCon;
    public EngineMonitor monitor;

	// Use this for initialization
	void Start () {
        setEnergyLevel(maxEnergy);
        setOxygenLevel(maxOxygen);
        setCabinPressure(0.0f);
        curTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(shipEnergy);
        Debug.Log(curTime);
        curTime += Time.deltaTime;
        if (curTime >= maxTime)
        {
            setEnergyLevel(1.75f);
            if (subCon.getEngineOn())
            {
                setEnergyLevel(-1.0f);
            }
            if (monitor.getSpotState() == 1)
            {
                setEnergyLevel(-1.0f);
            }
            if (monitor.getInstState() == 1)
            {
                setEnergyLevel(-0.5f);
            }
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
