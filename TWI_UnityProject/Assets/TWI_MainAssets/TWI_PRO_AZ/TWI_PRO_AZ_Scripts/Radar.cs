using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Radar : MonoBehaviour {

//Public Variables

//Canvas Refernce
	public Canvas canvas;					//Game Canvas
//Text Reference
    public Text TotDist;
//GameObject References
    public GameObject radarCanvas;			//Empty GameObject to Prevent the Rotation of the Blip(+,-)
    public GameObject Ship;					//Player Ship
	public GameObject blipCanvas;			//Small Blip Canvas, 2 Pixels Height and Width
	public GameObject radarRotation;		//Rotational Radar Graphic
	public GameObject heightAbove;			//Plus Icon
	public GameObject heightBelow;			//Minus Icon
	public GameObject LongRangeRadar;		//Long Range Radar
	public GameObject FarLeftSp;			//Far Left Long Range Radar Indicator Sprite
	public GameObject LeftSp;				//Left Long Range Radar Indicator Sprite
	public GameObject StraightSp;			//Straight Long Range Radar Indicator Sprite
	public GameObject RightSp;				//Right Long Range Radar Indicator Sprite
	public GameObject FarRightSp;			//Far Right Long Range Radar Indicator Sprite
//AR Manager References
    private KeyboardManager keyboard;
	public Submarine_Resources subRes;
//Radar Variable References
	public float radarZoom;					//Radar Scan Area Size
	public float RotationSpeed;				//Radar Graphic Rotation Speed
	public float radarRange = 45f;			//Minimum Long Range Radar Scan Distance
//Ship References for the Radar
	public bool   radarCenterActive;		//Sets Center Object(Ship) to be Active
	public string radarCenterTag;			//Defines the Tag of the Center Object
//Public Blip References
	public bool radarBlip1Active = false;	//Sets Default Blip State to Not Active
	public string radarBlip1Tag;			//Defines the Tag of the Object to be Tracked

//Private Initialization

//Image References
    private Image image;
    private Image above;
    private Image below;
    private Image farLeft;
    private Image left;
    private Image straight;
    private Image right;
    private Image farRight;
//Text Reference
    private Text Dist;
//Location Reference
    private float bX;						//X Coordinate of Target Object
	private float bZ;						//Z Coordinate of Target Object
	private float bY;						//Y Coordinate of Target Object
	private bool fired=false;				//Checks if Radar is Fired
//Private Blip References
	private GameObject _BlipObject;
	private GameObject _centerObject;
	private float        _radarWidth;
	private Vector2    _radarCenter;
	private Texture2D  _radarCenterTexture;
//Radar State References
    private bool ShortRadarFired = false;
    private bool LongRadarFired = false;
    private bool ShortRadarActive = true;
    private bool LongRadarActive = false;
//Location Lights
	public Light[] lt;
	public float amplitude;
	private int Location = 0;

//Starts When Program Runs
	void Start ()
	{
//Initializing 
        keyboard = FindObjectOfType<KeyboardManager>();							//Keyboard Manager			
		RectTransform CanvasTransform = canvas.GetComponent<RectTransform> ();	//Initialize Canvas Transform Values   
		float radarWidth = CanvasTransform.rect.width;							//Initialize Radar Width
		_radarWidth = radarWidth;												//Set Radar Width
	}
//Updates Every Frame
	void Update()
	{
//Arrays
		GameObject[] gos;												//Initialize Array
		GameObject[] mother;											//Initialize Array
		gos = GameObject.FindGameObjectsWithTag (radarCenterTag);		//Array That Stores All the Objects With the Center Tag
		mother = GameObject.FindGameObjectsWithTag (radarBlip1Tag); 	//Array That Stores All the Objects With the Blip Tag, That We Track
		_centerObject = gos[0];											//Set Center Object to be the First Item in gos Array
		_BlipObject = mother[0];										//Set Blip Object to be the First Item in mother Array
		RectTransform blip = blipCanvas.GetComponent<RectTransform> ();	//Initialize Blip Canvas Transform Values
//Initialize Sprites        
		image = blipCanvas.GetComponent<Image> ();
		above = heightAbove.GetComponent<Image> ();
		below = heightBelow.GetComponent<Image> ();
		farLeft = FarLeftSp.GetComponent<Image> ();
		left = LeftSp.GetComponent<Image> ();
		straight = StraightSp.GetComponent<Image> ();
		right = RightSp.GetComponent<Image> ();
		farRight = FarRightSp.GetComponent<Image> ();
        Dist = TotDist.GetComponent<Text>();							//Initialize Distance From Object Text
//Initialize the Color Attribute of the Sprites
		Color col = image.color;
		Color abv = above.color;
		Color blw = below.color;
		Color flt = farLeft.color;
		Color lft = left.color;
		Color str = straight.color;
		Color rgt = right.color;
		Color frt = farRight.color;
        Color dst = Dist.color;
//Default Radar Position if We Have a Center Object		
		if (_centerObject) 
		{
//Initialize Variables That Track the Locations of Objects            
			Vector2 extPos = blip.localPosition;					//Position of the Green Blip on the 2D Canvas
			Vector3 centerPos = _centerObject.transform.position;	//Position of the Center Object
			Vector3 virusPos = _BlipObject.transform.position;		//Position of the Object That is Being Tracked

//Rotate the Canvas of the Radar With the Rotation of the Ship to Display the Blip Accuratly
            radarCanvas.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, Ship.transform.eulerAngles.y);
//Rotate the Canvas of the Blip With the Rotation of the Radar Canvas to Display the Blip and Height Indicators Accuratly
            blipCanvas.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, (-1) *Ship.transform.eulerAngles.y);
                                                  
		    float dist = Vector3.Distance (centerPos, virusPos);	//Get the Distance Between the Center Object and the Tracked Object
//Displaying 3D Positions Onto a 2D Canvas
			bX = virusPos.x - centerPos.x;			//X Coordiante Offset From Center Object to Tracked Object	
			bZ = virusPos.z - centerPos.z;    		//Z Coordiante Offset From Center Object to Tracked Object
			bY = virusPos.y - centerPos.y;			//Y Coordiante Offset From Center Object to Tracked Object

            bX = bX * radarZoom;					//Account for the Radar Zoom on the X Offset
			bZ = bZ * radarZoom;					//Account for the Radar Zoom on the Z Offset
//Rotate the Display Sprite at a Certain Speed
			radarRotation.transform.Rotate(0,0, (-1)*RotationSpeed * Time.deltaTime);
//Listening for Player Input
            if (Input.GetKeyDown(keyboard.LeftMouse) && ShortRadarFired == false && ShortRadarActive == true)
			{
				StartCoroutine (ShortRangeRoutine ());	//Starts the Short Range Radar for 10 Seconds
			}
            else if (Input.GetKeyDown(keyboard.LeftMouse) && LongRadarFired == false && LongRadarActive == true)
			{
				StartCoroutine (Recharge ());			//Starts the Long Range Radar for 10 Seconds
			}
//Changing Radar States
            if (Input.GetKeyDown(keyboard.Scanner) && ShortRadarActive == true)		//If Player Swaps From Short Range Radar to Long Range Radar
			{
				LongRadarActive= true;												//Activates Long Range Radar
				ShortRadarActive= false;											//Deactivates Short Range Radar
				LongRangeRadar.transform.SetSiblingIndex (2);						//Make Long Range Radar the Last Child on the Canvas, so the Player can see it
			}
			else if (Input.GetKeyDown(keyboard.Scanner) && LongRadarActive == true)	//If Player Swaps From Long Range Radar to Short Range Radar
			{
				ShortRadarActive= true;												//Activates Short Range Radar
				LongRadarActive= false;												//Deactivates Long Range Radar
				transform.SetSiblingIndex (2);										//Make Short Range Radar the Last Child on the Canvas, so the Player can see it
			}
//Display Checks

			//Make Sure That the Object We are Tracking is Within the Range of the Short Range Radar
			if (dist <= (_radarWidth - 4) * 0.5 / radarZoom && radarBlip1Active==true && ShortRadarActive == true)
				{
			//Make Sure That the Blip is Going to be Displayed Within the 2D Radar
					if( bX < radarRange && bX > (radarRange * (-1)) && bZ < radarRange && bZ > (radarRange * (-1)))
				   {            
//Displays
						extPos.x = bX;				//Blip X Coordinate is X Coordinate Offset * Radar Zoom
						extPos.y = bZ;				//Blip Y Coordiante is Z Coordinate Offset * Radar Zoom                     
						blip.localPosition= extPos;	//Set Blip Coordinates
						col.a =255f;				//Set Alpha to Maximum
						image.color = col;			//Set Alpha to the Sprite

						if (bY>0)					//If Object We are Tracking is Above Center Object Display + , Hide -
						{
							abv.a = 255f;
							above.color = abv;
                            blw.a = 0f;
                            below.color = blw;
						}
						else if(bY<0)				//If Object We are Tracking is Below Center Object Display - , Hide +
						{
							blw.a = 255f;
							below.color = blw;
                            abv.a = 0f;
                            above.color = abv;
						}
					}
					else 							//If Blip Would be Displayed Outside of the Radar, Make Everyting Transparent
					{					
						col.a =0f;
						abv.a = 0f;
						blw.a = 0f;
					
						image.color = col;
						above.color = abv;
						below.color = blw;
					}
				}
//Long Range Radar
			if (dist >= radarRange && LongRadarActive ==true) 		//If the Object is Outside Short Radar Range	
			{
				//float phi = Time.time / duration * 2 * Mathf.PI;
				//float amplitude = Mathf.Cos(phi) * 0.5F + 0.5F;

				float dX = centerPos.x - virusPos.x; 														//X Coordinate Offset
				float dZ = centerPos.z - virusPos.z;														//Z Coordinate Offset
				float deltay = Mathf.Atan2(dX, dZ) * Mathf.Rad2Deg - _centerObject.transform.eulerAngles.y;	//Angle Calculation
//Long Range Radar Fired
                if (LongRadarFired)
				{
//Display Distance to Object
                    dst.a = 255;
                    Dist.color = dst;
                    TotDist.text = dist.ToString("F2");
//Angle Settings
					if(deltay < -170f && deltay> -190f){
                        Lights(lft, flt, rgt, frt, farLeft, left, right, farRight);			//Which Indicator Lights Up
						StartCoroutine (LongRangeRoutine(straight, dist,Dist));				//Long Range Routine Starts		
					}			
					else if(deltay < -190 && deltay> -210){
                        Lights(str, flt, rgt, frt, farLeft, right, farRight, straight);
                        StartCoroutine(LongRangeRoutine(left, dist, Dist));		
					}
					else if(deltay < -210 && deltay> -230){											
                        Lights(lft, str, rgt, frt, left, right, farRight, straight);
                        StartCoroutine(LongRangeRoutine(farLeft, dist, Dist));		
					}
					else if(deltay < -150 && deltay> -170){
                        Lights(flt, lft, str, frt, left, farRight, straight, farLeft);
                        StartCoroutine(LongRangeRoutine(right, dist, Dist));		
					}
					else if(deltay < -130 && deltay> -150){
                        Lights(rgt, flt,lft,str, left, straight, farLeft, right);
                        StartCoroutine(LongRangeRoutine(farRight, dist, Dist));		
					}
					else if(deltay < -500 && deltay> -530){						
						Lights(rgt, flt,lft,str, left, straight, farLeft, right);						
						StartCoroutine(LongRangeRoutine(farRight, dist, Dist));		
					}
					else          			//If Object Not Within Search Area, Display Nothing
					{
						lft.a=0f;
						flt.a=0f;
						rgt.a=0f;
						frt.a=0f;
						str.a=0f;
						dst.a=0f;
						farLeft.color= flt;
						left.color=lft;
						right.color=rgt;
						farRight.color=frt;
						straight.color=str;                     
						Dist.color = dst;						
					}
				}
				else      					//If Object Not Within Search Area, Display Nothing (Error Catching)
				{
					lft.a=0f;
					flt.a=0f;
					rgt.a=0f;
					frt.a=0f;
					str.a=0f;
					dst.a=0f;
					farLeft.color= flt;
					left.color=lft;
					right.color=rgt;
					farRight.color=frt;
					straight.color=str;                     
					Dist.color = dst;					
				}
			}
		}
	}

