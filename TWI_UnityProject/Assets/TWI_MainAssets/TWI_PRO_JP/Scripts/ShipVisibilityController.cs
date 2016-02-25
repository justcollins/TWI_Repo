using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 *   Ship Visibility Class
 *   10 Feb 2016
 *   Jose Pascua
 * 
 *   Debugger for the ShipVisibility.cs class.
 */

public class ShipVisibilityController : MonoBehaviour {

	public KeyCode addVisibility;
	public KeyCode removeVisibility;
	public KeyCode addTagPercentage;
	public KeyCode removeTagPercentage;
	public KeyCode switchTagBool;

	public Slider visibilitySlider;
	public Slider tagPercentSlider;
	public Image tagImage;
	public Color tagColor;

	void Update () {
		ManipulateVisiblity ();
		ManipulateTagPercentage ();
		ManipulateTagBool ();
		UpdateGUI ();
	}

	void ManipulateVisiblity() {
		if ((Input.GetKey (addVisibility)) && (ShipVisibility.GetVisibility() < 100f)) {
			ShipVisibility.AddVisibility( 0.5f );
		}
		if ((Input.GetKey (removeVisibility)) && (ShipVisibility.GetVisibility() > 0.5f)) {
			ShipVisibility.AddVisibility( -0.5f );
		}
	}

	void ManipulateTagPercentage() {
		if ((Input.GetKey (addTagPercentage)) && (ShipVisibility.GetTagPercent() < 100f)) {
			ShipVisibility.AddTagPercent( 0.5f );
		}
		if ((Input.GetKey (removeTagPercentage)) && (ShipVisibility.GetTagPercent() > 0.5f)) {
			ShipVisibility.AddTagPercent( -0.5f );
		}
	}

	void ManipulateTagBool() {
		if (Input.GetKeyDown (switchTagBool)) {
			ShipVisibility.SetTagged( !ShipVisibility.GetTagged() );
		}
	}

	void UpdateGUI() {
		visibilitySlider.value = ShipVisibility.GetVisibility ();
		tagPercentSlider.value = ShipVisibility.GetTagPercent ();
		if (ShipVisibility.GetTagged ()) {
			tagImage.color = tagColor;
		} else {
			tagImage.color = Color.white;
		}
	}


}

/// <comment>
/// by Jose Pascua
/// Debugger for the class ShipVisiblity
/// </comment>