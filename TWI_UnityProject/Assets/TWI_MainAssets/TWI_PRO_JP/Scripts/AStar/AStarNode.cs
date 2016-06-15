﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStarNode : MonoBehaviour {

    private float _gCost;
    private float _hCost;
    public AStarNode[] initialNeighbors;
    private List<AStarNode> _neighbors = new List<AStarNode>();
    public float radius;

    [Header("Visibility Options")]
    public bool visible;
    public bool showNeighbors = false;

    private AStarNode _parent;

    public float fCost {
        get { return _gCost + _hCost; }
    }

    public float gCost {
        set { _gCost = value; }
        get { return _gCost; }
    }

    public float hCost {
        set { _hCost = value; }
        get { return _hCost; }
    }

    public Vector3 worldPosition {
        get { return transform.position; }
    }

    public List<AStarNode> neighbors {
        get { return _neighbors; }
    }

    public AStarNode parent {
        set { _parent = value; }
        get { return _parent; }
    }

    void Awake() {
        foreach (AStarNode neighbor in initialNeighbors) {
            _neighbors.Add(neighbor);
        }
        _gCost = 0f;
        _hCost = 0f;
    }

    void OnDrawGizmos() {
        if (visible) {
            Gizmos.color = new Color(0f, 1f, 1f, 0.4f);
            Gizmos.DrawSphere(transform.position, radius);
        }

        if (showNeighbors) {
            foreach (AStarNode n in initialNeighbors) {
                if (n != null) {
                    Gizmos.color = new Color(0f, 1f, 1f, 0.5f);
                    Gizmos.DrawLine(transform.position, n.transform.position);
                } else {
                    Gizmos.color = new Color(1f, 0f, 0f, 0.6f);
                    Gizmos.DrawCube(transform.position, Vector3.one * (radius * 0.5f));
                }
            }
        }
    }

    void OnDrawGizmosSelected() {
        if (visible) {
            Gizmos.color = new Color(1f, 1f, 0f, 0.4f);
            Gizmos.DrawSphere(transform.position, radius);
        }

        if (showNeighbors) {
            foreach (AStarNode n in initialNeighbors) {
                if (n != null) {
                    Gizmos.color = new Color(1f, 1f, 0f, 0.4f);
                    Gizmos.DrawLine(transform.position, n.transform.position);
                }
            }
        }
    }
}
