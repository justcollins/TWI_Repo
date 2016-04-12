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

	void OnTriggerStay(Collider other) {
		if (other.tag == playerTag) {
			ChangeTarget(other.transform);
		} else if ( other.GetComponent<Waypoint>() ) {
			if (other.transform == nearestWaypoint) {

				if ( controller.goinForth ) { //goin forward
					if ( other.GetComponent<Waypoint>().next ) { // if there is a next, then go there
						//Debug.Log ( "there's a next; goin to " + other.gameObject.name );
						nearestWaypoint = other.GetComponent<Waypoint>().next.transform;
						ChangeTarget(nearestWaypoint);
					} else { // otherwise turn around
						//Debug.Log ( "turning around" );
						controller.goinForth = false;
						nearestWaypoint = other.GetComponent<Waypoint>().prev.transform;
						ChangeTarget(nearestWaypoint);
					}
				} else { // goin backwards
					if ( other.GetComponent<Waypoint>().prev ) { // if there is a previous, then go there
						//Debug.Log ( "there's a next; goin to " + other.gameObject.name );
						nearestWaypoint = other.GetComponent<Waypoint>().prev.transform;
						ChangeTarget(nearestWaypoint);
					} else { // otherwise turn around
						//Debug.Log ( "turning around" );
						controller.goinForth = true;
						nearestWaypoint = other.GetComponent<Waypoint>().next.transform;
						ChangeTarget(nearestWaypoint);
					}
				}


			}
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