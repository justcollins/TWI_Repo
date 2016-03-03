using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SuperRadar4 : MonoBehaviour {


	public Canvas canvas;
	public GameObject blipCanvas;
	public GameObject radarRotation;
	public float RotationSpeed;
	public GameObject heightAbove;
	public GameObject heightBelow;
	public GameObject LongRangeRadar;
	public GameObject FarLeftSp;
	public GameObject LeftSp;
	public GameObject StraightSp;
	public GameObject RightSp;
	public GameObject FarRightSp;
    private KeyboardManager keyboard;

	Image image;
	Image above;
	Image below;
	Image farLeft;
	Image left;
	Image straight;
	Image right;
	Image farRight;

	public float radarZoom;
	float radarRange = 35f;


	public bool   radarCenterActive;
	public string radarCenterTag;

	public bool   radarBlip1Active = false;
	public string radarBlip1Tag;
	

	private GameObject _BlipObject;
	private GameObject _centerObject;
	private float        _radarWidth;
	private Vector2    _radarCenter;
	private Texture2D  _radarCenterTexture;

	bool ShortRadarFired = false;
	bool LongRadarFired = false;
	bool ShortRadarActive = true;
	bool LongRadarActive = false;


	void Start ()
	{


        keyboard = FindObjectOfType<KeyboardManager>();
		RectTransform CanvasTransform = canvas.GetComponent<RectTransform> ();

		float radarWidth = CanvasTransform.rect.width;

		_radarWidth = radarWidth;



	}
	void Update()
	{

		GameObject[] gos;
		GameObject[] mother;

		
		gos = GameObject.FindGameObjectsWithTag (radarCenterTag);
		mother = GameObject.FindGameObjectsWithTag (radarBlip1Tag); 

		_centerObject = gos[0];
		_BlipObject = mother[0];

		RectTransform blip = blipCanvas.GetComponent<RectTransform> ();

		image = blipCanvas.GetComponent<Image> ();
		above = heightAbove.GetComponent<Image> ();
		below = heightBelow.GetComponent<Image> ();
		farLeft = FarLeftSp.GetComponent<Image> ();
		left = LeftSp.GetComponent<Image> ();
		straight = StraightSp.GetComponent<Image> ();
		right = RightSp.GetComponent<Image> ();
		farRight = FarRightSp.GetComponent<Image> ();


		Color col = image.color;
		Color abv = above.color;
		Color blw = below.color;
		Color flt = farLeft.color;
		Color lft = left.color;
		Color str = straight.color;
		Color rgt = right.color;
		Color frt = farRight.color;

		
		if (_centerObject) 
		{

			Vector2 extPos = blip.localPosition;
			Vector3 centerPos = _centerObject.transform.position;
			Vector3 virusPos = _BlipObject.transform.position;

			float dist = Vector3.Distance (centerPos, virusPos);

			float bX = virusPos.x - centerPos.x;
			float bZ = virusPos.z - centerPos.z;
			float bY = virusPos.y - centerPos.y;

			bX = bX * radarZoom;
			bZ = bZ * radarZoom;

			radarRotation.transform.Rotate(0,0, (-1)*RotationSpeed * Time.deltaTime);

            if (Input.GetKeyDown(keyboard.LeftMouse) && ShortRadarFired == false && ShortRadarActive == true)
			{
				StartCoroutine (ShortRangeRoutine ());
			}
            else if (Input.GetKeyDown(keyboard.LeftMouse) && LongRadarFired == false && LongRadarActive == true)
			{
				StartCoroutine (Recharge ());			
			}

            if (Input.GetKeyDown(keyboard.Scanner) && ShortRadarActive == true)
			{
				LongRadarActive= true;
				ShortRadarActive= false;

				transform.SetSiblingIndex (0);

			}
			else if (Input.GetKeyDown(keyboard.Scanner) && LongRadarActive == true)
			{
				ShortRadarActive= true;
				LongRadarActive= false;

				transform.SetSiblingIndex (1);

			}



			if (dist <= (_radarWidth - 3) * 0.5 / radarZoom && radarBlip1Active==true && ShortRadarActive == true)
				{
					if( bX < radarRange && bX > (radarRange * (-1)) && bZ < radarRange && bZ > (radarRange * (-1)))

				   { 	
						extPos.x = bX;
						extPos.y = bZ;

						blip.localPosition= extPos;

						col.a =255f;
						image.color = col;

						if (bY>0)
						{
							abv.a = 255f;
							above.color = abv;
						}
						else if(bY<0)
						{
							blw.a = 255f;
							below.color = blw;
						}
					}

					else 

					{					
					col.a =0f;
					abv.a = 0f;
					blw.a = 0f;
					
					image.color = col;
					above.color = abv;
					below.color = blw;
					}

				}

			if (dist >= radarRange && LongRadarActive ==true)
			{
			
				float dX = centerPos.x - virusPos.x; 
				float dZ = centerPos.z - virusPos.z;

				float deltay = Mathf.Atan2(dX, dZ) * Mathf.Rad2Deg - 270 - _centerObject.transform.eulerAngles.y;

				if (LongRadarFired)
				{

					if(deltay < -440f && deltay> -450f){

						lft.a=0f;
						flt.a=0f;
						rgt.a=0f;
						frt.a=0f;
						farLeft.color= flt;
						left.color=lft;
						right.color=rgt;
						farRight.color=frt;

						StartCoroutine (LongRangeRoutine(straight, dist));					
					}
					else if(deltay < -90 && deltay> -100){

						lft.a=0f;
						flt.a=0f;
						rgt.a=0f;
						frt.a=0f;
						farLeft.color= flt;
						left.color=lft;
						right.color=rgt;
						farRight.color=frt;
						
						StartCoroutine (LongRangeRoutine(straight, dist));		
					}
					else if(deltay < -100 && deltay> -110){
						str.a=0f;
						flt.a=0f;
						rgt.a=0f;
						frt.a=0f;
						farLeft.color= flt;
						right.color=rgt;
						farRight.color=frt;
						straight.color=str;
						
						StartCoroutine (LongRangeRoutine(left, dist));		
					}
					else if(deltay < -110 && deltay> -120){

						lft.a=0f;
						str.a=0f;
						rgt.a=0f;
						frt.a=0f;
						left.color=lft;
						right.color=rgt;
						farRight.color=frt;
						straight.color=str;

						StartCoroutine (LongRangeRoutine(farLeft, dist));		
					}
					else if(deltay < -430 && deltay> -440){

						flt.a=0f;
						lft.a=0f;
						str.a=0f;
						frt.a=0f;
						left.color=lft;
						farRight.color=frt;
						straight.color=str;
						farLeft.color= flt;
						
						StartCoroutine (LongRangeRoutine(right, dist));		
					}
					else if(deltay < -420 && deltay> -430){

						rgt.a=0f;
						flt.a=0f;
						lft.a=0f;
						str.a=0f;
						left.color=lft;
						straight.color=str;
						farLeft.color= flt;
						right.color=rgt;

						StartCoroutine (LongRangeRoutine(farRight, dist));		
					}
					else
					{
						lft.a=0f;
						flt.a=0f;
						rgt.a=0f;
						frt.a=0f;
						str.a=0f;
						farLeft.color= flt;
						left.color=lft;
						right.color=rgt;
						farRight.color=frt;
						straight.color=str;

					}
				}
			}
		}
	}

	IEnumerator ShortRangeRoutine()
	{
		Color col = image.color;
		Color abv = above.color;
		Color blw = below.color;

		ShortRadarFired = true;
		radarBlip1Active = true;

		yield return new WaitForSeconds (10);

		col.a = 0f;
		abv.a = 0f;
		blw.a = 0f;

		image.color = col;
		above.color = abv;
		below.color = blw;

		radarBlip1Active = false;
		ShortRadarFired = false;
		
	}
	IEnumerator LongRangeRoutine(Image Indicator, float dist)
	{
		Color idc = Indicator.color;

		float alph = 0.1f+(dist/((dist/10f)*(dist- Mathf.Sqrt(dist))));
		idc.a = alph;
		Indicator.color = idc;

		yield return new WaitForSeconds (10);

		idc.a = 0f;
		Indicator.color = idc;
	}
	IEnumerator Recharge()
	{
		LongRadarFired = true;

		yield return new WaitForSeconds (10);

		LongRadarFired = false;

		
	}
}
	