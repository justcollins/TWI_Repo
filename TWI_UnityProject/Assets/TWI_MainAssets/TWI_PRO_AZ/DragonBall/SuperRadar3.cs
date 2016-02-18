using UnityEngine;
using System.Collections;

public class SuperRadar3 : MonoBehaviour {
	public enum RadarLocations : int {BottomCenter,Custom};
	
	//Display Location
	public RadarLocations radarLocation = RadarLocations.BottomCenter;
	public Vector2 radarLocationCustom;
	public Texture2D radarTexture;
	public Texture2D longRadarTexture;
	public float radarSize = 0.20f;  
	public float radarZoom = 10.0f;
	public bool   radarCenterActive;
	public Color  radarCenterColor = new Color(255, 255, 255);
	public string radarCenterTag;
	public GameObject FarLeft;
	public GameObject Left;
	public GameObject Straight;
	public GameObject Right;
	public GameObject FarRight;

	
	//Blip Info
	//Blip 1
	public bool   radarBlip1Active;
	public Color  radarBlip1Color = new Color(0, 0, 255);
	public string radarBlip1Tag;
	//Blip 2
	public bool   radarBlip2Active;
	public Color  radarBlip2Color = new Color(0, 0, 255);
	public string radarBlip2Tag;

	//Radar Specs
	private GameObject _centerObject;
	private GameObject norm;
	private int        _radarWidth;
	private int        _radarHeight;
	private Vector2    _radarCenter;
	private Texture2D  _radarCenterTexture;
	private Texture2D  _radarBlip1Texture;
	private Texture2D  _radarBlip2Texture;
	private Color color;
	//Check Variables
	private bool RadarOn = false;
	private bool LongRadarOn = false;
	private bool ShortRange = false;
	private bool LongRange= true;
	
	
	void Start ()
	{
		// Determine the size of the radar
		_radarWidth = (int)(Screen.width * radarSize);
		_radarHeight = _radarWidth;
		
		// Get the location of the radar
		setRadarLocation ();
		
		// Create the blip textures
		_radarCenterTexture = new Texture2D (3, 3, TextureFormat.RGB24, false);
		_radarBlip1Texture = new Texture2D (3, 3, TextureFormat.RGB24, false);
		_radarBlip2Texture = new Texture2D(3, 3, TextureFormat.RGB24, false);
		
		CreateBlipTexture (_radarCenterTexture, radarCenterColor);
		CreateBlipTexture (_radarBlip1Texture, radarBlip1Color);
		CreateBlipTexture(_radarBlip2Texture, radarBlip2Color);
		
		
		GameObject[] gos;

		gos = GameObject.FindGameObjectsWithTag(radarCenterTag);
		
		_centerObject = gos[0];
		radarBlip1Active = false;
		
	}
	void Update(){
		if (Input.GetKeyDown ("e") && ShortRange==true) {
			LongRange=true;
			ShortRange=false;
			Debug.Log ("Were in long range");
		}
		else if (Input.GetKeyDown ("e") && ShortRange==false) {
			ShortRange=true;
			LongRange=false;
			Debug.Log ("Were in short range");
		}

	}

	void OnGUI ()
	{
		GameObject[] gos;
		GameObject[] test;

		
		if (ShortRange== true && LongRange == false) {
			
			Rect radarRect = new Rect (_radarCenter.x - _radarWidth / 2, _radarCenter.y - _radarHeight / 2, _radarWidth, _radarHeight);
			GUI.DrawTexture (radarRect, radarTexture);;
			
			if (Input.GetKeyDown ("r") && RadarOn == false) {
				RadarOn = true;	
				StartCoroutine (MyCoroutine ());
				
			} else if (Input.GetKeyDown ("r") && RadarOn == true) {
				RadarOn = false;
			}
			
			
			if (radarBlip1Active) {
				// Find all game objects
				gos = GameObject.FindGameObjectsWithTag (radarBlip1Tag); 
				
				// Iterate through them and call drawBlip function
				foreach (GameObject go in gos) {
					drawBlip (go, _radarBlip1Texture);
				}
			}
			
			if (radarCenterActive) {
				Rect centerRect = new Rect (_radarCenter.x - 1.5f, _radarCenter.y - 1.5f, 3, 3);
				GUI.DrawTexture (centerRect, _radarCenterTexture);
			}
			
		}
		
		else if (LongRange== true && ShortRange== false) {
			
			test = GameObject.FindGameObjectsWithTag (radarBlip2Tag); 
			
			Rect radarRect = new Rect (_radarCenter.x - _radarWidth / 2, _radarCenter.y - _radarHeight / 2, _radarWidth, _radarHeight);
			GUI.DrawTexture (radarRect, longRadarTexture);
			
			if (Input.GetKeyDown ("r") && LongRadarOn == false) {
				LongRadarOn = true;	
				foreach(GameObject te in test){
					RangeBlip(te,_radarBlip2Texture);

					
				}
			} 
			
			else if (Input.GetKeyDown ("r") && LongRadarOn == true) {
				LongRadarOn = false;
			}
			
			
			if (radarCenterActive) {
				Rect centerRect = new Rect (_radarCenter.x - 1.5f, _radarCenter.y - 1.5f, 3, 3);
				GUI.DrawTexture (centerRect, _radarCenterTexture);
			}
			
			
		}
		
	}
	void drawBlip(GameObject go, Texture2D blipTexture)
	{
		if (_centerObject && ShortRange) {
			Vector3 centerPos = _centerObject.transform.position;
			Vector3 extPos = go.transform.position;
			
			// Get the distance to the object from the centerObject
			float dist = Vector3.Distance (centerPos, extPos);
			
			// Get the object's offset from the centerObject
			float bX = centerPos.x - extPos.x;
			float bZ = centerPos.z - extPos.z;
			float bY = centerPos.y - extPos.y;
			
			// Scale the objects position to fit within the radar
			bX = bX * radarZoom;
			bY = bY * radarZoom;

		
			
			// For a round radar, make sure we are within the circle
			if (dist <= (_radarWidth - 2) * 0.5 / radarZoom) {
				Rect clipRect = new Rect (_radarCenter.x - bX - 1.5f, _radarCenter.y + bZ - 1.5f, 3, 3);
				GUI.DrawTexture (clipRect, blipTexture);
				if (bY > 0)
				{
					Debug.Log ("The Virus is under you");
				}
				else 
				{
					Debug.Log ("The Virus is above you");
				}

			}
		}
		
	}

