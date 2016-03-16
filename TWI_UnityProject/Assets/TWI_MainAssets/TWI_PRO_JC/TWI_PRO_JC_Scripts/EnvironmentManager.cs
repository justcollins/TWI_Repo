using UnityEngine;
using System.Collections;

///<summary>
///This class manages the environment in the current scene. It takes in groups of meshes and turns them on and off, depending on what is the Current 
///Group, Previous Group, and Rest.
///</summary>
public class EnvironmentManager : MonoBehaviour {

    public BodyFlow startingGroup;
    public BodyFlow[] environmentGroups;
    public SubControl ship;

    private BodyFlow currentGroup;

    private int curSection;

    ///<summary>
    ///This method controls setting what is the current group and what is the previous group.
    ///The previous group was a second thought, as what if we could still see the previous environment meshes, even though we are in a new 
    ///environment. For this, if the previous group is ever the same as the current group, then you just put in the same environment mesh into 
    ///your public gameobjects and enter that in.
    ///</summary>
    public void SetActiveGroups(BodyFlow newCurrentGroup)
    {
        for(int i = 0; i < environmentGroups.Length; i++) {
            if(newCurrentGroup.name == environmentGroups[i].name) {
                currentGroup = newCurrentGroup;
                Debug.Log("currentGroup: " + currentGroup);
            }
        }
        
    }

    public string GetCurrentGroup() {
        return currentGroup.name;
    }

	void Start () {
        curSection = startingGroup.sectionNumber;
        ManageVisibleGroup();
        if (!RenderSettings.fog) {
            RenderSettings.fog = true;
        }
        
    }
	
	void Update () {
	    if(ship.getSectionInt() != curSection) {
            curSection = ship.getSectionInt();
        }
        Debug.Log(curSection);
        ManageVisibleGroup();
	}

    private void ManageVisibleGroup() {
        for(int i = 0; i < environmentGroups.Length; i++) {
            if (environmentGroups[i].sectionNumber == curSection)
            {
                currentGroup = environmentGroups[i];
                environmentGroups[i].gameObject.SetActive(true);
                for (int j = 0; j < currentGroup.adjacentSections.Length; j++)
                {
                    if (environmentGroups[i] == currentGroup.adjacentSections[j])
                    {
                        environmentGroups[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        environmentGroups[i].gameObject.SetActive(false);
                    }
                }
            }
        }
        
    }

    public void ChangeFog(float newFogDensity, Color newFogColor) {
        RenderSettings.fogDensity = newFogDensity;
        RenderSettings.fogColor = newFogColor;
    }

}
