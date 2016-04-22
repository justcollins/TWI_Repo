using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Radar : MonoBehaviour {


	public Canvas canvas;

    public Text TotDist;

    public GameObject radarCanvas;
    public GameObject Ship;
	public GameObject blipCanvas;
	public GameObject radarRotation;
	public GameObject heightAbove;
	public GameObject heightBelow;
	public GameObject LongRangeRadar;
	public GameObject FarLeftSp;
	public GameObject LeftSp;
	public GameObject StraightSp;
	public GameObject RightSp;
	public GameObject FarRightSp;

    private KeyboardManager keyboard;
	public Submarine_Resources subRes;


    private Image image;
    private Image above;
    private Image below;
    private Image farLeft;
    private Image left;
    private Image straight;
    private Image right;
    private Image farRight;

    private Text Dist;

    
	public float radarZoom;
    public float RotationSpeed;

	public float radarRange = 45f;
    float checkValue;
    float bX;
    float bZ;
    float bY;
	bool fired=false;

	public bool   radarCenterActive;
	public string radarCenterTag;

	public bool   radarBlip1Active = false;
	public string radarBlip1Tag;
	

	private GameObject _BlipObject;
	private GameObject _centerObject;
	private float        _radarWidth;
	private Vector2    _radarCenter;
	private Texture2D  _radarCenterTexture;

    private bool ShortRadarFired = false;
    private bool LongRadarFired = false;
    private bool ShortRadarActive = true;
    private bool LongRadarActive = false;

	private int Location=0;
	public float duration = 1.0F;
	public Light[] lt = new Light[15];

	void Start ()
	{


        keyboard = FindObjectOfType<KeyboardManager>();
		RectTransform CanvasTransform = canvas.GetComponent<RectTransform> ();
     
		float radarWidth = CanvasTransform.rect.width;

		_radarWidth = radarWidth;

		lt = GetComponent<Light>();
	

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
        Dist = TotDist.GetComponent<Text>();


		Color col = image.color;
		Color abv = above.color;
		Color blw = below.color;
		Color flt = farLeft.color;
		Color lft = left.color;
		Color str = straight.color;
		Color rgt = right.color;
		Color frt = farRight.color;
        Color dst = Dist.color;
		
		if (_centerObject) 
		{
            
			Vector2 extPos = blip.localPosition;
			Vector3 centerPos = _centerObject.transform.position;
			Vector3 virusPos = _BlipObject.transform.position;

            radarCanvas.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, Ship.transform.eulerAngles.y);
            blipCanvas.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, (-1) *Ship.transform.eulerAngles.y);
                                      
            
		    float dist = Vector3.Distance (centerPos, virusPos);


			bX = virusPos.x - centerPos.x ;
			bZ = virusPos.z - centerPos.z;    
			bY = virusPos.y - centerPos.y;

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

				LongRangeRadar.transform.SetSiblingIndex (2);

			}
			else if (Input.GetKeyDown(keyboard.Scanner) && LongRadarActive == true)
			{
				ShortRadarActive= true;
				LongRadarActive= false;

				transform.SetSiblingIndex (2);

			}



			if (dist <= (_radarWidth - 4) * 0.5 / radarZoom && radarBlip1Active==true && ShortRadarActive == true)
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
                            blw.a = 0f;
                            below.color = blw;
						}
						else if(bY<0)
						{
							blw.a = 255f;
							below.color = blw;
                            abv.a = 0f;
                            above.color = abv;
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
				//float phi = Time.time / duration * 2 * Mathf.PI;
				//float amplitude = Mathf.Cos(phi) * 0.5F + 0.5F;

				float dX = centerPos.x - virusPos.x; 
				float dZ = centerPos.z - virusPos.z;

				float deltay = Mathf.Atan2(dX, dZ) * Mathf.Rad2Deg - _centerObject.transform.eulerAngles.y;

                if (LongRadarFired)
				{

                    dst.a = 255;
                    Dist.color = dst;
                    TotDist.text = dist.ToString("F2");

					float amplitude = 1.0f;
					lt.intensity = amplitude;
                        
					if(deltay < -170f && deltay> -190f){

                        Lights(lft, flt, rgt, frt, farLeft, left, right, farRight);

						StartCoroutine (LongRangeRoutine(straight, dist,Dist));					
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

					else
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

				else
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
	IEnumerator LongRangeRoutine(Image Indicator, float dist, Text Distance)
	{
		if (fired = false) {
			fired=true;
		}
		Color idc = Indicator.color;
        Color dst = Distance.color;

        float alph = 1.0f - ((dist - radarRange) / (200 - radarRange));
        if (alph <= 0)
        {
            alph = 0.3f;
        }
		idc.a = alph;
        dst.a = 255f;
		Indicator.color = idc;
        Distance.color = dst;

		yield return null;

	}
    
        IEnumerator Recharge()
	{
		LongRadarFired = true;
	
		yield return new WaitForSeconds (10);
		subRes.setEnergyLevel (-8.0f);

		LongRadarFired = false;	

	}
    
    public void DegreeCheck(int degreeValueMin, int degreeValueMax , float shipRotation, float bX, float bZ, float checkValue)
    {
        if (shipRotation > degreeValueMin && shipRotation < degreeValueMax)
        {
            bX = bX + checkValue;
            bZ = bZ + checkValue;
        }
    }
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
	