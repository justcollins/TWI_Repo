using UnityEngine;
using System.Collections;

public class PathRequester : MonoBehaviour {

    public Transform target;
    public float waypointRadius = 5.0f;

    public float lowerboundWait = 0.0f;
    public float upperboundWait = 10.0f;

    public bool active = false;
    private Vector3[] path;
    private int targetIndex;

    public Vector3 currWaypoint {
        get { return (targetIndex < path.Length)?path[targetIndex] : path[path.Length-1]; }
    }

    public bool showpath = false;

    IEnumerator Start() {
        while (active) {
            RequestPath();
            yield return new WaitForSeconds( Random.Range( lowerboundWait, upperboundWait ) );
        }
    }

    public void RequestPath() {
        if ((AStarMap.instance.NodeFromWorldPoint(transform.position) != null) && (AStarMap.instance.NodeFromWorldPoint(target.position) != null)) {
            targetIndex = 0;

            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        }
    }

    public void OnPathFound( Vector3[] newPath, bool pathSuccess ) {
        if (pathSuccess) {
            path = newPath;

            StopCoroutine( "FollowPath" );
            StartCoroutine( "FollowPath" );
        }
    }

    IEnumerator FollowPath() {
        Vector3 currentWaypoint = path[0];

        while (true) {
            //if (transform.position == currentWaypoint) {
            if ( Vector3.Distance(transform.position, currentWaypoint) < waypointRadius ) {
                targetIndex++;
                if (targetIndex >= path.Length) {
                    yield break;
                }

                currentWaypoint = path[targetIndex];
            }

            yield return null;
        }
    }

    public void OnDrawGizmos() {
        if (showpath) {
            if (path != null) {
                for (int i = targetIndex; i < path.Length; i++) {
                    Gizmos.color = Color.black;
                    Gizmos.DrawCube(path[i], Vector3.one);

                    if (i == targetIndex) {
                        Gizmos.DrawLine(transform.position, path[i]);
                    } else {
                        Gizmos.DrawLine(path[i - 1], path[i]);
                    }
                }
            }
        }
    }
}
