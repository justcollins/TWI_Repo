using UnityEngine;
using System.Collections;

public class ArbiterParasite : MonoBehaviour {
	
	public Transform[] spawnLocations;
	public GameObject arbiterPrefab;
	//	public float randomness = 0;
	
	GameObject arbiter;
	
	void Awake() {
		if (spawnLocations.Length != 0f) {
			float loc = Random.Range( 0.0f, (float)spawnLocations.Length );
			
			//			Vector3 rand = new Vector3 ((Random.value * 2) - 1, (Random.value * 2) - 1, (Random.value * 2) - 1);
			//			rand.Normalize();
			//			rand *= randomness;
			
			//			arbiter = Instantiate( arbiterPrefab, spawnLocations[(int)loc].position + rand, spawnLocations[(int)loc].rotation) as GameObject;
			arbiter = Instantiate( arbiterPrefab, spawnLocations[(int)loc].position, spawnLocations[(int)loc].rotation) as GameObject;
			arbiter.transform.Rotate( new Vector3(0, (Random.value * 360), 0) );
		}
	}
	
	public void DestroyMe() {
		Destroy (arbiter);
		Destroy (this.gameObject);
	}
}
