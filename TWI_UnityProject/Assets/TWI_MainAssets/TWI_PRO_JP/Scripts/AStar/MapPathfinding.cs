using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapPathfinding : MonoBehaviour {

    PathRequestManager requestManager;
    AStarMap map;

    void Awake() {
        map = GetComponent<AStarMap>();
        requestManager = GetComponent<PathRequestManager>();
    }

    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos) {

        Vector3[] waypoints = new Vector3[0];
        bool pathSuccessful = false;

        AStarNode startNode = map.NodeFromWorldPoint(startPos);
        AStarNode targetNode = map.NodeFromWorldPoint(targetPos);
        Debug.Log( "START:" + startNode.gameObject.name + " // END:" + targetNode.gameObject.name );

        if ((startNode != null)&&(targetNode != null)) {

            List<AStarNode> openSet = new List<AStarNode>();
            HashSet<AStarNode> closedSet = new HashSet<AStarNode>();
            openSet.Add(startNode);

            while (openSet.Count > 0) {
                AStarNode currentNode = openSet[0];
                //Debug.Log( currentNode.gameObject.name + " is the currentNode" );
                for (int i = 1; i < openSet.Count; i++) {
                    if (openSet[i].fCost < currentNode.fCost ||
                        openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost) {
                        currentNode = openSet[i];
                    }
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == targetNode) {
                    //RetracePath(startNode, targetNode);
                    pathSuccessful = true;
                    break;
                }

                List<AStarNode> neighbours = currentNode.neighbors;
                foreach( AStarNode neighbour in neighbours) {
                    if (closedSet.Contains(neighbour)) { continue; }

                    float newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);

                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour)) {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }
        } else {
            if (startNode == null) {
                Debug.Log(" START NODE IS OUT OF BOUNDS?? ");
            } else {
                Debug.Log(" TARGET NODE IS OUT OF BOUNDS?? ");
            }
        }

        yield return null;
        if (pathSuccessful) {
            waypoints = RetracePath( startNode, targetNode );
        }

        requestManager.FinishedProcessingPath( waypoints, pathSuccessful );

    }

    public void StartFindPath(Vector3 start, Vector3 target) {
        StartCoroutine(FindPath(start, target));
    }

    Vector3[] RetracePath(AStarNode start, AStarNode end) {
        List<AStarNode> path = new List<AStarNode>();
        AStarNode currentNode = end;

        while (currentNode != start) {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        return NodesToPositions(path);
    }

    Vector3[] NodesToPositions(List<AStarNode> path) {
        List<Vector3> waypoints = new List<Vector3>();

        foreach (AStarNode node in path) {
            waypoints.Add( node.worldPosition );
        }

        return waypoints.ToArray();
    }

    float GetDistance(AStarNode nodeA, AStarNode nodeB) {
        return Vector3.Distance(nodeA.worldPosition, nodeB.worldPosition);
    }
}
