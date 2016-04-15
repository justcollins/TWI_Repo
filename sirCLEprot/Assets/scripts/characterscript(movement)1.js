#pragma strict

var rotationSpeed : float = 10;
var walkSpeed : float = 7;
var gravity : float = 50;
function Update() {
	var Controller : CharacterController = GetComponent(CharacterController);
	var vertical : Vector3 = transform.TransformDirection(Vector3.forward);
	var horizontal : Vector3 = transform.TransformDirection(Vector3.right);
	
	 if(Input.GetAxis("Vertical") || Input.GetAxis("Horizontal")){
	 Controller.Move((vertical * (walkSpeed * Input.GetAxis("Vertical"))) * Time.deltaTime);
	  Controller.Move((horizontal * (walkSpeed * Input.GetAxis("Horizontal"))) * Time.deltaTime);
	 } 
}