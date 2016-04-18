using UnityEngine;
using System.Collections;

/*
Laser Aim script by Jonathan Harrington

Rotates laser based on camera rotational value
    */
public class LaserAim_2 : MonoBehaviour
{

    public GameObject mCamera;
    public Rigidbody objLaser;

    void Start()
    {
        //select object to follow camera
        objLaser = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        //rotates laser gameobject based on camera rotation
        objLaser.transform.Rotate(mCamera.transform.rotation.x,
                                mCamera.transform.rotation.y,
                                mCamera.transform.rotation.z);

        

    }
}
