﻿﻿using UnityEngine;
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
    public float arbiterminionDamage = 0.01f;
    public float arbiterparasiteDamage = 0.01f;

    private bool macrophageNear = false;
    private bool arbiterminionNear = false;
    private bool arbiterparasiteNear = false;

    public void setMacrophageNear(bool _b) { macrophageNear = _b; }
    public bool getMacrophageNear() { return macrophageNear; }
    public void setArbiterMinionNear(bool _b) { arbiterminionNear = _b; }
    public bool getArbiterMinionNear() { return arbiterminionNear; }
    public void setArbiterParasiteNear(bool _b) { arbiterparasiteNear = _b; }
    public bool getArbiterParasiteNear() { return arbiterparasiteNear; }

    void Update() {
        AntibodyDamage();
        MacrophageDamage();
        ArbiterMinionDamage();
        ArbiterParasiteDamage();
    }

    void AntibodyDamage() {
        if (GetComponent<FixedJoint>()) {
            // adds to the tag percentage based on how many hinges are attached
            ShipVisibility.AddTagPercent(antibodyDamage * GetComponents<FixedJoint>().Length);
        }
    }

    void MacrophageDamage() {
        if (macrophageNear) {
            //increase the pressure
            gameObject.GetComponent<Submarine_Resources>().setCabinPressure(-macrophageDamage);

            // turns on the damage visual
            damageVisual.SetActive(true);
        } else {
            damageVisual.SetActive(false);
        }
    }

    void ArbiterMinionDamage() {
        if (arbiterminionNear) {
            //increase the pressure
            gameObject.GetComponent<Submarine_Resources>().setCabinPressure(-arbiterminionDamage);

            // turns on the damage visual
            damageVisual.SetActive(true);
        } else {
            damageVisual.SetActive(false);
        }
    }

    void ArbiterParasiteDamage() {
        if (arbiterparasiteNear) {
            //increase the pressure
            gameObject.GetComponent<Submarine_Resources>().setCabinPressure(-arbiterparasiteDamage);

            // turns on the damage visual
            damageVisual.SetActive(true);
        } else {
            damageVisual.SetActive(false);
        }
    }
}
