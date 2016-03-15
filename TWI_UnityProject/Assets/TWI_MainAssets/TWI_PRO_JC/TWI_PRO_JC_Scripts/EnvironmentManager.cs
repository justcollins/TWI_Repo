using UnityEngine;
using System.Collections;

///<summary>
///This class manages the environment in the current scene. It takes in groups of meshes and turns them on and off, depending on what is the Current 
///Group, Previous Group, and Rest.
///</summary>
public class EnvironmentManager : MonoBehaviour {

    public GameObject startingGroup;
    public GameObject[] environmentGroups;

    private GameObject currentGroup;
    private GameObject previousGroup;
    private bool changeCurGroup;

    ///<summary>
    ///This method controls setting what is the current group and what is the previous group.
    ///The previous group was a second thought, as what if we could still see the previous environment meshes, even though we are in a new 
    ///environment. For this, if the previous group is ever the same as the current group, then you just put in the same environment mesh into 
    ///your public gameobjects and enter that in.
    ///</summary>
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
        if (!RenderSettings.fog) {
            RenderSettings.fog = true;
        }
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

    public void ChangeFog(float newFogDensity, Color newFogColor) {
        RenderSettings.fogDensity = newFogDensity;
        RenderSettings.fogColor = newFogColor;
    }

}
