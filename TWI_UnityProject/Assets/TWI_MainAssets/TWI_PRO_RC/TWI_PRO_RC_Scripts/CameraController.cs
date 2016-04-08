using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject pCamera;
    public float vSpeed = 5.0f;
    public float hSpeed = 5.0f;

    public float minX = -45.0f;
    public float maxX = 45.0f;
    public float minY = -45.0f;
    public float maxY = 45.0f;

    public float yaw = 0.0f;
    public float pitch = 0.0f;


	// Update is called once per frame
    void Update()
    {
        yaw += hSpeed * Input.GetAxis("Mouse X");
        yaw = Mathf.Clamp(yaw, minX, maxX);

        pitch -= vSpeed * Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, minY, maxY);

        transform.localEulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
	
}
