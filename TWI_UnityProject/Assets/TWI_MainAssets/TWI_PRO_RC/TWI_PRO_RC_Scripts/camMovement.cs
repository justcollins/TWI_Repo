using UnityEngine;
using System.Collections;

public class camMovement : MonoBehaviour {

    public float vSpeed = 5.0f;
    public float hSpeed = 5.0f;

    public float yaw = 0.0f;
    public float pitch = 0.0f;


	// Update is called once per frame
    void Update()
    {
        yaw += hSpeed * Input.GetAxis("Mouse X");
        pitch -= vSpeed * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
	
}
