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
			Debug.Log ( this.gameObject.name + " died from low health!" );
			GameObject.Destroy(this.gameObject);
		}
	}
}

/// <comment>
/// by Jose Pascua
/// Health for the enemy;
/// </comment>