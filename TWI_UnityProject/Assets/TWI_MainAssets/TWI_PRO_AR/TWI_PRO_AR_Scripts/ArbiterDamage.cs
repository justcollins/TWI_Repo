using UnityEngine;
using System.Collections;

public class ArbiterDamage : MonoBehaviour {


    public Submarine_Resources subRes;
    public float motherDamage;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            subRes.setCabinPressure(motherDamage);
        }
    }
}
