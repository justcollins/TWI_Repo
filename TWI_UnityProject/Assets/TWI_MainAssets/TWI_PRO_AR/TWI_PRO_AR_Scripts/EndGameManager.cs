using UnityEngine;
using System.Collections;

public class EndGameManager : MonoBehaviour {

	public Submarine_Resources subRes;
	public static EnemyHealth arbiter;
	public GameObject endCanvas;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (subRes.getOxygenLevel () <= 0 || subRes.getCabinPressure () >= 100) {
			StartCoroutine(endScreen(10.0f, 0));//Lose is 0 child
		} else if (arbiter == null) {
			StartCoroutine(endScreen (10.0f, 1));//Win is 1 child
		}
	}

	IEnumerator endScreen(float waitTime, int child){
		endCanvas.transform.GetChild (child).gameObject.SetActive (true);
		yield return new WaitForSeconds (waitTime);
		Application.LoadLevel (0);
	}
}
