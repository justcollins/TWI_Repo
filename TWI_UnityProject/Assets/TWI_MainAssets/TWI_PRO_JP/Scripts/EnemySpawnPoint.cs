﻿﻿using UnityEngine;
using System.Collections;

/**
 *   Enemy Spawn Point Class
 *   2 June 2016
 *   Jose Pascua
 * 
 *   Attach this to a respawn point
 */

public class EnemySpawnPoint : MonoBehaviour {

    public EnemyType type;
    public EnemyRespawn myEnemy;
    public float waitSeconds = 15f;
    public Transform firstWaypoint;

    private GameObject myEnemyObject;
    private bool iDied = false;

    void Awake() {
        myEnemyObject = myEnemy.gameObject;
    }

    void Update() {
        Respawn();
    }

    void Respawn() {
        if (iDied == true) {
            //Debug.Log ("Died");
            if (type == EnemyType.Macrophage) {
                if (myEnemyObject.GetComponent<EnemyMovement>()) {
                    myEnemyObject.GetComponent<EnemyMovement>().chasee = firstWaypoint;
                }

                myEnemyObject.GetComponent<EnemyHealth>().ResetHealth();
            } else if (type == EnemyType.ArbiterMinion) {

                myEnemyObject.GetComponent<EnemyHealth>().ResetHealth();
            } else if (type == EnemyType.Tagger_IGG) {
                //myEnemyObject = Instantiate(myEnemy.gameObject, transform.position, transform.rotation) as GameObject;
                StartCoroutine(TurnOnOffBoidController(3.0f));
            }

            myEnemyObject.transform.position = this.transform.position;
            myEnemyObject.transform.rotation = this.transform.rotation;

            if (type == EnemyType.Macrophage) {
                if (myEnemyObject.GetComponent<BasicEnemyBehavior>()) {
                    myEnemyObject.GetComponent<BasicEnemyBehavior>().GotRespawned();
                }
            } else if (type == EnemyType.ArbiterMinion) {

                if (myEnemyObject.GetComponent<BasicEnemyBehavior>()) {
                    myEnemyObject.GetComponent<BasicEnemyBehavior>().GotRespawned();
                }
            }

            iDied = false;
        }
    }

    IEnumerator TurnOnOffBoidController(float waitTime) {
        Debug.Log("gonna wait");

        myEnemy.GetComponent<BoidController>().enabled = false;
        yield return new WaitForSeconds(waitTime);
        Debug.Log("we waited");
        myEnemy.GetComponent<BoidController>().SelfPopulate();
        iDied = false;
        myEnemy.GetComponent<BoidController>().enabled = true;
    }

    public void SetIDied(bool _b) { iDied = _b; }
    public bool GetIDied() { return iDied; }
}