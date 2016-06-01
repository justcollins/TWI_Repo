using UnityEngine;
using System.Collections;

///<summary>
///This class manages the environment in the current scene. It takes in groups of meshes and turns them on and off, depending on what is the Current 
///Group, Previous Group, and Rest.
///LOTS OF COMMENTS
///</summary>
public class EnvironmentManager : MonoBehaviour {

    private float newDensity;
    private Color newColor;
    private bool changingFog;

    //public BodyFlow startingGroup;
    //public BodyFlow[] environmentGroups;
    //public SubControl ship;
    //private BodyFlow currentGroup;
    
    //private int curSection;

    ///<summary>
    ///This method controls setting what is the current group and what is the previous group.
    ///The previous group was a second thought, as what if we could still see the previous environment meshes, even though we are in a new 
    ///environment. For this, if the previous group is ever the same as the current group, then you just put in the same environment mesh into 
    ///your public gameobjects and enter that in.
    ///</summary>
    
    private void Start() {
        newDensity = RenderSettings.fogDensity;
        newColor = RenderSettings.fogColor;
    }
   

	/*void Start () {
        curSection = ship.getSectionInt();
        SetActiveGroups(startingGroup);
        if (!RenderSettings.fog) {
            RenderSettings.fog = true;
        }      
    }*/
	
	/*void Update () {
        //Debug.Log(curSection);
        curSection = ship.getSectionInt();
        //ManageVisibleGroup(curSection);
	}*/

    /*public string GetCurrentGroup()
    {
        return currentGroup.name;
    }*/

    /*public void SetActiveGroups(BodyFlow newCurrentGroup)
    {
        newCurrentGroup.gameObject.SetActive(true);
        for (int j = 0; j < newCurrentGroup.adjacentSections.Length; j++)
        {
            newCurrentGroup.adjacentSections[j].gameObject.SetActive(true);
        }
        

    }*/

    /*private void ManageVisibleGroup(int section) {
        for(int i = 0; i < environmentGroups.Length; i++) {
            if (environmentGroups[i].sectionNumber == section)
            {
                currentGroup = environmentGroups[i];
                SetActiveGroups(currentGroup);
                break;
                
            }
            else
            {
                environmentGroups[i].gameObject.SetActive(false);
            }
                 
        }
    }*/

    private void Update() {
        

        if (RenderSettings.fogDensity == newDensity && RenderSettings.fogColor == newColor) {
            changingFog = false;
            //Debug.Log("not changing fog");
        } else {
            //Debug.Log("changing fog");
            RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, newDensity, Time.time / 1.0f);
            RenderSettings.fogColor = Color.Lerp(RenderSettings.fogColor, newColor, Time.time / 1.0f);
        }
    }

    public void ChangeFog(float newFogDensity, Color newFogColor) {
        //Debug.Log("Time to change");
        changingFog = true;
        newDensity = newFogDensity;
        newColor = newFogColor;
    }

}
