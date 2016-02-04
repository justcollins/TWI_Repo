using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NavMeshAgent))]

/**
 *   Enemy Movement Class
 *   27 Jan 2016
 *   Jose Pascua
 * 
 *   Basic movement
 */

public class EnemyMovement : MonoBehaviour {

	public enum CurrentBehavior {
		Patroling = 0,
		Seeking = 1
	};

	[Header("Basic Options")]
	public float movementSpeed;

	[Header("Targets and Points")]
	public Transform[] patrolPoints;
	public bool startAtFirstPoint = false;
	public string playerTag = "Player";

	[Header("Behavior")]
	public CurrentBehavior currentBehavior;
	public EnemyVerticalMovement verticalMovement;

	private Transform target;
	private NavMeshAgent nma;
	private int currPoint;

	void Awake() {
		nma = GetComponent<NavMeshAgent>();
	} // end of Awake()

	void Start() {
		if (startAtFirstPoint) { transform.position = patrolPoints[0].position; }
		currPoint = 0;
		target = (GameObject.FindGameObjectWithTag(playerTag)).transform;
		nma.speed = movementSpeed;
	} // end of Start()

	void Update() {
		switch ( (int)currentBehavior ) {
		case 0:
			Patrol ();
			break;
		case 1:
			Seek ();
			break;
		}

		if (verticalMovement != null) {
			if (verticalMovement.GetAdjustStop()) {
				nma.Stop();
			} else {
				nma.Resume ();
			}
		}
	}

	void Patrol() {
		nma.speed = movementSpeed;
		if (transform.position.x!=patrolPoints[currPoint].position.x) {
			nma.SetDestination(patrolPoints[currPoint].position);
			if (verticalMovement != null) {
				verticalMovement.SetTarget( patrolPoints[currPoint] ); 
			}
		} else {
			currPoint = ((currPoint+1)%(patrolPoints.Length));
		}
	}

	void Seek () {
		nma.speed = movementSpeed;
		nma.SetDestination(target.position);
		if (verticalMovement != null) {
			verticalMovement.SetTarget( target ); 
		}
	}
}

/// <comment>
/// by Jose Pascua
/// Basic enemy movement
/// </comment>