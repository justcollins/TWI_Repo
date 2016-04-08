using UnityEngine;
using System.Collections;

public class FanSpin : MonoBehaviour {

    public float spinSpeed = 1;

	void Start () {
	
	}
	
	void Update () {
        SpinFan();
	}

    private void SpinFan() {
        transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
    }
}
