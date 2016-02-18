using UnityEngine;
using System.Collections;

/**
 *   Antibody Stick Class
 *   17 Feb 2016
 *   Jose Pascua
 * 
 *   For the player; whenever an antibody
 *   collides with the player, it calls a function
 *   in the BoidFlocking Class to stick to the
 *   game object that has this script.
 */


public class AntibodyStick : MonoBehaviour {

	void OnCollisionEnter(Collision c) {
		if ( c.gameObject.GetComponent<BoidFlocking>() ) {
			c.gameObject.GetComponent<BoidFlocking>().StickToOther(this.gameObject);
		}
	}
}

/// <comment>
/// by Jose Pascua
/// Class that calls a function in BoidFlocking to stick to the game object
/// that has this script.
/// </comment>