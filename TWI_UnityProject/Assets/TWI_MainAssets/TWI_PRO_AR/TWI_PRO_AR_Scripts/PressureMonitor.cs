using UnityEngine;
using System.Collections;

public class PressureMonitor : MonoBehaviour {

    public GameObject pressureNeedle;
    public GameObject oxygenNeedle;
    public Submarine_Resources Sub;

    private float subPressure;
    private float subOxygen;

	// Use this for initialization
	void Start () {
        subPressure = Sub.getCabinPressure();
        subOxygen = Sub.getOxygenLevel();
	}
	
	// Update is called once per frame
	void Update () {
        //MAKE THE VALUE IN THE FORMULA THE MAX OXYGEN AND PRESSURE
        subPressure = Sub.getCabinPressure();
        subOxygen = Sub.getOxygenLevel();
        float pressRotY = Mathf.Abs(270.0f * (subPressure / Sub.maxPressure));
        float oxyRotY = Mathf.Abs((270.0f * (subOxygen/Sub.maxOxygen)));
        //float pressRotY = (90.0f * (subPressure / Sub.maxPressure));
        //float oxyRotY = (90.0f * (subOxygen / Sub.maxOxygen));
        Debug.Log("OxyRotY " + oxyRotY);
        Vector3 pressureRot = new Vector3(0.0f, pressRotY, 0.0f);
        Vector3 oxygenRot = new Vector3(0.0f, oxyRotY, 0.0f);
        pressureNeedle.transform.localEulerAngles = pressureRot;
        oxygenNeedle.transform.localEulerAngles = oxygenRot;
        
	}
}
