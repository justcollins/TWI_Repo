using UnityEngine;
using System.Collections;

public class SubControl_V2 : MonoBehaviour {

	private Submarine_Resources subRes;							//Sub Resource Scrpit Reference
	private KeyboardManager keyboard;							//Keyboard Input Scrpit Reference
	private Rigidbody rb;										//Rigidbody Reference
	
	private float VerMoveScroll;								//Mouse Scroll Variable
	private float thrust;										//Forward Ship Thrust
	public float speed;        									//Thrust Increase Rate
	public float breaks;										//Breaking Rate
	
	public GameObject exteriorLights;							//Exterior Cockpit Lights
	public GameObject interiorLights;							//Interior Cockpit Lights

	public float maxThrustValue;								//Max Acceleration
	private float minThrustValue= 0f;       					//Min Acceleration
	public float TopSpeed;										//Top Speed Cockpit Can Go
	private float MaxSpeed;										//Top Speed Variable

	public float boostVal;										//Boost Value
	private float boost;										//Boost Variable
	private int boostCostTime;									//How Much Time Unitl the Player Has to Pay for Boost Again
	private bool boosting=false;								//Are We Boosting
	private float BoostTopSpeed;								//Top Speed While We Boost

	public bool isEngineOn = false;							//Checks if Engine is On
	private bool Back = false;									//Checks if We are In Reverse

	private Vector3 ShipVelocity;								//Tracks Ships Velocity (Vector x,y,z)
	private float ShipDirection;								//Tracks Ships Direction(float z)
	private bool Strafing = false;								//Strafe Check
	[Range(0f,0.1f)]											//Range Slider for Strafe Speed
	public float StrafeSpeed=0.01f;								//Strafe Speed

	private int dir;											//Changes Ships Direction
	private bool TurnCheck = false;
	public bool Torque;
	public bool rotation;
	public Transform com;


