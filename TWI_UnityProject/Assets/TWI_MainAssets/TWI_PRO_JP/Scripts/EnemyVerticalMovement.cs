using UnityEngine;
using System.Collections;

/**
 *   Enemy Movement Class
 *   27 Jan 2016
 *   Jose Pascua
 * 
 *   This handles vertical movement since
 *   NavMeshAgent only handles X and Z
 */

public class EnemyVerticalMovement : MonoBehaviour {

	private float myYPos;
	private float targetYPos;
	private Transform target;

	[Header("Basic Options")]
	public float ySpeed = 3f;

	[Header("Obstacle Avoidance")]
	public float avoidDistance = 2f;
	public LayerMask avoidLayer;
	[Range(0f, 10f)]public float avoidTargetOffset = 5f;
	private bool stopToAdjust = false;
	private Vector3 chk_fwd, chk_up, chk_down;
	private bool disableTYP;
	private int TYPtimesup = 0;
	public int checkTime = 50;

	void Start () {
	}

	void Update () {
		if (target != null) {
			//get y value of the current position
			myYPos = transform.position.y;

			if (myYPos != targetYPos) {
				transform.position = new Vector3 (transform.position.x, Mathf.Lerp ( myYPos, targetYPos, ySpeed*Time.deltaTime ), transform.position.z);
			}
		}

		ObjectAvoidanceForward();
	}

	void FixedUpdate() {
		if (TYPtimesup != 0) {
			TYPtimesup -= 1;
			if (TYPtimesup == 1) {
				disableTYP = false;
			}
		}
	}

	#region SetsAndGets
	public void SetTarget(Transform _t) {
		target = _t;
		if (!disableTYP) {
			targetYPos = target.position.y; }
	}

	public Transform GetTarget() {
		return target;
	}

	public bool GetAdjustStop() {
		return stopToAdjust;
	}
	#endregion

	void ObjectAvoidanceForward() {

		chk_fwd = transform.TransformDirection(Vector3.forward) * avoidDistance;
		RaycastHit hit;

		if ( Physics.Raycast ( transform.position, chk_fwd, out hit, avoidDistance, avoidLayer ) ) {
			//Debug.Log ("Hit Something Forward");
			stopToAdjust = true;
			ObjectAvoidanceVertical();
		}  else {
			stopToAdjust = false;
		}
	}

	void ObjectAvoidanceVertical() {
		chk_up = transform.TransformDirection(Vector3.up) * avoidDistance;
		chk_down = transform.TransformDirection(Vector3.down) * avoidDistance;

		RaycastHit hit;
		bool upcheck = ( Physics.Raycast ( transform.position, chk_up, out hit, avoidDistance, avoidLayer ) );
		bool downcheck = ( Physics.Raycast ( transform.position, chk_down, out hit, avoidDistance, avoidLayer ) );
		//targetYPos += Random.Range ( -1f, 1f );

		if ( upcheck == downcheck ) {
		} else {
			if ( upcheck ){
				targetYPos = target.position.y - avoidTargetOffset;
				DisableTargetYPosChange();
			}
			if ( downcheck ){
				targetYPos = target.position.y + avoidTargetOffset;
				DisableTargetYPosChange();
			}
		}
	}

	void DisableTargetYPosChange() {
		disableTYP = true;
		TYPtimesup = checkTime;
	}
}

/// <comment>
/// by Jose Pascua
/// Handles vertical movement of simpler enemies
/// </comment>