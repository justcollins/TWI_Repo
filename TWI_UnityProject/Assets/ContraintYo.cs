using UnityEngine;
using System.Collections;

//This constrains the particle system to the cockpit
//Woohoo~

public class ContraintYo : MonoBehaviour {

    public Transform player;

    void Start () {
        transform.position = player.position;
	}

    void Update()
    {
        transform.position = player.position; 
    }
}
