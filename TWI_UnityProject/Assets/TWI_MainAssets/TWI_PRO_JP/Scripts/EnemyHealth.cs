﻿﻿﻿using UnityEngine;
using System.Collections;

/**
 *   Enemy Health Class
 *   17 Feb 2016
 *   Jose Pascua
 * 
 *   A class that contains a reference to the
 * 	 next waypoint and a radius.
 */

public class EnemyHealth : MonoBehaviour {

    public EnemyType type;
    public float maxHealth = 100;
    public float health;

    private Submarine_Resources submarineResources;
    private EnemySpawnPoint respawnPoint;

    [Header("Death")]
    public GameObject[] deathPrefab = null;

    #region Accessors and Mutators
    public void ResetHealth() { health = maxHealth; }
    public void SetHealth(float h) { health = h; }
    public void AddHealth(float h) { health += h; }
    public float GetHealth() { return health; }
    public float GetHealthPercentage() { return ((float)(health / maxHealth)); }
    #endregion

    void Awake() {
        health = maxHealth;
        submarineResources = FindObjectOfType<Submarine_Resources>();

        if (GetComponent<EnemyRespawn>()) {
            respawnPoint = GetComponent<EnemyRespawn>().point;
        }
    }

    void Update() {
        if (health <= 0) {
            if (GetComponent<EnemyRespawn>()) {
                RDeath();
            } else {
                Death();
            }

        }
    }

    void RDeath() {
        switch (type) {

            case EnemyType.Tagger_IGG:
                break;

            case EnemyType.Macrophage:
            case EnemyType.ArbiterMinion:
                transform.position = new Vector3(9999, 9999, 9999);
                respawnPoint.SetIDied(true);
                break;
        }
    }

    void Death() {
        switch (type) {

            case EnemyType.Tagger_IGG: //tagger
                Debug.Log(this.gameObject.name + " died from low health!");
                BoidFlocking bf = GetComponent<BoidFlocking>();
                bf.DestroyMe();
                break;

            case EnemyType.ArbiterParasite:
                GameObject.Destroy(this.gameObject);
                break;

            case EnemyType.Macrophage:
            case EnemyType.ArbiterMinion:
                Debug.Log(this.gameObject.name + " died from low health!");
                if (deathPrefab != null) {
                    foreach (GameObject dp in deathPrefab) {
                        if (dp != null) {
                            Instantiate(dp, transform.position, transform.rotation);
                        }
                    }
                }
                GameObject.Destroy(this.gameObject);
                break;
        }
    }
}

/// <comment>
/// by Jose Pascua
/// Health for the enemy;
/// </comment>