	// Create the blip textures
	void CreateBlipTexture(Texture2D tex, Color c)
	{
		Color[] cols = {c, c, c, c, c, c, c, c, c};
		tex.SetPixels(cols, 0);
		tex.Apply();
	}
	
	void setRadarLocation()
	{
		// Sets radarCenter based on enum selection
		if(radarLocation == RadarLocations.BottomCenter)
		{
			_radarCenter = new Vector2(Screen.width / 2, Screen.height - _radarHeight / 2);
		}
		else if(radarLocation == RadarLocations.Custom)
		{
			_radarCenter = radarLocationCustom;
		}
	} 
	
	IEnumerator MyCoroutine()
	{
		radarBlip1Active = true;
		yield return new WaitForSeconds (10);
		radarBlip1Active = false;			
		
	}

	private void RangeBlip(GameObject go, Texture aTexture)
	{
		Vector3 centerPos = _centerObject.transform.position;
		Vector3 extPos = go.transform.position;
		
		// first we need to get the distance of the enemy from the player
		float dist = Vector3.Distance(centerPos, extPos);
		
		float dx = centerPos.x - extPos.x; // how far to the side of the player is the enemy?
		float dz = centerPos.z - extPos.z; // how far in front or behind the player is the enemy?
		
		// what's the angle to turn to face the enemy - compensating for the player's turning?
		float deltay = Mathf.Atan2(dx, dz) * Mathf.Rad2Deg - 270 - _centerObject.transform.eulerAngles.y;
		
		// just basic trigonometry to find the point x,y (enemy's location) given the angle deltay
		float bX = dist * Mathf.Cos(deltay * Mathf.Deg2Rad);
		float bY = dist * Mathf.Sin(deltay * Mathf.Deg2Rad);
		
		bX = bX * radarZoom; // scales down the x-coordinate by half so that the plot stays within our radar
		bY = bY * radarZoom; // scales down the y-coordinate by half so that the plot stays within our radar


		if (dist >= 10f)
		{
			if (deltay < -420f || deltay> -120f)
			{
			Debug.Log("Shows up " +deltay);
			}
			if(deltay < -440f && deltay> -450f){

				StartCoroutine (AlphaRoutine (Straight,dist));

			}
			else if(deltay < -90 && deltay> -100){

				StartCoroutine (AlphaRoutine (Straight,dist));

			}
			else if(deltay < -100 && deltay> -110){

				StartCoroutine (AlphaRoutine (Left,dist));
				
			}
			else if(deltay < -110 && deltay> -120){
				
				StartCoroutine (AlphaRoutine (FarLeft,dist));
				
			}
			else if(deltay < -430 && deltay> -440){
				
				StartCoroutine (AlphaRoutine (Right,dist));
				
			}
			else if(deltay < -420 && deltay> -430){
				
				StartCoroutine (AlphaRoutine (FarRight,dist));
				
			}

		}
	}

	IEnumerator AlphaRoutine(GameObject Indicator, float dist)
	{
		SpriteRenderer spRend = Indicator.transform.GetComponent<SpriteRenderer>();
		float alph = 0.1f+(dist/((dist/10f)*(dist- Mathf.Sqrt(dist))));
		Debug.Log(alph);
		Color col = spRend.color;
		col.a =alph;
		spRend.color = col;
		yield return new WaitForSeconds (10);
		col.a =0f;
		spRend.color = col;

	}
}
