using UnityEngine;
using System.Collections;

public class ArbiterParasiteSpawner : MonoBehaviour
{

    public GameObject[] spawnLocations;
//    GameObject arbiter;

    void Awake()
    {
        if (spawnLocations.Length != 0f)
        {
            foreach (GameObject go in spawnLocations) {
                go.SetActive(false);
            }
			int r = Random.Range(0, spawnLocations.Length);
			EndGameManager.arbiter = spawnLocations[r].GetComponent<EnemyHealth>();
            spawnLocations[ r ].SetActive(true);
        }
    }

//    public void DestroyMe()
//    {
//        Destroy(arbiter);
//        Destroy(this.gameObject);
//    }
}