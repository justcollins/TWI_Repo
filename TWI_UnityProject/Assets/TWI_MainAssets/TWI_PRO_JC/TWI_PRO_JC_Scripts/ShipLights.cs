using UnityEngine;
using System.Collections.Generic;

public class ShipLights : MonoBehaviour {

    private SubControl sub;
    private List<Light> lights;
    private Light[] Lights;

    private void Start() {
        sub = GameObject.FindObjectOfType<SubControl>();
        Lights = sub.exteriorLights.GetComponentsInChildren<Light>(true);
        foreach(Light light in Lights) {
            Debug.Log("light " + light);
        }
    }

    public void ChangeExteriorLights(float newIntensity, float newRange, Color newColor) {
        foreach( Light light in Lights) {
            light.intensity = newIntensity;
            light.range = newRange;
            light.color = newColor;
        }
    }
}
