using UnityEngine;
using System.Collections;
/*
Jonathan Harrington

 guides laser to target

*/
public class LaserAim : MonoBehaviour {

    public float speed = 3.0f; //how fast laserguide moves
    private Vector3 targetPos; //inital position of laserguide

    void Start(){

        targetPos = transform.position;

    }

    void Update(){
       
        // keep certain distance from camera
        float distance = transform.position.z - Camera.main.transform.position.z;

        //laser guide moves with mouse position values
        targetPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance+50);
        //use 2d space
        targetPos = Camera.main.ScreenToWorldPoint(targetPos);
        //updates location of laserguide
        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
    }
}

