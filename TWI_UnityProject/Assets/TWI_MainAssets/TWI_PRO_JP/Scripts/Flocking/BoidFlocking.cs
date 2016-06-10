﻿using UnityEngine;
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
    public string playerWeaponTag = "Player Weapon";
    public float speedToPlayer = 10f;

    private bool attached = false;

    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    IEnumerator Start() {
        while (!attached) {
            if (controller) {
                //if (controller.chasee.tag == ShipVisibility.GetShip().tag) {
                //    // STEER DIRECTLY TO THE FUCKER
                //    rb.velocity += CalculateSteering() * (controller.maxVelocity * speedToPlayer);
                //} else {
                //    // whatevs bruh
                rb.velocity += CalculateSteering() * Time.deltaTime;
                //}

                // enforce min and max velocity
                float speed = rb.velocity.magnitude;
                if (speed > controller.maxVelocity) {
                    rb.velocity = rb.velocity.normalized * controller.maxVelocity;
                } else if (speed < controller.minVelocity) {
                    rb.velocity = rb.velocity.normalized * controller.minVelocity;
                }
                yield return new WaitForSeconds(Random.Range(controller.lowerboundWait, controller.upperboundWait));
            }
        }
    }

    Vector3 CalculateSteering() {
        Vector3 randomize = new Vector3((Random.value * 2) - 1, (Random.value * 2) - 1, (Random.value * 2) - 1);
        randomize.Normalize();
        randomize *= controller.randomness;

        Vector3 center = controller.flockCenter - transform.localPosition;
        Vector3 velocity = controller.flockVelocity - rb.velocity;
        Vector3 follow = controller.chasee.position - transform.position;

        return (center + velocity + follow * 2 + randomize);
    }

    Vector3 SteerDirectlyToFucker() {
        return (controller.chasee.position - transform.position);
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Player Weapon") {
            DestroyMe();
        }
    }

    public void DestroyMe() {
        controller.RemoveBoid(this);
        GameObject.Destroy(this.gameObject);
    }

    public void StickToOther(GameObject go) {
        if ((controller.stickToPlayer) && (!attached)) {
            transform.parent = go.transform;
            FixedJoint joint = go.AddComponent<FixedJoint>() as FixedJoint;
            joint.connectedBody = this.rb;
            attached = true;
        }
    }
}

/// <comment>
/// by Benoit Fouletier
/// modified by Jose Pascua
/// Controls individual flocking behavior.
/// </comment>