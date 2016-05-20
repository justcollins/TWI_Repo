using UnityEngine;
using System.Collections;

public class ArbiterBehavior : MonoBehaviour {
	
	public GameObject spawnOfSatan;
	public Transform spawnLoc;

	public GameObject motherSpawn;
	public ActiveEnvironments activeEnvCheck;

	private int spawnTotal;
	private int spawnMax = 50;
	private float maxTime;
	private float longTime = 0.0f;
	private float shortTime = 0.0f;

	// Use this for initialization
	void Start () {
		Vector3 spawn = new Vector3 (spawnLoc.position.x, spawnLoc.position.y, spawnLoc.position.z);
		activeEnvCheck = FindObjectOfType<ActiveEnvironments>();
		for(int i = 0; i < 4; i++){
			Instantiate (spawnOfSatan, spawn, Quaternion.identity);
			spawnTotal++;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(spawnTotal < spawnMax){
			if(activeEnvCheck.checkActive(motherSpawn)){
				shortSpawn();
			}
			else{
				longSpawn();
			}
		}
	}

	void shortSpawn(){
		Debug.Log ("Short");
		Vector3 spawn = new Vector3 (spawnLoc.position.x, spawnLoc.position.y, spawnLoc.position.z);
		maxTime = 4.0f;
		shortTime += Time.deltaTime;
		Debug.Log (shortTime);
		if (shortTime >= maxTime) {
			Instantiate (spawnOfSatan, spawn, Quaternion.identity);
			shortTime = 0;
		}
	}

	void longSpawn(){
		Debug.Log ("Long");
		Vector3 spawn = new Vector3 (spawnLoc.position.x, spawnLoc.position.y, spawnLoc.position.z);
		maxTime = 8.0f;
		Debug.Log (longTime);
		longTime += Time.deltaTime;
		if (longTime >= maxTime) {
			Instantiate (spawnOfSatan, spawn, Quaternion.identity);
			longTime = 0;
		}
	}
}
