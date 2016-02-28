using UnityEngine;
using System.Collections;

/**
 *   Enemy Movement Class
 *   10 Feb 2016
 *   Jose Pascua
 * 
 *   Steering towards targets
 */

public enum EnemyType {
	None = 0,
	Tagger = 1,
	Macrophage = 2
}

public class EnemyMovement : MonoBehaviour {

	[Header("Basic Options")]
	public float minVelocity = 0.5f;
	public float maxVelocity = 20f;
	public float randomness = 1.0f;
	public Transform chasee;
	public float rotateSpeed = 20f;

	private Rigidbody rb;

	void Awake() {
		rb = GetComponent<Rigidbody> ();
	}

	void Update() {
		Movement ();
		LookAtTarget ();
	}

	void Movement() {
		rb.velocity += CalculateSteering() * Time.deltaTime;
		
		// enforce min and max velocity
		float speed = rb.velocity.magnitude;
		if (speed > maxVelocity) {
			rb.velocity = rb.velocity.normalized*maxVelocity;
		} else if (speed < minVelocity) {
			rb.velocity = rb.velocity.normalized*minVelocity;
		}
	}
	
	Vector3 CalculateSteering() {
		Vector3 randomize = new Vector3 ((Random.value * 2) - 1, (Random.value * 2) - 1, (Random.value * 2) - 1);
		randomize.Normalize ();
		randomize *= randomness;

		return (( chasee.position - transform.position ) + (randomize));
	}

	void LookAtTarget() {
		Vector3 dir;
		dir = (chasee.position - transform.position).normalized;
		
		if (dir == Vector3.zero) {
		} else {
			transform.rotation = Quaternion.Slerp (transform.rotation, (Quaternion.LookRotation (dir, Vector3.up)), Time.deltaTime * rotateSpeed);
		}
	}
}

/// <comment>
/// by Jose Pascua
/// Steering towards targets and maneuvering through 3D space.
/// </comment>