	void Awake (){
		subRes = FindObjectOfType<Submarine_Resources>();		//Sub Res Link
		rb = GetComponent<Rigidbody>();							//Rigidbody Link
		keyboard = FindObjectOfType<KeyboardManager>();			//Keyboard Link
	}
// Use this for initialization
void Start () {
		exteriorLights.SetActive(false);						//Turns Off Exterior Lights
		interiorLights.SetActive(false);						//Turns Off Interior Lights
		MaxSpeed = TopSpeed;									//Set Max Speed to be Top Speed
		BoostTopSpeed = TopSpeed + 2;							//Make Boost Top Speed Greater than Top Speed
		boost = boostVal;										//Reset Boost
		rb.centerOfMass = com.transform.position;
	}
	void FixedUpdate(){

		if (Torque) {
			Turning ();
			//NormalizeTurning ();
		} else if (rotation)
		{
			Rotating();
		}
		//Ricardo ();
	}	
// Update is called once per frame
void Update () {

		ShipVelocity=rb.GetRelativePointVelocity (transform.position);				//Gets Ships Velocity (Relative to the Ship)
		ShipDirection = ShipVelocity.z;												//Gets Ship Direation (z axis)
		VerMoveScroll = Input.GetAxis("Mouse ScrollWheel");							//Mouse Scroll Listener
																	
	
		if (Input.GetKeyDown (keyboard.EngineOn) && subRes.getShipEnergy () > 0) 	//If We Press Tab and Have More Than 0 Energy
		{isEngineOn = !isEngineOn;}													//Turn Engine On

		if (isEngineOn) { 															//If Engine is On
			ForwardMovement ();														//Forward Movement Function Call
			StrafeMovement();														//Strafe Movement Function Call
			Boost ();																//Boost Function Call
			if (Input.GetKeyDown (keyboard.LeftMouse)) { 							//Middle Mouse Click Listener keyboard.MiddleMouse
				Back = !Back;
				thrust = 0;
			}																		//Forward / Reverse
		} 
		else if (isEngineOn == false) 																			//If Engine is Off
		{	thrust=0;																							//Set Thrust to 0
			Normalize();
			if (ShipDirection < 0f)																				//If We are Moving Backwards
			{rb.AddRelativeForce (transform.forward * Time.fixedDeltaTime * breaks, ForceMode.Acceleration);}	//Slowly Come to a Halt
			if (ShipDirection > 0f)																				//If We are Moving Forwards
			{rb.AddRelativeForce (-transform.forward * Time.fixedDeltaTime * breaks, ForceMode.Acceleration);}	//Slowly Come to a Halt
		}

		if (subRes.getShipEnergy() > 0)												//If We Have More than 0 Energy
		{
			if (Input.GetKeyDown(keyboard.ExteriorLights))							//And We Press the Lights Button
				{exteriorLights.SetActive(!exteriorLights.activeSelf);}				//Turn On/Off Exterior Lights
			
			if (Input.GetKeyDown(keyboard.InteriorLights))
				{interiorLights.SetActive(!interiorLights.activeSelf);}				//Turn On/Off Interior Lights
		}
		else if (subRes.getShipEnergy() <= 0)										//If We Have Less than 0 Energy
				{isEngineOn = false;}												//Turn the Engine Off
	}

	
public void ForwardMovement()																					//Forward Movement Function
	{
		if (Back == false) {																					//If We Are Not In Reverse																									
			dir= 1;																								//Direction is Forward
			Moving (dir);																						//Movement Function
			if (ShipDirection < 0f)																				//If We are Moving Backwards
			{rb.AddRelativeForce (transform.forward * Time.fixedDeltaTime * breaks, ForceMode.Acceleration);}	//Slowly Come to a Halt
		} 
		else if (Back == true){																					//If We Are In Reverse																									
			dir= -1;																							//Direction is Backwards
			Moving (dir);																						//Movement Function
			if (ShipDirection > 0f)																				//If We are Moving Forwards
			{rb.AddRelativeForce (-transform.forward * Time.fixedDeltaTime * breaks, ForceMode.Acceleration);}	//Slowly Come to a Halt
		}

	}

public void Moving (int dir)																							//Basic Movement Function
	{
		if (VerMoveScroll > 0f) { 																						//If Mouse is Scrolled
			if (thrust <= maxThrustValue) 																				//If Thurst is Less than Max Thrust Value																											
				{thrust += speed;}																						//Add Speed to Thrust			 		
			else if (thrust > maxThrustValue && boosting == false)														//If Thrust is Greater than Max		
				{thrust = maxThrustValue;}																				//Set to Maximum Value	

			if (ShipVelocity.magnitude < MaxSpeed)
				{rb.AddRelativeForce (dir * transform.forward * Time.fixedDeltaTime * thrust, ForceMode.Acceleration);}	//Add Forward Force According to Thrust 						
		}
	}

public void Boost ()																									//Boost Function
	{
		if (Input.GetKey (keyboard.Boost)) {																			//If Left Control is Pushed
			boosting = true;																							//Start the Boost
			thrust = thrust + boost;																					//Add Boost to Thrust
			MaxSpeed = BoostTopSpeed;																					//Increase Top Speed
				if (thrust > maxThrustValue)																			//If We Get to Max Thrust, Boost Only Once		
					{boost = 0;}																						
			rb.AddRelativeForce (dir * transform.forward * Time.fixedDeltaTime * thrust, ForceMode.Acceleration);		//Apply Boost		
	
		} else if (Input.GetKeyUp (keyboard.Boost)) 																	//If We Let Go, Never Let Go Jack, Never Let Go....
		{
			boost= boostVal;																							//Boost Reset
			boosting = false;																							//Boosting Off
			MaxSpeed = TopSpeed;																						//Top Speed Reset
		}
	}

