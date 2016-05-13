using UnityEngine;
using System.Collections;



public class IntConverter : MonoBehaviour {

	private BodyFlow BF;
	private Radar R;
	// Use this for initialization
	void Start () {

		BF = FindObjectOfType<BodyFlow>();		
		R = FindObjectOfType<Radar>();			
	}
	
	// Update is called once per frame
	void Update () {

		//Locator (BF.currentZone);

	}

	public void Locator(GameObject CurZone)
	{
		if (CurZone.name == "HEART") {
			R.Location = 0;
		}
		else if (CurZone.name == "LUNGS") {
			R.Location = 1;
		}
		else if (CurZone.name == "STOMACH") {
			R.Location = 2;			
		}
		else if (CurZone.name == "ARTERY_R") {
			R.Location = 3;		
		}
		else if (CurZone.name == "ARTERY_L") {
			R.Location = 4;	
		}
		else if (CurZone.name == "VEINS") {
			R.Location = 5;
		}
		else if (CurZone.name == "ARTERYSM_L") {
			R.Location = 6;
		}
		else if (CurZone.name == "ARTERYSM_R") {
			R.Location = 7;
		}
	}
}
