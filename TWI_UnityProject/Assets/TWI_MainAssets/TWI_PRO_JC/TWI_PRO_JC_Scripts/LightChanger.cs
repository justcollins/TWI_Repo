using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Light))]
public class LightChanger : MonoBehaviour {

    public Color startingColor;
    public Color endingColor;
    public float colorSpeed;
    [Range(0, 8)]
    public float startingLightIntensity = 1;
    [Range(0, 8)]
    public float endingLightIntensity = 1;
    public float intensitySpeed = 0;

    private new Light light;

	private void Start () {
        light = GetComponent<Light>();
	}
	
	private void Update () {
        LerpLights();
	}

    private void LerpLights() {
        if(colorSpeed != 0) {
            light.color = Color.Lerp(startingColor, endingColor, Mathf.PingPong(Time.time, colorSpeed) / colorSpeed);
        }
        if(intensitySpeed != 0) {
            light.intensity = Mathf.Lerp(startingLightIntensity, endingLightIntensity, Mathf.PingPong(Time.time, intensitySpeed) / intensitySpeed);
        }
        
    }
}
