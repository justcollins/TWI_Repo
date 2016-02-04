using UnityEngine;
using System.Collections;

/**
 *   BoidFlocking
 *   from the unifycommunity wikia
 *   converted to C# by Benoit Fouletier
 *   additional modifications by Jose Pascua
 */

public class BoidFlocking : MonoBehaviour {

	internal BoidController controller;
	private Rigidbody rb;

	void Awake() {
		rb = GetComponent<Rigidbody> ();
	}

	IEnumerator Start() {
		while (true) {
			if (controller) {
				rb.velocity += CalculateSteering() * Time.deltaTime;

				// enforce min and max velocity
				float speed = rb.velocity.magnitude;
				if (speed > controller.maxVelocity) {
					rb.velocity = rb.velocity.normalized*controller.maxVelocity;
				} else if (speed < controller.minVelocity) {
					rb.velocity = rb.velocity.normalized*controller.minVelocity;
				}
				yield return new WaitForSeconds( Random.Range (0.3f, 0.5f) );
			}
		}
	}

	Vector3 CalculateSteering() {
		Vector3 randomize = new Vector3 ((Random.value * 2) - 1, (Random.value * 2) - 1, (Random.value * 2) - 1);
		randomize.Normalize ();
		randomize *= controller.randomness;

		Vector3 center = controller.flockCenter - transform.localPosition;
		Vector3 velocity = controller.flockVelocity - rb.velocity;
		Vector3 follow = controller.chasee.position - transform.position;

		return (center + velocity + follow * 2 + randomize);
	}
}

/// <comment>
/// by Benoit Fouletier
/// modified by Jose Pascua
/// Controls individual flocking behavior.
/// </comment>