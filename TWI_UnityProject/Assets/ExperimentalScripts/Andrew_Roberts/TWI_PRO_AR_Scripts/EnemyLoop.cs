using UnityEngine;
using System.Collections;


/// <summary>
/// Creates a Point based movement for enemies in the world, currently outdated by Jose.
/// </summary>

public class EnemyLoop : MonoBehaviour {

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
                currentPoint = 0;

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
        transform.LookAt(patrolPoints[currentPoint].position);
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, moveSpeed * Time.deltaTime);
    }
}

