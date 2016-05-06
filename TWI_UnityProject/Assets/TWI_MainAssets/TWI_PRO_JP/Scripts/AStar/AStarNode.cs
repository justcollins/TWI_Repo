﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStarNode : MonoBehaviour {

    private float _gCost;
    private float _hCost;
    public AStarNode[] initialNeighbors;
    private List<AStarNode> _neighbors = new List<AStarNode>();
    public float radius;
    public bool visible;
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
    }
}