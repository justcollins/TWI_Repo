using UnityEngine;
using System.Collections;

public class TestEnvironmentScript : MonoBehaviour {

    public GameObject environment1;
    public GameObject environment2;
    public GameObject environment3;
    public GameObject environment4;
    public GameObject environment5;

    private EnvironmentManager environmentManager;

    private void Awake() {
        environmentManager = GameObject.FindObjectOfType<EnvironmentManager>();
    }

    private void Start () {

	}
	
	private void Update () {
        if(Input.GetKeyDown(KeyCode.A)) {
            environmentManager.SetActiveGroups(environment1, environment5);
        } else if(Input.GetKeyDown(KeyCode.S)) {
            environmentManager.SetActiveGroups(environment2, environment1);
        } else if(Input.GetKeyDown(KeyCode.D)) {
            environmentManager.SetActiveGroups(environment3, environment2);
        } else if(Input.GetKeyDown(KeyCode.F)) {
            environmentManager.SetActiveGroups(environment4, environment3);
        } else if(Input.GetKeyDown(KeyCode.G)) {
            environmentManager.SetActiveGroups(environment5, environment4);
        }
	}
}
