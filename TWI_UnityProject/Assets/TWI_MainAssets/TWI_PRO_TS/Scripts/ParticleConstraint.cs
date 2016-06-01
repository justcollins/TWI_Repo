using UnityEngine;
using System.Collections;

public class ParticleConstraint : MonoBehaviour {

    public Transform player;

    void Start () {
        transform.position = player.position;
	}

    void Update()
    {
        transform.position = player.position; 
    }
}
