using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class BlurOxyDown : MonoBehaviour 
{
	public Submarine_Resources sub;
	public ColorCorrectionCurves ccc;
	private float value;
	public float subt;

	// Use this for initialization
	void Start () 
	{
		sub = GetComponent<Submarine_Resources> ();
		value = sub.getOxygenLevel();


	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.A)) 
		{
			sub.setOxygenLevel (-5f);
			value = sub.getOxygenLevel();
		}

		if(sub.getOxygenLevel() < 10)
			Camera.main.GetComponent<ColorCorrectionCurves> ().saturation -= subt;
	
	}
}
