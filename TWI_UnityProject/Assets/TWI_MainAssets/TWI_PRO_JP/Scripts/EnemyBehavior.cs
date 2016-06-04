﻿using UnityEngine;
using System.Collections;

/**
 *   Enemy Sight Class
 *   10 Feb 2016
 *   Jose Pascua
 * 
 *   Finding targets
 */

public enum EnemyType {
    None = 0,
    Tagger_IGG = 1,
    Macrophage = 2,
    Bodyguard_IGM = 3,
    ArbiterParasite = 4,
    ArbiterMinion = 5
}

public class EnemyBehavior : MonoBehaviour {

    public EnemyType type;
    private EnemyMovement em;
    private BoidController bc;
    private PathRequester pr;

    private Transform nextWaypoint;
    private float initialRadius;

    internal bool goinForth = true;

    [Header("Pathfinding")]
    public bool pathfinding;
    private bool pathfindingOverride;
    public bool wandering = false;
    private int wanderIndex;

    void Awake() {
        switch (type) {
            case EnemyType.Tagger_IGG:
                TaggerAwake();
                break;

            case EnemyType.Bodyguard_IGM:
                BodyguardAwake();
                break;

            case EnemyType.Macrophage:
                MacrophageAwake();
                break;

            case EnemyType.ArbiterMinion:
                ArbiterMinionAwake();
                break;
        }
    }

    void Update() {
        switch (type) {
            case EnemyType.Tagger_IGG:
                TaggerUpdate();
                break;

            case EnemyType.Bodyguard_IGM:
                BodyguardUpdate();
                break;

            case EnemyType.Macrophage:
                MacrophageUpdate();
                break;

            case EnemyType.ArbiterMinion:
                ArbiterMinionUpdate();
                break;
        }
    }

    #region Macrophage

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     *                         Macrophage
     *  -Uses Pathfinding
     *  -Moves through predefined paths
     *  -Heads towards the player based on if the player has been tagged
     * 
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private void MacrophageAwake() {
        em = GetComponent<EnemyMovement>();
        if (pathfinding) {
            pr = GetComponent<PathRequester>();
            pr.active = true;
        }

        nextWaypoint = em.chasee;
    }

    private void MacrophageUpdate() {
        if (ShipVisibility.GetTagged()) {
            if (pathfinding) {
                pr.target = ShipVisibility.GetShip().transform;

                if (Vector3.Distance(ShipVisibility.GetShip().transform.position, transform.position) > pr.waypointRadius) {
                    //if it is too far, it'll follow the waypoints
                    em.chaseePos = pr.currWaypoint;
                } else {
                    //if it is close enough, just steer to the player
                    em.chaseePos = ShipVisibility.GetShip().transform.position;
                }

                /* to do: use raycasts instead of distance; cast a raycast towards the player and if the hit is the player
                 * then just steer towards the player.*/
            } else {
                em.chaseePos = ShipVisibility.GetShip().transform.position;
            }
        } else {
            if (wandering && pathfinding) {
                if (Vector3.Distance(ShipVisibility.GetShip().transform.position, transform.position) > pr.waypointRadius) {
                    //if it is far enough, just go through the waypoints
                    em.chaseePos = pr.currWaypoint;
                } else {
                    //if it is close enough, just steer to the player
                    wanderIndex = Random.Range(0, AStarMap.instance.map.Length);
                    pr.target = AStarMap.instance.map[wanderIndex].transform;
                }
            } else {
                em.chaseePos = nextWaypoint.position;
                em.chasee = nextWaypoint;
            }
        }
    }

    #endregion

    #region Tagger (IGG)

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     *                        "Taggers" (IGG)
     *  -Does not use pathfinding
     *  -Uses flocking behaviors defined through BoidController
     *  -Moves through predefined paths
     *  -Attacks the player depending on how visible the player is
     *      -Sphere of detection gets larger 
     * 
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private void TaggerAwake() {
        bc = GetComponent<BoidController>();
        initialRadius = bc.centerRadius;
    }

    private void TaggerUpdate() {
        if (ShipVisibility.GetVisibility() >= 75) {
            bc.centralAgency = false;
            bc.chasee = ShipVisibility.GetShip().transform;
        } else if ((ShipVisibility.GetVisibility() < 75) && (ShipVisibility.GetVisibility() >= 50)) {
            bc.AdjustRadius(initialRadius * 1.75f);
            bc.centralAgency = true;
        } else if ((ShipVisibility.GetVisibility() < 50) && (ShipVisibility.GetVisibility() >= 25)) {
            bc.AdjustRadius(initialRadius * 1.3f);
            bc.centralAgency = true;
        } else {
            bc.ResetRadius();
            bc.centralAgency = true;
        }
    }

    #endregion

    #region Bodyguard (IGM)

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     *                        "Bodyguard" (IGM)
     *  -Does not use pathfinding
     *  -Steers towards
     *  -Attacks the player depending on how close the player is
     * 
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private EnemyMovement masterMacrophage;
    [Header("IGM Only")]
    public float distanceThreshold;
    private Vector3 igmOffset;

