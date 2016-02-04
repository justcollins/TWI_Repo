using UnityEngine;
using System.Collections;

/**
 *   Enemy Vision Class
 *   20 Jan 2016
 *   Jose Pascua
 * 
 *   Enemies use this in order to pursue the player
 */

public class EnemyVision : MonoBehaviour {

	[Range(0,100f)]public float visionRadius = 50f;
	public bool seenShip = false;

	void Update() {
	} // end of Update()

	void DetectingShip() {
	} // end of DetectingShip()
}

/// <comment>
/// by Jose Pascua
/// Enemies use this in order to pursue the player
/// </comment>