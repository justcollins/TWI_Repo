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

public enum Hub {
	NoHub,
	FirstHub,
	SecondHub,
	ThirdHub,
	FourthHub,
}

public class ShipVisibility : MonoBehaviour {
	
	[Range(0,100f)] private static float myVisibility = 0f;
	[Range(0,100f)] private static float myTagPercent = 0f;
	private static bool amTagged = false;
	private static GameObject myShip;
	private static Hub shipHub = Hub.FirstHub;
	
	public static void SetVisibility(float n) { myVisibility = n; }
	public static void AddVisibility(float n) { myVisibility += n; }
	public static float GetVisibility() { return myVisibility; }
	
	public static void SetTagPercent(float n) { myTagPercent = n; }
	public static void AddTagPercent(float n) { myTagPercent += n; }
	public static float GetTagPercent() { return myTagPercent; }
	
	public static void SetTagged(bool b) { amTagged = b; }
	public static bool GetTagged() { return amTagged; }
	
	public static void SetShip(GameObject g) { myShip = g; }
	public static GameObject GetShip() { return myShip; }
	public static Vector3 GetShipPos() { return myShip.transform.position; }
	public static void SetShipHub(Hub h) { shipHub = h; }
	public static Hub GetShipHub() { return shipHub; }
	
	
	void Awake() {
		myVisibility = 0f;
		myTagPercent = 0f;
		amTagged = false;
		myShip = GameObject.FindGameObjectWithTag ( "Player" );
	}
}

/// <comment>
/// by Jose Pascua
/// Information on the visibility of the ship, including if it is tagged.
/// </comment>