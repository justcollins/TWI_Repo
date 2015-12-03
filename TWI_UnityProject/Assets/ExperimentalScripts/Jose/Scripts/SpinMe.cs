using UnityEngine;
using System.Collections;

public class SpinMe : MonoBehaviour {

	public Vector3 SpinHow = Vector3.zero;

	// Update is called once per frame
	void Update () {
		this.transform.Rotate (SpinHow);
	}
}
