using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	private ShieldAndLazer Laser;
	private SubControl SubC;
	private Radar Rad;
	private bool PlayLaser = false;
	public bool PlayLas = false;

	private bool PlayShield = false;
	public bool PlayShie = false;


	public bool PlayEngine = false;


	public AudioSource LazerSource;
	public AudioSource EngineSource;
	public AudioSource ShieldSource;
	public AudioSource RadarSource;

	// Use this for initialization
	void Start () {

		Laser = FindObjectOfType<ShieldAndLazer> ();
		SubC = FindObjectOfType<SubControl> ();
		Rad = FindObjectOfType<Radar> ();
	}
	
	// Update is called once per frame
	void Update () {

		LaserSound ();
		ShieldSound ();
		EngineSound ();
		RadarSound ();
	}

public void RadarSound()
	{
		if (Rad.SoundCheck == true) 
		{
			RadarSource.Play();
			Rad.SoundCheck = false;
		}

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
public void ShieldSound()
	{
		if (Laser.PlayShield == true && PlayShie == false) {
			
			ShieldSource.Play ();
			PlayShie = true;
			
		} 
		else if (Laser.PlayShield == false) 
		{
			ShieldSource.Stop();
		}
	}
public void EngineSound()
	{
		if (SubC.isEngineOn == true && PlayEngine == true) {

			EngineSource.Play ();
			PlayEngine = false;

		} else if (SubC.isEngineOn == false) {

			EngineSource.Stop();
		}
	}

}
