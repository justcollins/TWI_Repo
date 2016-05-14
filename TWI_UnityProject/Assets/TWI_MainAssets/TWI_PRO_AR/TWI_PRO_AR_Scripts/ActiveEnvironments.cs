using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActiveEnvironments : MonoBehaviour {

    private GameObject[] allObj;
    private List<GameObject> envNames;
    private List<GameObject> activeEnv;
    public GameObject[] startingEnv;
   
	// Use this for initialization
	void Start () {
        allObj = new GameObject[200];
        allObj = FindObjectsOfType<GameObject>(); 
        activeEnv = new List<GameObject>(25);
        for (int i = 0; i < startingEnv.Length; i++)
        {
            addToActive(startingEnv[i], i);
        }

        envNames = new List<GameObject>(70);
        findLayer(8);
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("Count: " + envNames.Count);
        foreach (GameObject e in envNames)
            e.GetComponent<MeshRenderer>().enabled = false;
        for (int i = 0; i < envNames.Count; i++)
        {
            for (int j = 0; j < activeEnv.Count; j++)
            {
                if (activeEnv[j].name == envNames[i].name)
                {
                    activeEnv[j].GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }
	}


    void findLayer(int fLayer)
    {
        //Debug.Log("fLayer: " + fLayer);
        for (int i = 0; i < allObj.Length; i++)
        {
            //Debug.Log("OBJ: " + allObj[i].name + " Layer: " + allObj[i].layer);
            if (allObj[i].layer == fLayer)
            {
                envNames.Add(allObj[i]);
                //Debug.Log("BUTTS");
            }
        }

        /*for (int j = 0; j < envNames.Count; j++)
        {
            Debug.Log(envNames[j].name);
        }*/
    }

    public void addToActive(GameObject envItem, int index)
    {
        activeEnv.Insert(index, envItem);
    }

	public bool checkActive(GameObject check){
		for (int i = 0; i < activeEnv.Count; i++) {
			if (check == activeEnv[i]) {
				Debug.Log ("True");
				return true;
			}
		}
		Debug.Log ("False");
		return false;
	}
}
