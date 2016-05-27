/* PROGRAMMER	:	Kien Ngo
 * FILE			:	DistortSndOxyDown.cs
 * DATE			:	May 19, 2016
 * PURPOSE		:	This will distort the sound when the oxygen run below a certain percentage.
 * 
 * NOTE:
 * This file works around with Unity's Audio mixer
 * 
 * INSTRUCTION:
 * 1. Audio Mixer
 *      Window --> Audio Mixer or ALT + 8
 * 2. Go to Unity Website and follow the setup instruction for Audio Mixer Setup
 *      <a href= "https://unity3d.com/learn/tutorials/modules/beginner/5-pre-order-beta/audiomixer-and-audiomixer-groups"></a>
 * 3. With the save Audio Mixer file drag it into this script section for Master Mixer
 * 4. Change the percentage value for when you want this to take in affect.
 * 5. If you want to
 *      Change the value for the Max Snd(Maximum Sound) 20.0f is the highest value
 *      Change the value for the Min Snd(Minimum Sound) -80.0f is the lowest value
 * 6. Change the value  for Snd Sub( Sound value you want to alter) This value will increase and decresase the volume
 * 7. Value_per_click is a debugging value for testing this file only
 *      The Oxygen level will control the rest in the game.
 */

using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class DistortSndOxyDown : MonoBehaviour
{
    [Header("Audio Mixer")]
    public AudioMixer masterMixer;
    
    [Header("Tweak Value")]
    [Tooltip("Percentage Value when the saturation will start taking affect. \n"
            +"0.0 =   0 % \n"
            +"1.0 = 100 %")]
    [Range(0.0f, 1.0f)]
    public float percentage = 0.1f;

    [Tooltip("The maximum value for Unity Db volume is 20.0f")]
    public float maxSnd = 20.0f;

    [Tooltip("The minimum value for Unity Db volume is -80.0f")]
    public float minSnd = -20.0f;   //At -20.0f You don't really hear the sound anymore

    [Tooltip("The value you want to change the volume by.\n recommend 0.5f")]
    public float sndSub = 1.0f;

    //Debugging
    //[Tooltip("")]
    //[Range(-80.0f, 20.0f)]
    //private float bgm = 0.0f;

    //[Tooltip("")]
    //[Range(-80.0f, 20.0f)]
    //[Tooltip("")]
    public float sndTurbine = 0.0f;
    
    private Submarine_Resources sub_res; //Use to get the Submarine_Resource component info.

    /*
     * Temporary this value is use to change the oxygen level when you press A
     */ 
    [Tooltip("This value is the amount of oxygen will go down when you press P")]
    public float value_per_click = 5.0f;

    /*
     * These values are for viewing purpose only since we do not have the full project.
     * This is to show that values are changing without viewing it through the Unity Console.
     * It is only for monitoring other values without switching to it.
     * Will be remove later.
     */ 
    //[Tooltip("DO NOT TWEAK ANY VALUES DOWN HERE")]
    //[Header("Debugging Purposes")]
    public float oxygenValue;


    // Use this for initialization
    void Start()
    {
        //This guarantee the volome does not go beyond 20.0f as the highest value is 20.0f
        if (maxSnd > 20.0f)
            maxSnd = 20.0f;

        if (minSnd < -80.0f)
            minSnd = -80.0f;

        sub_res = GetComponent<Submarine_Resources>();
        oxygenValue = sub_res.getOxygenLevel();
    }//End of Start()

    // Update is called once per frame
    void Update()
    {
        //Take this condition out in the real implement.
        //It has no value as the oxygen level will handle this.
        if (Input.GetKeyDown(KeyCode.P))
            sub_res.setOxygenLevel(-value_per_click);
       
        oxygenValue = sub_res.getOxygenLevel();

        //This guarantee that the oxygen level will never go below 0
        if (oxygenValue <= 0.0f)
        {
            oxygenValue = 0.0f;
            sub_res.setOxygenLevel(0.0f);
        }

        //If the Oxygen goes below a certain percentage subtract
        if (oxygenValue <= (sub_res.maxOxygen * percentage))
        {
            //if oxygen goes below zero we want the distortion sound at its highest as possible
            if (oxygenValue <= 0.0f || sndTurbine > maxSnd)
				sndTurbine = maxSnd;
            else
				sndTurbine += sndSub;
        }
        //If the Oxygen is above a certain percentage add oxygen
        else if (oxygenValue > (sub_res.maxOxygen * percentage))
        {
            //If oxygen goes above the percentage level then lessen the distortion sound
            if (oxygenValue >= sub_res.maxOxygen || sndTurbine < minSnd)
				sndTurbine = minSnd;
            else
                sndTurbine -= sndSub;
        }

        SetTurbineLevel(sndTurbine);
        //If you need to alter the background sound then enable this.
        //SetMusicLevel(bgm);

    }//End of Update()

    public void SetTurbineLevel(float turbinglvl)
    {
        masterMixer.SetFloat("Turbine", turbinglvl);  
    }//end of SetTurbineLevel method

    public void SetMusicLevel(float musiclvl)
    {
       masterMixer.SetFloat("BGM", musiclvl);
    }//end of SetMusicLevel method

}//End of BlurOxyDown
