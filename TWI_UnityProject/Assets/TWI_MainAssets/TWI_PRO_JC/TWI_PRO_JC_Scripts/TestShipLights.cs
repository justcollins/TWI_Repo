using UnityEngine;
using System.Collections;

public class TestShipLights : MonoBehaviour
{

    public float angle1;
    public float angle2;
    public float range1;
    public float range2;
    public float intensity1;
    public float intensity2;

    private ShipLights shipLights;

    void Start() {
        shipLights = GameObject.FindObjectOfType<ShipLights>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            shipLights.ChangeExteriorLights(intensity1, range1, angle1);
        } else if (Input.GetKeyDown(KeyCode.E)) {
            shipLights.ChangeExteriorLights(intensity2, range2 , angle2);
        }
    }
}