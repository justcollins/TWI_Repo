using UnityEngine;
using System.Collections.Generic;

public class ShipLights : MonoBehaviour {

    private SubControl sub;
    private List<Light> lights;
    private Light[] Lights;

    private void Start() {
        sub = GameObject.FindObjectOfType<SubControl>();
        Lights = sub.exteriorLights.GetComponentsInChildren<Light>(true);
    }

    public void ChangeExteriorLights(float newIntensity, float newRange, float newAngle) {
        foreach( Light light in Lights) {
            light.intensity = newIntensity;
            light.range = newRange;
            light.spotAngle = newAngle;
        }
    }

}
