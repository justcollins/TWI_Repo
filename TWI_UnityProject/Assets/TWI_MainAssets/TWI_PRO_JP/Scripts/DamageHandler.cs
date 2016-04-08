using UnityEngine;
using System.Collections;

/**
 *   Damage Handler Class
 *   16 Mar 2016
 *   Jose Pascua
 * 
 * 	 Attach this to the ship; it handles all the damage that will be done to it.
 */

public class DamageHandler : MonoBehaviour {

	public GameObject damageVisual;
	public string playerTag = "Player";
	public float antibodyDamage = 0.01f;
	public float macrophageDamage = 0.01f;

	private bool macrophageNear = false;

	public void setMacrophageNear(bool _b) { macrophageNear = _b; }
	public bool getMacrophageNear() { return macrophageNear; }

	void Update() {
		AntibodyDamage ();
		MacrophageDamage ();
	}

	void AntibodyDamage() {
		if ( GetComponent<FixedJoint>() ) {
			// adds to the tag percentage based on how many hinges are attached
			ShipVisibility.AddTagPercent( antibodyDamage * GetComponents<FixedJoint>().Length );
		}
	}

	void MacrophageDamage() {
		if (macrophageNear) {
			//increase the pressure
			gameObject.GetComponent<Submarine_Resources>().setCabinPressure ( -macrophageDamage ); 

			// turns on the damage visual
			damageVisual.SetActive (true);
		} else {
			damageVisual.SetActive(false);
		}
	}

}
