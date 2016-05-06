using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStarMap : MonoBehaviour {

    static AStarMap _instance;
    public static AStarMap instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<AStarMap>();
            }
            return _instance;
        }
    }

    public AStarNode[] map;
    //internal List<AStarNode> path;
    public bool findMapAutomatically = false;

    public int size {
        get {
            return map.Length;
        }
    }

    void Awake() {
        if ((map.Length == 0)&&(findMapAutomatically)) {
            int msize = transform.childCount;
            map = new AStarNode[msize];

            for (int i = 0; i < msize; i++) {
                map[i] = transform.GetChild(i).GetComponent<AStarNode>();
            }
        }
    }

    public AStarNode NodeFromWorldPoint( Vector3 worldPosition ) {
        foreach (AStarNode n in map) {
            Vector3 npos = n.worldPosition;
            if (Vector3.Distance(npos, worldPosition) < n.radius) {
                return n;
            }
        }

        Debug.Log(" OUT OF BOUNDS: Position passed into AStarMap.NodeFromWorldPoint does not fall into any node ");
        return null;
    }

    //public void OnDrawGizmos() {
    //    if (path != null) {
    //        foreach (AStarNode n in path) {
    //            Gizmos.color = Color.red;
    //            Gizmos.DrawSphere(n.worldPosition, 1);
    //        }
    //    }
    //}
}
