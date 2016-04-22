using UnityEngine;
using System.Collections;
/*by: Sarah
 * stores GameObjects tagged "PlayerSpawn" in an array and randomizes
 * where the Player will spawn in game space
 */
public class PlayerSpawnController : MonoBehaviour 
{
	private GameObject[] playerSpawnArray; //initialize an array variable
	private int randSpawnID;

	void Awake()
	{
		//the length of the array is determined by the number of GameObjects tagged "PlayerSpawn"
		playerSpawnArray = GameObject.FindGameObjectsWithTag ("PlayerSpawn");
	}

	void Start() 
	{
		GetRandomPlayerSpawn();
	}

	public GameObject GetRandomPlayerSpawn()//it's type GameObject b/c it's accessed by another script
	{
		randSpawnID = Random.Range(0,(playerSpawnArray.Length));
		Debug.Log("SpawnID: " + randSpawnID);
		return playerSpawnArray[randSpawnID];//returns randSpawnID value to another script called "PlayerSpawning"
	}
}