    private void BodyguardAwake() {
        em = GetComponent<EnemyMovement>();
        masterMacrophage = em.chasee.GetComponent<EnemyMovement>();
    }

    private void BodyguardUpdate() {
        if (Vector3.Distance(ShipVisibility.GetShip().transform.position, transform.position) > distanceThreshold) {
            em.chaseePos = ShipVisibility.GetShip().transform.position;
        } else {
            igmOffset = new Vector3(Random.Range(-distanceThreshold, distanceThreshold),
                                    Random.Range(-distanceThreshold, distanceThreshold),
                                    Random.Range(-distanceThreshold, distanceThreshold));
            igmOffset = igmOffset.normalized * distanceThreshold;
            em.chaseePos = masterMacrophage.transform.position;
        }
    }

    /* to do: finish up IGM*/

    #endregion

    #region Arbiter Minions

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     *                        Arbiter Minions
     *  -Uses Pathfinding
     *  -Moves freely through the body saves for the
     *  -When the mother attacks, it should head towards the player
     * 
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private void ArbiterMinionAwake() {
        em = GetComponent<EnemyMovement>();
        if (pathfinding) {
            pr = GetComponent<PathRequester>();
            pr.active = true;
        }

        nextWaypoint = em.chasee;
    }

    private void ArbiterMinionUpdate() {
        if (ShipVisibility.GetMotherSeen()) {
            if (pathfinding) {
                pr.target = ShipVisibility.GetShip().transform;

                if (Vector3.Distance(ShipVisibility.GetShip().transform.position, transform.position) > pr.waypointRadius) {
                    //if it is far enough, just go through the waypoints
                    em.chaseePos = pr.currWaypoint;
                } else {
                    //if it is close enough, just steer to the player
                    em.chaseePos = ShipVisibility.GetShip().transform.position;
                }
            } else {
                em.chaseePos = ShipVisibility.GetShip().transform.position;
            }
        } else {
            if (wandering && pathfinding) {
                wanderIndex = Random.Range(0, AStarMap.instance.map.Length - 1);
                pr.target = AStarMap.instance.map[wanderIndex].transform;

                if (Vector3.Distance(ShipVisibility.GetShip().transform.position, transform.position) > pr.waypointRadius) {
                    //if it is far enough, just go through the waypoints
                    em.chaseePos = pr.currWaypoint;
                } else {
                    //if it is close enough, just steer to the player
                    wanderIndex = Random.Range(0, AStarMap.instance.map.Length);
                    pr.target = AStarMap.instance.map[wanderIndex].transform;
                }
            } else {
                em.chaseePos = nextWaypoint.position;
                em.chasee = nextWaypoint;
            }
        }
    }

    #endregion

    void OnTriggerStay(Collider other) {
        switch ((int)type) {
            case 2: // macrophage
                if (other.GetComponent<Waypoint>()) { // checks to see if it collided with a waypoint
                    if (other.transform == em.chasee) { // checks to see if the collided waypoint was the chasee
                        if (goinForth) { //goin forward
                            if (other.GetComponent<Waypoint>().next) { // if there is a next, then go there
                                nextWaypoint = other.GetComponent<Waypoint>().next.transform;
                            } else { // otherwise turn around
                                goinForth = false;
                                nextWaypoint = other.GetComponent<Waypoint>().prev.transform;
                            }
                        } else { // goin backwards
                            if (other.GetComponent<Waypoint>().prev) { // if there is a previous, then go there
                                nextWaypoint = other.GetComponent<Waypoint>().prev.transform;
                            } else { // otherwise turn around
                                goinForth = true;
                                nextWaypoint = other.GetComponent<Waypoint>().next.transform;
                            }
                        }
                    }

                    if (pathfinding && ShipVisibility.GetTagged()) {
                        // if the pathfinding is on and it's currently seeking the player, get reference to the last passed waypoint
                        nextWaypoint = other.transform;
                    }
                }
                break; // end of case 2

            case 5: // arbiter minion
                if (other.GetComponent<Waypoint>()) { // checks to see if it collided with a waypoint
                    if (other.transform == em.chasee) { // checks to see if the collided waypoint was the chasee
                        if (goinForth) { //goin forward
                            if (other.GetComponent<Waypoint>().next) { // if there is a next, then go there
                                nextWaypoint = other.GetComponent<Waypoint>().next.transform;
                            } else { // otherwise turn around
                                goinForth = false;
                                nextWaypoint = other.GetComponent<Waypoint>().prev.transform;
                            }
                        } else { // goin backwards
                            if (other.GetComponent<Waypoint>().prev) { // if there is a previous, then go there
                                nextWaypoint = other.GetComponent<Waypoint>().prev.transform;
                            } else { // otherwise turn around
                                goinForth = true;
                                nextWaypoint = other.GetComponent<Waypoint>().next.transform;
                            }
                        }
                    }

                    if (pathfinding && ShipVisibility.GetMotherSeen()) {
                        // if the pathfinding is on and it's currently seeking the player, get reference to the last passed waypoint
                        nextWaypoint = other.transform;
                    }
                }
                break; // end of case 5
        }
    }

}

/// <comment>
/// by Jose Pascua
/// Debugger for the class ShipVisiblity
/// </comment>