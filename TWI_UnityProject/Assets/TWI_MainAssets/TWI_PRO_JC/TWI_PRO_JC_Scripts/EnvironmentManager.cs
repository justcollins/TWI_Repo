using UnityEngine;
using System.Collections;

public class EnvironmentManager : MonoBehaviour {

    public GameObject startingGroup;
    public GameObject[] environmentGroups;

    private GameObject currentGroup;
    private GameObject previousGroup;
    private bool changeCurGroup;

    public void SetActiveGroups(GameObject newCurrentGroup, GameObject newPreviousGroup) {
        for(int i = 0; i < environmentGroups.Length; i++) {
            if(newCurrentGroup.name == environmentGroups[i].name) {
                currentGroup = newCurrentGroup;
                Debug.Log("currentGroup: " + currentGroup);
            }

            if(newPreviousGroup.name == environmentGroups[i].name) {
                previousGroup = newPreviousGroup;
                Debug.Log("previousGroup: " + previousGroup);
            }
        }

        changeCurGroup = true;
    }

    public string GetCurrentGroup() {
        return currentGroup.name;
    }

	void Start () {
        SetActiveGroups(startingGroup, startingGroup);
    }
	
	void Update () {
	    if(changeCurGroup) {
            ManageVisibleGroup();
        }
	}

    private void ManageVisibleGroup() {
        for(int i = 0; i < environmentGroups.Length; i++) {
            if(environmentGroups[i].name == currentGroup.name || environmentGroups[i].name == previousGroup.name) {
                environmentGroups[i].SetActive(true);
            } else {
                environmentGroups[i].SetActive(false);
            }
        }
        changeCurGroup = false;
    }

}
