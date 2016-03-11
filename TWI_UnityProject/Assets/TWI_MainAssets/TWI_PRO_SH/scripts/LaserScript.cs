using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/*
 Class specifically for the laser weapon
 2/17/16
 Sarah Ho
 script for firing the laser weapons. It will show a laser texture image, collide/hit other objects, and light up
 */


public class LaserScript : MonoBehaviour 
{
	LineRenderer line;
	Light light;
	Image red;
    CursorLockMode lockCursor;

	float timer= 0f;// Andrew New
	float elapsedTime=0f;
	bool check= true;

	public GameObject bg;
	public GameObject WarningLight;
	public GameObject Text;
	public bool useLight = false;

	private KeyboardManager keyboard;
	private EngineMonitor EM;


	[Range(0f, 10f)] public float damageDone;

	void Start () 
	{
		
		red = WarningLight.GetComponent<Image> ();
        EM = GameObject.FindObjectOfType<EngineMonitor>();
        keyboard = FindObjectOfType<KeyboardManager>();
		line = gameObject.GetComponent<LineRenderer>();

		//the pink square is gone at end of line
		line.enabled = false; 

		if (useLight) 
		{
			light = gameObject.GetComponent<Light> ();
			light.enabled = false;
		}

        lockCursor = CursorLockMode.Locked;
        Cursor.visible = false;
	}

	void Update () 
	{
		if (EM.getLaserState () == 1) 
		{
			bg.transform.SetSiblingIndex (2);
		}

		//GetButtonDown means when the button is clicked once
		if (EM.getLaserState () == 1 && Input.GetKeyDown (keyboard.LeftMouse)) 
		{
			check = false;

			//this is a fail-safe (but coroutine should auto stop)
			StopCoroutine ("FireLaser"); 
			StopCoroutine ("Overheat");
			StartCoroutine ("FireLaser");

		} 
		else if (check== true)
		{
			elapsedTime += Time.deltaTime;
		}
	}

	//coroutine needed to keep laser on
	IEnumerator FireLaser() 
	{
		//make the line visible
		line.enabled = true; 
		if (useLight) {light.enabled = true; }

		timer = timer - elapsedTime;

		if (timer < 0) 
		{
			timer = 0f;
		} 
		else if (timer > 0 && timer < 6) 
		{
			WarningLight.transform.localScale = new Vector3 (0.3F, 0.3F, 0);
			red.color= Color.green;
		} 
		else if (timer > 6 && timer < 9) 
		{
			WarningLight.transform.localScale = new Vector3 (0.6F, 0.6F, 0);
			red.color= Color.yellow;
		}
		else if (timer > 9) 
		{
			WarningLight.transform.localScale = new Vector3 (1F, 1F, 0);
			red.color= Color.red;			
		}

		//GetButton down means while the button is pushed
		while(Input.GetButton ("Fire")) 
		{
			elapsedTime=0;
			timer+=Time.deltaTime;

            Ray ray = new Ray(transform.position, transform.forward); //.position gets the position of the gun, .forward means wherever the x-axis is facing
			RaycastHit hit; //so laser will stop if it collides w/something

			line.SetPosition (0,ray.origin); //line starts at the tip of gun

			//this if-statment determines if the ray hits an object
            if (Physics.Raycast(ray, out hit, 100)) //if it does, make the laser ray STOP at the point of collision
			{
				line.SetPosition(1, hit.point);
				if(hit.rigidbody) //it'll automatically be False and NOT go thro this statment if it doesn't detect a rigidbody
				{
					//hit the obj w/rigidbody with 5 units of force AT obj's arm,head,feet, etc
					hit.rigidbody.AddForceAtPosition(transform.forward * 5, hit.point);
				}

				TouchEnemies (hit);
			}
			else//if not, let the laser ray go on until it reaches the limit we put 
			{
				line.SetPosition (1,ray.GetPoint (100)); //line ends at 100 units from gun tip
			}


			yield return null; //every frame it'll keep looping itself while button is pushed

		}

		check = true;
		line.enabled = false; //once unpushed, make the line invisible again
		if (useLight) { light.enabled = false; }

	}



	void TouchEnemies(RaycastHit _h) {
		if (_h.transform.tag == "enemy") {
			EnemyHealth eh;
			eh = _h.transform.gameObject.GetComponent<EnemyHealth>();

			if (eh) {
				switch((int)eh.type) {
					case 1: //tagger
					BoidFlocking bf;
					bf = eh.GetComponent<BoidFlocking>();
					bf.DestroyMe();
					break;

					case 2: //macrophage
					eh.AddHealth(-damageDone);
					break;
				}
			}
		}
	}
}
