﻿﻿﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 *   Basic Enemy Behavior Class
 *   31 May 2016
 *   Jose Pascua
 * 
 * 	 Basic Enemy Behavior script
 */

public class BasicEnemyBehavior : MonoBehaviour {

    [Header("Target")]
    public Transform target;
    public bool findTargetAutomatically;

    [Header("Movement")]
    public float minVelocity = 1f;
    public float maxVelocity = 5f;
    public float rotateSpeed = 20f;
    public float waypointRadius = 50f;

    [Header("Behavior")]
    public EnemyType type;
    public DamageHandler playerDamageHandler;
    public float lowerboundWait = 0f;
    public float upperboundWait = 10f;
    public float distanceThreshold = 5f;
    public float damageThreshold = 4f;
    public bool allowedToWander = true;
    public Transform[] wanderpoints;
    public bool allowedToPatrol = false;
    public Waypoint initialPatrolPoint;

    [Header("Debug Options")]
    public bool showPath = true;
    public bool showDebugMessages = false;

    //PRIVATE VARIABLES
    private bool headToPlayer;
    private bool headToWaypoint;
    private bool chasing, chased;
    private Waypoint currPatrolpoint;
    private List<Transform> wanderpointList;
    private bool useWanderpoints;
    private bool patrolForward;
    private Vector3 currWp;
    private Vector3 playerWasLastSeen;
    private Rigidbody rb;
    private Vector3[] path;
    private int targetIndex;

    void Awake() {
    }

    void Start() {

        // MAKES SURE THERE IS A RIGIDBODY
        rb = GetComponent<Rigidbody>();
        if (rb == null) {
            gameObject.SetActive(false);
            if (showDebugMessages) {
                Debug.Log("NO RIGIDBODY ON: " + name + ". IT WAS DEACTIVATED.");
            }
        }

        // FINDS WHERE THE SHIP IS
        if (findTargetAutomatically) {
            target = ShipVisibility.GetShip().transform;
            playerDamageHandler = target.gameObject.GetComponent<DamageHandler>();
        }

        // CHECKS FOR PATROL REQUIREMENTS
        if (allowedToPatrol) {
            if (initialPatrolPoint == null) {
                allowedToPatrol = false;
                if (showDebugMessages) {
                    Debug.Log(name + " WAS SET TO PATROL, BUT HAD NO INITIAL PATROL POINT; PATROL TURNED OFF");
                }
            } else {
                currPatrolpoint = initialPatrolPoint;
                patrolForward = true;
            }
        }

        // CHECKS FOR WANDER REQUIREMENTS
        if (allowedToWander) {
            if (wanderpoints.Length == 0) {
                useWanderpoints = false;
            } else {
                useWanderpoints = true;

                wanderpointList = new List<Transform>();
                foreach (Transform w in wanderpoints) {
                    wanderpointList.Add(w);
                }
            }
        }

        // SETS UP OTHER VARIABLES
        headToPlayer = false;
        headToWaypoint = false;
        chasing = false; chased = false;

        // BEGINS LOOKING FOR THINGS
        StartCoroutine(LookForTarget());
    }

    void Update() {
        if (playerDamageHandler) {
            DealDamage();
        }
    }

    void FixedUpdate() {
        Movement();
    }



    #region Pathfinding

