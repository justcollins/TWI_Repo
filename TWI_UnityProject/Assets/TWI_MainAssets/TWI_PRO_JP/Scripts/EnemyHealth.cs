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
	
	public void SetHealth(float h) { health = h; }
	public void AddHealth(float h) { health += h; }
	public float GetHealth() { return health; }
	public float GetHealthPercentage() { return (health / maxHealth); }

	void Awake() {
		health = maxHealth;
	}

	void Update() {
		if (health <= 0) {
			Death ();
		}
	}
	
	void Death() {
		switch((int)type) {
		case 0:
			break;
			
		case 1: //tagger
			Debug.Log ( this.gameObject.name + " died from low health!" );
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