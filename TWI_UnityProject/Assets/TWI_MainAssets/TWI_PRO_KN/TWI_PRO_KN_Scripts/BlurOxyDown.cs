using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class BlurOxyDown : MonoBehaviour
{
    //Get rid of this later and make it private. Our reference only.
    [Header("Submarine Resources")]
    public Submarine_Resources sub_res;

    [Header("Tweak Value")]
    [Tooltip("This is how much you would like to subtract from the Saturation Value")]
    public float subt;

    [Tooltip("Percentage Value when the saturation will start taking affect. \n"
            +"0.0 =   0 % \n"
            +"1.0 = 100 %")]
    [Range(0.0f, 1.0f)]
    public float percentage = 0.1f;


    [Tooltip("Minimum Saturation Value that the saturation will go down to.")]
    [Range(0.0f, 1.0f)]
    public float minSaturationValue = 0.0f;


    /*
     * Temporary this value is use to change the oxygen level when you press A
     */ 
    [Tooltip("This value is the amount of oxygen will go down when you press A")]
    public float value_per_click = 5.0f;

    /*
     * These values are for viewing purpose only since we do not have the full project.
     * This is to show that values are changing without viewing it through the Unity Console.
     * It is only for monitoring other values without switching to it.
     * Will be remove later.
     */ 
    [Tooltip("DO NOT TWEAK ANY VALUES DOWN HERE")]
    [Header("Debugging Purposes")]
    public float oxygenValue;
    public float saturationValue;

    // Use this for initialization
    void Start()
    {
        sub_res = GetComponent<Submarine_Resources>();
        oxygenValue = sub_res.getOxygenLevel();
        saturationValue = Camera.main.GetComponent<ColorCorrectionCurves>().saturation;
    }//End of Start()

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            sub_res.setOxygenLevel(-value_per_click);

        oxygenValue = sub_res.getOxygenLevel();

        //If the Oxygen goes below a certain percentage subtract
        if (oxygenValue <= (sub_res.maxOxygen * percentage))
        {
            //if oxygen goes below zero we want the saturation to equal only the minimum saturation
            if (oxygenValue <= 0.0f || Camera.main.GetComponent<ColorCorrectionCurves>().saturation < minSaturationValue)
                Camera.main.GetComponent<ColorCorrectionCurves>().saturation = minSaturationValue;
            else
                Camera.main.GetComponent<ColorCorrectionCurves>().saturation -= subt;
        }
        //If the Oxygen is above a certain percentage add oxygen
        else if (oxygenValue > (sub_res.maxOxygen * percentage))
        {
            if (oxygenValue >= sub_res.maxOxygen)
                Camera.main.GetComponent<ColorCorrectionCurves>().saturation = 1.0f;
            else
                Camera.main.GetComponent<ColorCorrectionCurves>().saturation += subt;
        }

        saturationValue = Camera.main.GetComponent<ColorCorrectionCurves>().saturation;

    }//End of Update()

}//End of BlurOxyDown
