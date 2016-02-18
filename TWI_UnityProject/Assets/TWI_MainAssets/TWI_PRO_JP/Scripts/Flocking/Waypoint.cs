using UnityEngine;
using System.Collections;

/**
 *   Waypoint Class
 *   Jose Pascua
 * 
 *   A class that contains a reference to the
 * 	 next waypoint and a radius.
 */

public class Waypoint : MonoBehaviour {

	public Waypoint next;
	public float radius = 20f;
	private SphereCollider col;
	public bool visible = true;

	void Awake() {
		col = GetComponent<SphereCollider>();
		col.radius = radius;
	}

	void OnDrawGizmos () {
		if (visible) {
			Gizmos.color = new Color (1f, 0f, 1f, 0.2f);
			Gizmos.DrawSphere (transform.position, radius);
		}
	}
}

/// <comment>
/// by Jose Pascua
/// Class to attach to waypoints; contains a reference to the next waypoint.
/// </comment>