//Functions
//Short Range Radar Routine
IEnumerator ShortRangeRoutine() 					
	{
		Color col = image.color;
		Color abv = above.color;
		Color blw = below.color;
		ShortRadarFired = true;
		radarBlip1Active = true;

		yield return new WaitForSeconds (10);

		subRes.setEnergyLevel (-10.0f);
		col.a = 0f;
		abv.a = 0f;
		blw.a = 0f;
		image.color = col;
		above.color = abv;
		below.color = blw;
		radarBlip1Active = false;
		ShortRadarFired = false;		
	}
//Long Range Radar Routine
IEnumerator LongRangeRoutine(Image Indicator, float dist, Text Distance)
	{
		if (fired = false) 
			{fired=true;}

		Color idc = Indicator.color;
        Color dst = Distance.color;

        float alph = 1.0f - ((dist - radarRange) / (200 - radarRange));
        if (alph <= 0)
        	{alph = 0.3f;}

		idc.a = alph;
        dst.a = 255f;
		Indicator.color = idc;
        Distance.color = dst;

		yield return null;
	}
//Makes Sure Long Range Radar Fires for 10 Seconds    
	IEnumerator Recharge()
	{

		LongRadarFired = true;
		lt[Location].intensity = amplitude;
		yield return new WaitForSeconds (10);
		lt[Location].intensity = 0f;
		subRes.setEnergyLevel (-8.0f);
		LongRadarFired = false;	
	}
//Function That Lights up the Indicators on the Long Range Radar	    
public void Lights(Color fDirection, Color sDirection, Color tDirection, Color qDirection, Image fAssign, Image sAssign, Image tAssign, Image qAssign) 
    {
        fDirection.a = 0f;
        sDirection.a = 0f;
        tDirection.a = 0f;
        qDirection.a = 0f;

        fAssign.color = fDirection;
        sAssign.color = sDirection;
        tAssign.color = tDirection;
        qAssign.color = qDirection;
    }

	public void CheckLocation()

	{
		return;
	}
	
}
	