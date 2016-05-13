using UnityEngine;
using System.Collections;


public class BloodCellTrigger : MonoBehaviour
{
    public Transform WindForce;

    void OnTriggerEnter(Collider updateRotation)
    {
    
        if (updateRotation.tag == "WindSwoosh")
        {
            WindForce.eulerAngles = updateRotation.transform.forward;
            Debug.Log("Changing Rotation");
        }
    }
}