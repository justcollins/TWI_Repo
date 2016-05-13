﻿using UnityEngine;
using System.Collections;

public class MapTraverser : MonoBehaviour {

    public Transform target;
    public float speed = 5;
    
    private Vector3[] path;
    private int targetIndex;

    void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            if ((AStarMap.instance.NodeFromWorldPoint(transform.position) != null) && (AStarMap.instance.NodeFromWorldPoint(target.position) != null)) {
                targetIndex = 0;
                PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
            }
        }
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccess) {
        if (pathSuccess) {
            path = newPath;

            StopCoroutine( "FollowPath" );
            StartCoroutine( "FollowPath" );
        }
    }

    IEnumerator FollowPath() {
        Vector3 currentWaypoint = path[0];

        while (true) {
            if (transform.position == currentWaypoint) {
                targetIndex++;
                if (targetIndex >= path.Length) {
                    yield break;
                }

                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards( transform.position, currentWaypoint, speed * Time.deltaTime );
            yield return null;
        }
    }

    public void OnDrawGizmos() {
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
