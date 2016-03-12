using UnityEngine;
using System.Collections;

/**
 *   Enemy Sight Class
 *   10 Feb 2016
 *   Jose Pascua
 * 
 *   Finding targets
 */

public class EnemySight : MonoBehaviour {
	
	public EnemyType type;
	private EnemyMovement em;
	private BoidController bc;
	
	private Transform nextWaypoint;
	private Transform tnextWaypoint = null;
	private bool teleporting = false;
	private float initialRadius;
	
	public bool wallDetection;
	public LayerMask wallMask;
	
	void Awake () {
		switch ((int)type) {
		case 1: //tagger;
			TaggerAwake();
			break;
			
		case 2: //MacrophageAwake
			MacrophageAwake();
			break;
		}
	}
	
	void Update () {
		switch ((int)type) {
		case 1: //tagger;
			TaggerUpdate();
			break;
			
		case 2: //macrophage
			MacrophageUpdate();
			break;
		}
	}
	
	private void MacrophageAwake() {
		em = GetComponent<EnemyMovement> ();
		nextWaypoint = em.chasee;
	}
	
	private void TaggerAwake() {
		bc = GetComponent<BoidController> ();
		initialRadius = bc.centerRadius;
	}
	
	private void MacrophageUpdate() {
		if (ShipVisibility.GetTagged ()) {
			
			// checks to see if there is a wall in the way
			if (wallDetection) {
				Vector3 dirToTarget = (em.chasee.transform.position - transform.position).normalized;
				float dstToTarget = Vector3.Distance (transform.position, em.chasee.transform.position);
				if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, wallMask)) {
					em.chasee = ShipVisibility.GetShip ().transform;
				} else {
					em.chasee = nextWaypoint;
				}
			} else {
				em.chasee = ShipVisibility.GetShip ().transform;
			}
			
		} else {
			em.chasee = nextWaypoint;
		}
	}
	
	private void TaggerUpdate() {
		if ( ShipVisibility.GetVisibility() >= 75 ) {
			bc.centralAgency = false;
			bc.chasee = ShipVisibility.GetShip ().transform;
		} else if (( ShipVisibility.GetVisibility() < 75 )&&( ShipVisibility.GetVisibility() >= 50 )) {
			bc.AdjustRadius( initialRadius * 1.75f );
			bc.centralAgency = true;
		} else if (( ShipVisibility.GetVisibility() < 50 )&&( ShipVisibility.GetVisibility() >= 25 )) {
			bc.AdjustRadius( initialRadius * 1.3f );
			bc.centralAgency = true;
		} else {
			bc.ResetRadius();
			bc.centralAgency = true;
		}
	}
	
	void OnTriggerEnter(Collider other) {
		switch((int)type) {
		case 2: // macrophage
			Waypoint w = other.GetComponent<Waypoint>();
			if (w) {
				if (w.teleportPoint) {
					if (!teleporting) {
						teleporting = true;
						tnextWaypoint = w.next.transform;
						StartCoroutine( "Teleport", w.teleportDelay );
					}
				} else {
					nextWaypoint = w.next.transform;
				}
			}
			break;
		}
	}
	
	private IEnumerator Teleport(float _delay) {
		yield return new WaitForSeconds(_delay);
		transform.position = tnextWaypoint.position;
		teleporting = false;
	}
	
}

/// <comment>
/// by Jose Pascua
/// Debugger for the class ShipVisiblity
/// </comment>