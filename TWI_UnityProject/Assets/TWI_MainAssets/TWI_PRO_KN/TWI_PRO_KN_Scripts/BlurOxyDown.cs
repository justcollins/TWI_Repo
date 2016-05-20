/*
 * Current problem is the value for the sound is not currently subtracting correctly.
 * I need a variable for that.
 * Right now it simply subtract one due to the -- or ++
 * 
 */ 


using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityEngine.Audio;

public class BlurOxyDown : MonoBehaviour
{
    //Get rid of this later and make it private. Our reference only.
    [Header("Submarine Resources")]
    public Submarine_Resources sub_res;
    public AudioMixer masterMixer;

    [Header("Tweak Value")]
    [Tooltip("This is how much you would like to subtract from the Saturation Value")]
    public float subt;

    [Tooltip("Percentage Value when the saturation will start taking affect. \n"
            +"0.0 =   0 % \n"
            +"1.0 = 100 %")]
    [Range(0.0f, 1.0f)]
    public float percentage = 0.1f;

    public float maxSnd = 20.0f;
    public float minSnd = -80.0f;
    public float sndSub = 1.0f;


    [Tooltip("Minimum Saturation Value that the saturation will go down to.")]
    [Range(0.0f, 1.0f)]
    public float minSaturationValue = 0.0f;

    //Test purpose
    [Range(-80.0f, 20.0f)]
    public float bgm = 0.0f;

    [Range(-80.0f, 20.0f)]
    public float sndTurbine = 0.0f;



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
        if (Input.GetKeyDown(KeyCode.P))
            sub_res.setOxygenLevel(-value_per_click);

        oxygenValue = sub_res.getOxygenLevel();

        //If the Oxygen goes below a certain percentage subtract
        if (oxygenValue <= (sub_res.maxOxygen * percentage))
        {
            //if oxygen goes below zero we want the saturation to equal only the minimum saturation
            if (oxygenValue <= 0.0f || Camera.main.GetComponent<ColorCorrectionCurves>().saturation < minSaturationValue)
            {
                Camera.main.GetComponent<ColorCorrectionCurves>().saturation = minSaturationValue;
                //SetTurbineLevel(maxSnd);
            }
            else
            {
                Camera.main.GetComponent<ColorCorrectionCurves>().saturation -= subt;
                //if (sndSub <= maxSnd)
                //    SetTurbineLevel(sndSub++);
                //else
                //    SetTurbineLevel(maxSnd);
            }
        }
        //If the Oxygen is above a certain percentage add oxygen
        else if (oxygenValue > (sub_res.maxOxygen * percentage))
        {
            if (oxygenValue >= sub_res.maxOxygen)
            {
                Camera.main.GetComponent<ColorCorrectionCurves>().saturation = 1.0f;
               // SetTurbineLevel(minSnd);
            }
            else
            {
                Camera.main.GetComponent<ColorCorrectionCurves>().saturation += subt;
                //if (sndSub <= minSnd)

                //    SetTurbineLevel(sndSub--);
                //else
                //    SetTurbineLevel(minSnd);
            }
        }

        saturationValue = Camera.main.GetComponent<ColorCorrectionCurves>().saturation;

        SetTurbineLevel(sndTurbine);
        SetMusicLevel(bgm);

    }//End of Update()

    public void SetTurbineLevel(float turbinglvl)
    {
        //The value string you called in the mixer, value to set it to be
        masterMixer.SetFloat("Turbine", turbinglvl);
        
    }

    public void SetMusicLevel(float musiclvl)
    {
       masterMixer.SetFloat("BGM", musiclvl);
    }

}//End of BlurOxyDown
