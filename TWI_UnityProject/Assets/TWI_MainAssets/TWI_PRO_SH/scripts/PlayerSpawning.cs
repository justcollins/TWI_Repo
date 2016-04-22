using UnityEngine;
using System.Collections;
/*by: Sarah
 * this script handles the actual spawn for our Player
 */
public class PlayerSpawning : MonoBehaviour
{
	private GameObject player;
	private GameObject randPlayerSpawn;
	private PlayerSpawnController playerSpawnCTRL;
	
	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerSpawnCTRL = GameObject.FindGameObjectWithTag ("PlayerSpawnCTRL").GetComponent<PlayerSpawnController>();
	}
	
	void Start() 
	{
		//inherits from "PlayerSpawnController" script
		randPlayerSpawn = playerSpawnCTRL.GetRandomPlayerSpawn();
		SpawnPlayer();
	}
	
	void SpawnPlayer()
	{
		player.transform.position = randPlayerSpawn.transform.position;//accessing Player's pivot point
		Debug.Log("You have spawn at: " + randPlayerSpawn.name);
	}
}
