using UnityEngine;
using System.Collections;

public class TestShipLights : MonoBehaviour
{

    public Color color1;
    public Color color2;
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
            shipLights.ChangeExteriorLights(intensity1, range1, color1);
            Debug.Log("Change 1");
        } else if (Input.GetKeyDown(KeyCode.W)) {
            shipLights.ChangeExteriorLights(intensity2, range2 ,color2);
            Debug.Log("Change 2");
        }
    }
}