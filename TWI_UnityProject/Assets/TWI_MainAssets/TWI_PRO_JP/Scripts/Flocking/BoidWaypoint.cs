using UnityEngine;
using System.Collections;

public class BoidWaypoint : MonoBehaviour {

	public BoidWaypoint next;
	public float radius = 20f;
	private SphereCollider col;

	void Awake() {
		col = GetComponent<SphereCollider>();
		col.radius = radius;
	}

	void OnDrawGizmos () {
		Gizmos.color = new Color ( 1f, 0f, 1f, 0.2f);
		Gizmos.DrawSphere ( transform.position, radius );
	}
}
