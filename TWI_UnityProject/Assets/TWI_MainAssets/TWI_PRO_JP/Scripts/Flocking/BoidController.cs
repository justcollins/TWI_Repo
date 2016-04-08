using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 *   BoidController
 *   from the unifycommunity wikia
 *   converted to C# by Benoit Fouletier
 *   additional modifications by Jose Pascua
 */

public class BoidController : MonoBehaviour {

	[Header("Flocking Options")]
	public float minVelocity = 5f;
	public float maxVelocity = 20f;
	public float randomness = 1f;
	[Range(1,200)]public int flockSize = 10;
	public BoidFlocking prefab;
	public Transform chasee;

	[Header("Turning Delay")]
	public float lowerboundWait = 0.3f;
	public float upperboundWait = 0.5f;

	[Header("Additional Options")]
	public BoidFlockCenter centerCollider;
	public bool followCenter;
	public bool drawCenter;
	public float centerRadius = 3f;
	private float initialRadius;
	public bool centralAgency = false;
	public bool stickToPlayer;

	//[Header("Flock Vectors")]
	internal Vector3 flockCenter;
	internal Vector3 flockCenter2;
	internal Vector3 flockVelocity;

	List<BoidFlocking> boids = new List<BoidFlocking>();
	private Collider col;

	void Awake() {
		col = GetComponent<Collider> ();
		initialRadius = centerRadius;

		if (centerCollider) {
			centerCollider.controller = this;
			centerCollider.GetComponent<SphereCollider>().radius = centerRadius;
			centerCollider.nearestWaypoint = chasee;
		}
	}

	void Start() {


		for (int i = 0; i < flockSize; i++) {
			BoidFlocking boid = Instantiate (prefab, transform.position, transform.rotation) as BoidFlocking;
			boid.transform.parent = transform;
			boid.transform.localPosition = new Vector3 (
					Random.value * col.bounds.size.x,
					Random.value * col.bounds.size.y,
					Random.value * col.bounds.size.z
					) - col.bounds.extents;
			boid.controller = this;
			boids.Add (boid);
		}
	}

	void Update() {
		Vector3 theCenter = Vector3.zero;
		Vector3 theCenter2 = Vector3.zero;
		Vector3 theVelocity = Vector3.zero;

		foreach(BoidFlocking boid in boids) {
			theCenter += boid.transform.localPosition;
			theCenter2 += boid.transform.position;
			theVelocity += boid.GetComponent<Rigidbody>().velocity;
		}

		flockCenter = theCenter / (flockSize);
		flockCenter2 = theCenter2 / (flockSize);
		flockVelocity = theVelocity / (flockSize);

		if (followCenter) {
			if (centerCollider) {
				centerCollider.transform.position = flockCenter2;
			}
		}

		if (flockSize <= 1) {
			GameObject.Destroy(this.gameObject);
		}
	}

	void OnDrawGizmos() {
		if (drawCenter) {
			Gizmos.color = new Color( 0f, 1f, 1f, 0.2f );
			Gizmos.DrawSphere (flockCenter2, centerRadius);
		}
	}

	public void RemoveBoid( BoidFlocking _b ) {
		boids.Remove (_b);
		GameObject.Destroy (_b.gameObject);
		flockSize = boids.Count;
	}

	public void AdjustRadius(float _c) {
		centerRadius = _c;
		centerCollider.GetComponent<SphereCollider>().radius = centerRadius;
	}

	public void ResetRadius() {
		centerRadius = initialRadius;
		centerCollider.GetComponent<SphereCollider>().radius = centerRadius;
	}
}

/// <comment>
/// by Benoit Fouletier
/// modified by Jose Pascua
/// Controls mass flocks.
/// </comment>