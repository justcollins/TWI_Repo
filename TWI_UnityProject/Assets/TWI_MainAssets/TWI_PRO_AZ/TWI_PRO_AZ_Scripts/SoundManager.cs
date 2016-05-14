using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	private ShieldAndLazer Laser;
	private SubControl SubC;

	private bool PlayLaser = false;
	public bool PlayLas = false;

	public bool PlayEngine = false;


	public AudioSource LazerSource;
	public AudioSource EngineSource;

	// Use this for initialization
	void Start () {

		Laser = FindObjectOfType<ShieldAndLazer> ();
		SubC = FindObjectOfType<SubControl> ();
	}
	
	// Update is called once per frame
	void Update () {

		LaserSound ();
		EngineSound ();
	}

public void LaserSound()
	{
		if (Laser.PlayLazer == true && PlayLas == false) {
			
			LazerSource.Play ();
			PlayLas = true;
			
		} 
		else if (Laser.PlayLazer == false) 
		{
			LazerSource.Stop();
		}
	}
	public void EngineSound()
	{
		if (SubC.isEngineOn == true && PlayEngine == false) {

			EngineSource.Play ();

		} else if (SubC.isEngineOn == false) {

			EngineSource.Stop();
		}
	}
}
