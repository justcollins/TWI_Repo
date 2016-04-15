using UnityEngine;
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
	private float health;

    private Submarine_Resources submarineResources;
	private EnemySpawnPoint respawnpoint;

    public void ResetHealth() { health = maxHealth; }
	public void SetHealth(float h) { health = h; }
	public void AddHealth(float h) { 
        health += h;

        // delete after demo
        if (type == EnemyType.ArbiterParasite){
            submarineResources.setCabinPressure((h * -0.1f)); }
    }
	public float GetHealth() { return health; }
	public float GetHealthPercentage() { return ((float)(health / maxHealth)); }

	void Awake() {
		health = maxHealth;
        submarineResources = FindObjectOfType<Submarine_Resources>();

		if (GetComponent<EnemyRespawn>()) {
			respawnpoint = GetComponent<EnemyRespawn> ().point;
		}
	}
	
	void Update() {
		if (health <= 0) {
            if (GetComponent<EnemyRespawn>()) {
				RDeath ();
            } else {
                Death();
            }
		}

		//delete after testing
		if (Input.GetKeyDown (KeyCode.P)) {
			health -= 50.0f;
		}
	}

	void RDeath() {
		switch ((int)type) {
		case 0:
			break;

		case 1: //tagger
			break;

		case 2: //macrophage
			transform.position = new Vector3 ( 9999, 9999, 9999 );
			respawnpoint.SetIDied(true);
			break;
		}
	}

	void Death() {
		switch((int)type) {
		case 0:
			break;
			
		case 1: //tagger
			BoidFlocking bf = GetComponent<BoidFlocking>();
			bf.DestroyMe();
			break;
			
		case 2: //macrophage
		case 3: //paired
		case 4: //arbiter parasite
			Debug.Log ( this.gameObject.name + " died from low health!" );
			GameObject.Destroy(this.gameObject);
			break;
		}
	}
}

/// <comment>
/// by Jose Pascua
/// Health for the enemy;
/// </comment>