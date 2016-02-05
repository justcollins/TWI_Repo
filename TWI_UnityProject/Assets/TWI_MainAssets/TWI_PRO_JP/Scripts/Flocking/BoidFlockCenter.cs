using UnityEngine;
using System.Collections;

/**
 *   BoidFlockCenter
 *   3 Feb 2016
 *   Jose Pascua
 * 
 *   A script of the center of the flock that changes the
 *   target of the BoidController script, when enabled under
 *   the BoidController script.
 */

public class BoidFlockCenter : MonoBehaviour {

	internal BoidController controller;
	internal Transform nearestWaypoint;
	public string playerTag = "Player";

	void Update () {
	}

	void ChangeTarget(Transform _t) {
		if (controller.centralAgency) {
			controller.chasee = _t;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == playerTag) {
			ChangeTarget(other.transform);
		} else if ( other.GetComponent<BoidWaypoint>() ) {
			nearestWaypoint = other.GetComponent<BoidWaypoint>().next.transform;
			ChangeTarget(nearestWaypoint);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == playerTag) {
			ChangeTarget(nearestWaypoint);
		}
	}

}

/// <comment>
/// by Jose Pascua
/// Allows the center of the flock to change the target of Boid Controller.
/// </comment>