	public void StrafeMovement()																//Strafe Movement Function
	{
		if (Input.GetKey (keyboard.RightMouse)) {												//If Right Mouse Button is Pressed
			Strafing = true;																	//Strafing is On
			if (Input.GetKey (keyboard.Up))     												//If We Hold a Button
				{rb.AddRelativeForce (transform.up *StrafeSpeed, ForceMode.VelocityChange);}	//We Go Up Up Up					
			if (Input.GetKey (keyboard.Down)) 
				{rb.AddRelativeForce (-transform.up *StrafeSpeed, ForceMode.VelocityChange);}	//We Go Down Down Down					
			if (Input.GetKey (keyboard.Left))  
				{rb.AddRelativeForce (-transform.right *StrafeSpeed, ForceMode.VelocityChange);}//We Go Left Left Left					
			if (Input.GetKey (keyboard.Right)) 
				{rb.AddRelativeForce (transform.right *StrafeSpeed, ForceMode.VelocityChange);}	//We Go Right Right Right						
		} else 
		{Strafing = false; Normalize();}														//If We Stop Strafing, Slowly Normalize the Ship
	}

public void Normalize()																							//Normalizing the Ship Function
	{
		if (Strafing == false) 																					//If Strafing is Off
		{
			if (ShipVelocity.x > 0)																				//And the Ship is Still Moving to the Right
			{rb.AddRelativeForce (-transform.right * Time.fixedDeltaTime * breaks, ForceMode.Acceleration);}	//Slowly Stop the Ship
			else if (ShipVelocity.x < 0)																		//And the Ship is Still Moving to the Left
			{rb.AddRelativeForce (transform.right * Time.fixedDeltaTime * breaks, ForceMode.Acceleration);}		//Slowly Stop the Ship
			if (ShipVelocity.y > 0)																				//And the Ship is Still Going Up
			{rb.AddRelativeForce (-transform.up * Time.fixedDeltaTime * breaks, ForceMode.Acceleration);}		//Slowly Stop the Ship
			else if (ShipVelocity.y < 0)																		//And the Ship is Still Going Down
			{rb.AddRelativeForce (transform.up * Time.fixedDeltaTime * breaks, ForceMode.Acceleration);}		//Slowly Stop the Ship
		}
	}
public void Turning()																							//Turning Function
	{
		if (Strafing == false) {
			if (Input.GetKey (keyboard.Up)) {     																		//If We Hold a Button
				rb.AddRelativeTorque (-transform.right * StrafeSpeed, ForceMode.VelocityChange);
				TurnCheck = true;
				ZeroZ ();
				//ZeroV ();
			}	//We Turn Up Up Up
			if (Input.GetKey (keyboard.Down)) {
				rb.AddRelativeTorque (transform.right * StrafeSpeed, ForceMode.VelocityChange);
				TurnCheck = true;
				ZeroZ ();
				//ZeroV ();
			}	//We Turn Down Down Down					
			if (Input.GetKey (keyboard.Left)) {
				rb.AddRelativeTorque (-transform.up * StrafeSpeed, ForceMode.VelocityChange);
				TurnCheck = true;
				ZeroZ ();
				//ZeroV ();
			}		//We Turn Left Left Left					
			if (Input.GetKey (keyboard.Right)) {
				rb.AddRelativeTorque (transform.up * StrafeSpeed, ForceMode.VelocityChange);
				;
				TurnCheck = true;
				ZeroZ ();
				//ZeroV ();
			}		//We Turn Right Right Right	
			if (Input.GetKeyUp (keyboard.Up) || Input.GetKeyUp (keyboard.Down) || Input.GetKeyUp (keyboard.Left) || Input.GetKeyUp (keyboard.Right)) {
				TurnCheck = false;
			}
		}
	}
	public void Rotating() 
	{
		if (Input.GetKey (keyboard.Up)) {     																		//If We Hold a Button
			transform.Rotate (-Vector3.right * Time.deltaTime * 30);
			TurnCheck = true;
		}
			if (Input.GetKey (keyboard.Down)) {
				transform.Rotate (Vector3.right * Time.deltaTime * 30);
				TurnCheck = true;
			}	//We Turn Down Down Down					
			if (Input.GetKey (keyboard.Left)) {
				transform.Rotate (-Vector3.up * Time.deltaTime * 30);
				TurnCheck = true;
			}		//We Turn Left Left Left					
			if (Input.GetKey (keyboard.Right)) {
				transform.Rotate (Vector3.up * Time.deltaTime * 30);
				TurnCheck = true;
			}		//We Turn Right Right Right	
			if (Input.GetKeyUp (keyboard.Up) || Input.GetKeyUp (keyboard.Down) || Input.GetKeyUp (keyboard.Left) || Input.GetKeyUp (keyboard.Right)) {
				TurnCheck = false;
			}
		}

public void NormalizeTurning()
	{
		if (TurnCheck == false)
		{
			Vector3 temp = rb.angularVelocity;
			temp.x = 0;
			temp.y = 0;
			temp.z = 0;
			rb.angularVelocity = temp;
		}
	}
public void ZeroZ()
	{
		Vector3 temp = rb.angularVelocity;
		temp.z = 0;
		rb.angularVelocity = temp;
	}
	public void ZeroV()
	{
		Vector3 temp = rb.velocity;
		rb.velocity = Vector3.zero;
		rb.velocity = temp;
	}
}
