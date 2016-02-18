using UnityEngine;
using System.Collections;
	
	public class UltimateRadar : MonoBehaviour {

	public enum RadarLocations : int {TopLeft, TopCenter, TopRight, BottomLeft, BottomCenter, BottomRight, Left, Center, Right, Custom};

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

	//Blip Info
	//Blip 1
	public bool   radarBlip1Active;
	public Color  radarBlip1Color = new Color(0, 0, 255);
	public string radarBlip1Tag;
	//Blip 2
	public bool   radarBlip2Active;
	public Color  radarBlip2Color = new Color(0, 0, 255);
	public string radarBlip2Tag;

	public string normalTag;

	//Radar Specs
	private GameObject _centerObject;
	private GameObject norm;
	private int        _radarWidth;
	private int        _radarHeight;
	private Vector2    _radarCenter;
	private Texture2D  _radarCenterTexture;
	private Texture2D  _radarBlip1Texture;
	private Texture2D  _radarBlip2Texture;
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
		GameObject[] norma;

		gos = GameObject.FindGameObjectsWithTag(radarCenterTag);
		norma = GameObject.FindGameObjectsWithTag(normalTag);


		_centerObject = gos[0];
		norm = norma [0];
		radarBlip1Active = false;

	}

	void OnGUI ()
	{
		GameObject[] gos;
		GameObject[] test;


		if (Input.GetKeyDown ("e") && ShortRange==true) {
			LongRange=true;
			ShortRange=false;
		}
		if (Input.GetKeyDown ("l") && ShortRange==false) {
			ShortRange=true;
			LongRange=false;
		}


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


			/*if (radarBlip2Active) {
				
				foreach (GameObject te in test) {
					RangeBlip (te,_radarBlip2Texture);
				}

			}*/
			
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
			float bY = centerPos.z - extPos.z;
			
			// Scale the objects position to fit within the radar
			bX = bX * radarZoom;
			bY = bY * radarZoom;
			
			// For a round radar, make sure we are within the circle
			if (dist <= (_radarWidth - 2) * 0.5 / radarZoom) {
				Rect clipRect = new Rect (_radarCenter.x - bX - 1.5f, _radarCenter.y + bY - 1.5f, 3, 3);
				GUI.DrawTexture (clipRect, blipTexture);
			}
		}
		
	}
	void longBlip(GameObject te,Texture2D blipTexture)
	{
		if (_centerObject && LongRange) {
			Vector3 centerPos = _centerObject.transform.position;
			Vector3 extPos = te.transform.position;
			// Get the distance to the object from the centerObject
			float dist = Vector3.Distance (centerPos, extPos);


			float bX = centerPos.x - extPos.x;
			float bY = centerPos.z - extPos.z;
			
			// Scale the objects position to fit within the radar
			bX = bX * radarZoom;
			bY = bY * radarZoom;
			
			// For a round radar, make sure we are within the circle
		
		}
	}


	float SignedAngleBetween(Vector3 a, Vector3 b, Vector3 n){
		// angle in [0,180]
		float angle = Vector3.Angle(a,b);
		float sign = Mathf.Sign(Vector3.Dot(n,Vector3.Cross(a,b)));
		
		// angle in [-179,180]
		float signed_angle = angle * sign;
		
		// angle in [0,360] (not used but included here for completeness)
		//float angle360 =  (signed_angle + 180) % 360;
		
		return signed_angle;
	}

	void LongAngle(GameObject Virus)
	{
		Vector3 centerPos = _centerObject.transform.position;
		Vector3 extPos = Virus.transform.position;
		Vector3 normPos = norm.transform.position;
		float dist = Vector3.Distance (centerPos, extPos);
		float angle = Vector3.Angle(centerPos, extPos);
		float Hope= SignedAngleBetween(centerPos,extPos,normPos);
		Debug.Log ("basic angle " + angle);
		Debug.Log ("distance " + dist);
		Debug.Log ("angle " + Hope);

		if (angle < 90f && angle > 80f) {
			Debug.Log ("Shows up");

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
		
		if (dist >= 10)
		{
			// this is the diameter of our largest radar circle
			//Rect clipRect = new Rect (_radarCenter.x - bX - 1.5f, _radarCenter.y + bY - 1.5f, 3, 3);
			Debug.Log ("Too Far");
			Debug.Log("This is some angle " +deltay);
			Debug.Log ("This is dx " + dx);
			Debug.Log ("This is dz " + dz);
		}
	}
}
