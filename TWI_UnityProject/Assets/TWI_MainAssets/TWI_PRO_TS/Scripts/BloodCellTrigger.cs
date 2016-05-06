using UnityEngine;
using System.Collections;

//If the cockpit enters a trigger the local velocity of the 
//particle in the mesh emitter should increase/decrease
//in the "Z" 

public class BloodCellTrigger : MonoBehaviour
{

    public Vector3 localVelocity;
    public bool enabled;

    void OnTriggerEnter(Collider other)
    {
    
        if (other.tag == "BloodParticles")//I'm assuming that the cockpit is the player
        {
            ParticleSystem.VelocityOverLifetimeModule.enabled.z = localVelocity;
        }
    }
}