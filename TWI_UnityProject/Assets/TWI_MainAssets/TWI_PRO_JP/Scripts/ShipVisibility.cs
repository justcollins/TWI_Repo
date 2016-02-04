using UnityEngine;
using System.Collections;

/**
 *   Ship Visibility Class
 *   20 Jan 2016
 *   Jose Pascua
 * 
 *   This class has information on the visibility of the ship;
 * 	 This includes the ship's signature and the ship's tag;
 *   Since there will only be one ship, all the variables of
 *   the class are static.
 * 
 *   Note: The only things interacting with this class should
 *   be the enemies and the player.
 */

public class ShipVisibility : MonoBehaviour {

	[Range(0,100f)] private static float myVisibility = 0f;
	private static bool amTagged = false;
	private static GameObject myShip;

	public static void SetVisibility(float n) { myVisibility = n; }
	public static float GetVisibility() { return myVisibility; }

	public static void SetTag(bool b) { amTagged = b; }
	public static bool GetTag() { return amTagged; }

	public static void SetShip(GameObject g) { myShip = g; }
	public static GameObject GetShip() { return myShip; }
	public static Vector3 GetShipPos() { return myShip.transform.position; }
}

/// <comment>
/// by Jose Pascua
/// Information on the visibility of the ship, including if it is tagged.
/// </comment>