    IEnumerator LookForTarget() {
        while (true) {
            CheckWherePlayerIs();

            if (chasing) {
                if ((AStarMap.instance.NodeFromWorldPoint(transform.position) != null) && (AStarMap.instance.NodeFromWorldPoint(playerWasLastSeen) != null)) {
                    if (AStarMap.instance.NodeFromWorldPoint(transform.position) != AStarMap.instance.NodeFromWorldPoint(playerWasLastSeen)) {
                        if (showDebugMessages) {
                            Debug.Log(gameObject.name + " is requesting path to player...");
                        }
                        targetIndex = 0;
                        PathRequestManager.RequestPath(transform.position, playerWasLastSeen, OnPathFound);
                    } else {
                        if (showDebugMessages) {
                            Debug.Log(gameObject.name + " going to player...");
                        }
                        headToPlayer = true;
                    }
                }
            } else {
                if (allowedToWander) {
                    if (chased == false) {
                        targetIndex = 0;
                        chased = true;
                        if (useWanderpoints) {
                            // FINDS A RANDOM POINT IN THE WANDERPOINTS ARRAY
                            if (showDebugMessages) {
                                Debug.Log(gameObject.name + " requesting path to random point in preset array...");
                            }
                            int mapIndex = Random.Range(0, wanderpointList.Count - 1);
                            while (AStarMap.instance.NodeFromWorldPoint(transform.position) == AStarMap.instance.NodeFromWorldPoint(wanderpointList[mapIndex].position)) {
                                mapIndex = Random.Range(0, wanderpointList.Count - 1);
                            }

                            PathRequestManager.RequestPath(transform.position, wanderpointList[mapIndex].transform.position, OnPathFound);
                        } else {
                            // FINDS A RANDOM POINT IN THE ENTIRE MAP
                            if (showDebugMessages) {
                                Debug.Log(gameObject.name + " requesting path to random point in entire map...");
                            }
                            int mapIndex = Random.Range(0, AStarMap.instance.map.Length - 1);
                            PathRequestManager.RequestPath(transform.position, AStarMap.instance.map[mapIndex].transform.position, OnPathFound);
                        }
                    }
                } else if (allowedToPatrol) {
                    if (chased == false) {
                        if (showDebugMessages) {
                            Debug.Log(gameObject.name + " is going to its next point: ");
                        }
                        targetIndex = 0;
                        chased = true;

                        PathRequestManager.RequestPath(transform.position, currPatrolpoint.transform.position, OnPathFound);
                    }
                }
            }

            if (chasing == false) { CheckForRequirements(); }
            //Debug.Log( "CHASING THE PLAYER? " + chasing + " // ON PATH OF RAMPAGE? " + chased );

            yield return new WaitForSeconds(Random.Range(lowerboundWait, upperboundWait));
        }
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccess) {
        if (pathSuccess) {
            path = newPath;

            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    void CheckWherePlayerIs() {
        if ((AStarMap.instance.NodeFromWorldPoint(target.position) != null)) {
            //Debug.Log("Player was found at this node: " + AStarMap.instance.NodeFromWorldPoint(target.position).gameObject.name);
            playerWasLastSeen = target.position;
        } else { Debug.Log("Player out of bounds, using location where it was last seen."); }
    }

    public void MakeMeChase() {
        chasing = true;
    }

    #endregion

    #region KnowledgePool
    // knowing when it is time to chase the player

    void CheckForRequirements() {
        switch (type) {
            case EnemyType.Macrophage:
                if (ShipVisibility.GetTagged()) { chasing = true; }
                break;

            case EnemyType.ArbiterMinion:
                if (ShipVisibility.GetMotherSeen()) { chasing = true; }
                break;
        }

        if (showDebugMessages) {
            if (chasing) {
                Debug.Log(gameObject.name + " IS NOW CHASING THE PLAYER!!");
            } else {
                Debug.Log(gameObject.name + " IS NOT CHASING THE PLAYER.");
            }
        }
    }

    #endregion

    #region Movement

    void Movement() {
        if (headToPlayer) { GoToPlayer(); }
        if (headToWaypoint) { GoToWaypoint(); }

        if ((!headToPlayer) && (!headToWaypoint)) {
            rb.velocity -= rb.velocity;
        }

        float speed = rb.velocity.magnitude;
        if (speed > maxVelocity) {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
    }

    IEnumerator FollowPath() {
        currWp = path[0];
        headToWaypoint = true;

        while (true) {
            if (Vector3.Distance(transform.position, currWp) < waypointRadius) {
                targetIndex++;
                if (targetIndex >= path.Length) {
                    if (chasing == false) {

                        //PATROL LOGIC
                        if (allowedToPatrol && !allowedToWander) {
                            if (patrolForward) {
                                if (currPatrolpoint.next != null) {
                                    currPatrolpoint = currPatrolpoint.next;
                                } else {
                                    currPatrolpoint = currPatrolpoint.prev;
                                    patrolForward = !patrolForward;
                                }
                            } else {
                                if (currPatrolpoint.prev != null) {
                                    currPatrolpoint = currPatrolpoint.prev;
                                } else {
                                    currPatrolpoint = currPatrolpoint.next;
                                    patrolForward = !patrolForward;
                                }
                            }
                        }

                        chased = false;
                        if (showDebugMessages) { Debug.Log("finished path"); }
                    }
                    headToWaypoint = false;
                    yield break;
                }

                currWp = path[targetIndex];
            }
            yield return null;
        }
    }

    void GoToPlayer() {
        rb.velocity += CalculateMovement(target.position) * maxVelocity;
        LookAtTarget(target.position);

        if (Vector3.Distance(transform.position, target.position) <= distanceThreshold) {
            headToPlayer = false;
            if (showDebugMessages) {
                Debug.Log("... by the player now!");
            }
        }
    }

    void GoToWaypoint() {
        if (currWp != null) {
            rb.velocity += CalculateMovement(currWp) * maxVelocity;
            LookAtTarget(currWp);
        }
    }

    void LookAtTarget(Vector3 target) {
        Vector3 dir;
        dir = (target - transform.position).normalized;

        if (dir == Vector3.zero) {
        } else {
            transform.rotation = Quaternion.Slerp(transform.rotation, (Quaternion.LookRotation(dir, Vector3.up)), Time.deltaTime * rotateSpeed);
        }
    }

    Vector3 CalculateMovement(Vector3 t) {
        return t - transform.position;
    }

    #endregion

    #region Damage

    void DealDamage() {
        if (Vector3.Distance(this.transform.position, target.transform.position) < damageThreshold) {
            if (showDebugMessages) {
                Debug.Log(gameObject.name + " IS DAMAGING THE PLAYER");
            }
            switch (type) {
                case EnemyType.Macrophage:
                    playerDamageHandler.setMacrophageNear(true);
                    break;

                case EnemyType.ArbiterMinion:
                    playerDamageHandler.setArbiterMinionNear(true);
                    break;
            }
        } else {
            if (showDebugMessages) {
                Debug.Log(gameObject.name + " STOPPED DAMAGING THE PLAYER");
            }
            switch (type) {
                case EnemyType.Macrophage:
                    playerDamageHandler.setMacrophageNear(false);
                    break;

                case EnemyType.ArbiterMinion:
                    playerDamageHandler.setArbiterMinionNear(false);
                    break;
            }
        }
    }

    #endregion

    #region Respawn

    public void GotRespawned() {
        if (showDebugMessages) {
            Debug.Log(gameObject.name + " IS REBORN.");
        }
        chased = false;
        headToWaypoint = false;
        targetIndex = 0;
        path = null;
        rb.velocity = Vector3.zero;
        //currWp = AStarMap.instance.NodeFromWorldPoint(transform.position).transform.position;
    }

    #endregion

    #region Debug

    void OnDrawGizmos() {
        if ((path != null) && (showPath)) {
            for (int i = targetIndex; i < path.Length; i++) {
                Gizmos.color = Color.black;
                if (headToWaypoint) {
                    if (allowedToPatrol) { Gizmos.color = Color.yellow; }
                    if (allowedToWander) { Gizmos.color = Color.cyan; }
                }
                if (headToPlayer) { Gizmos.color = Color.blue; }

                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex) {
                    Gizmos.DrawLine(transform.position, path[i]);
                } else {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }

    #endregion
}
