using UnityEngine;
using System.Collections;

public class TestEnvironmentScript : MonoBehaviour {

    public BodyFlow environment1;
    public BodyFlow environment2;
    public BodyFlow environment3;
    public BodyFlow environment4;
    public BodyFlow environment5;

    private EnvironmentManager environmentManager;

    private void Awake() {
        environmentManager = GameObject.FindObjectOfType<EnvironmentManager>();
    }
	
	/*private void Update () {
        if(Input.GetKeyDown(KeyCode.A)) {
            environmentManager.SetActiveGroups(environment1, environment5, environment2);
            environmentManager.ChangeFog(1.0f, Color.blue);
        } else if(Input.GetKeyDown(KeyCode.S)) {
            environmentManager.SetActiveGroups(environment2, environment1, environment3);
            environmentManager.ChangeFog(0.5f, Color.magenta);
        } else if(Input.GetKeyDown(KeyCode.D)) {
            environmentManager.SetActiveGroups(environment3, environment2, environment4);
            environmentManager.ChangeFog(0.25f, Color.green);
        } else if(Input.GetKeyDown(KeyCode.F)) {
            environmentManager.SetActiveGroups(environment4, environment3, environment5);
            environmentManager.ChangeFog(0.75f, Color.red);
        } else if(Input.GetKeyDown(KeyCode.G)) {
            environmentManager.SetActiveGroups(environment5, environment4, environment1);
            environmentManager.ChangeFog(0.1f, Color.yellow);
        }
	}*/
}
