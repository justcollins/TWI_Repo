using UnityEngine;
using System.Collections;

public class GoTo : MonoBehaviour {

	private NavMeshAgent nma;
	public Transform target;

	// Use this for initialization
	void Start () {
		nma = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		nma.SetDestination(target.position);
	}
}
