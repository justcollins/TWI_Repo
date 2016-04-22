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
        for (int i = 0; i < startingEnv.Length; i++)
        {
            addToList(startingEnv[i], i);
        }
        activeEnv = new List<GameObject>(5);
        allObj = new GameObject[200];
        allObj = FindObjectsOfType<GameObject>();
        envNames = new List<GameObject>(10);
        findLayer(8);

    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < envNames.Count; i++)
        {
            for (int j = 0; j < activeEnv.Count; j++)
            {
                if (activeEnv[j].name == envNames[i].name)
                {
                    envNames[i].SetActive(true);
                }
            }
        }
	}


    void findLayer(int fLayer)
    {
        Debug.Log("fLayer: " + fLayer);
        for (int i = 0; i < allObj.Length; i++)
        {
            Debug.Log("OBJ: " + allObj[i].name + " Layer: " + allObj[i].layer);
            if (allObj[i].layer == fLayer)
            {
                envNames.Add(allObj[i]);
                Debug.Log("BUTTS");
            }
        }

        /*for (int j = 0; j < envNames.Count; j++)
        {
            Debug.Log(envNames[j].name);
        }*/
    }

    public void addToList(GameObject envItem, int index)
    {
        activeEnv.Insert(index, envItem);
    }
}
