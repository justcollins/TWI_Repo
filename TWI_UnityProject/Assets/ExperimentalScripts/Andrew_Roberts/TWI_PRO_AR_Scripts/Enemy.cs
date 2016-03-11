using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Creates a Point based movement for enemies in the world, currently outdated by Jose.
/// </summary>

public class Enemy : MonoBehaviour
{

    public Transform[] patrolPoints;
    public float moveSpeed;

    //Private
    private int currentPoint, prevPoint;


    // Use this for initialization
    void Start()
    {
        transform.position = patrolPoints[0].position;
        currentPoint = 0;
        prevPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == patrolPoints[currentPoint].position)
        {
            if (currentPoint == 0)
            {
                prevPoint = 0;
                currentPoint++;
            }
            else if (currentPoint == patrolPoints.Length - 1)
            {
                prevPoint++;
                currentPoint--;

            }
            else if (prevPoint < currentPoint)
            {
                prevPoint++;
                currentPoint++;
            }
            else if (prevPoint > currentPoint)
            {
                prevPoint--;
                currentPoint--;

            }

        }
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, moveSpeed * Time.deltaTime);
    }
}
