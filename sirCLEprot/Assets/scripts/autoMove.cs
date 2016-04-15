using UnityEngine;
using System.Collections;

public class autoMove : MonoBehaviour {

    public enum Direction { north, south, east, west};

    public float maxAmount;
    public Direction dir1;

    public float maxX;
    public float minX;
    public float maxY;
    public float minY;
    public float stepX;
    public float stepY;

	// Use this for initialization
	void Start () {
        dir1 = Direction.north;
	}
	
	// Update is called once per frame
	void Update () {
	
        if(dir1 == Direction.north)
        {
            if(transform.position.y >maxY)
            {
                dir1 = Direction.west;
            }
        }

        	}
}
