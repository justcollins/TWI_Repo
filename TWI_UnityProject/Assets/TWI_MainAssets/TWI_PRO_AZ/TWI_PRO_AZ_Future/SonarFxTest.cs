using UnityEngine;
using System.Collections;


public class SonarFxTest : MonoBehaviour
{
	SonarFx SNFX;

	void Start(){
		SNFX = GetComponent<SonarFx>();
	
	}
    void Update()
    {
		if (Input.GetKeyDown ("r") && SNFX.enabled == false) {
			SNFX.enabled = true;
		} 
		else if (Input.GetKeyDown ("r") && SNFX.enabled == true) {
			SNFX.enabled = false;

		}
	
    }

	IEnumerator MyCoroutine()
	{
		
		yield return new WaitForSeconds (3);
		
	}
}
