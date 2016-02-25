using UnityEngine;
using System.Collections;

public class ShieldScript : MonoBehaviour 
{
	public GameObject shieldObj;
    private Collider shieldObj2;
	private MeshRenderer sShader;
	private bool showShield = true;

	void Start () 
	{
		//beginning of game, you should NOT see the shield up
		sShader = shieldObj.GetComponent<MeshRenderer>();//sShader takes in GameObj's mesh render
        shieldObj2 = shieldObj.GetComponent<Collider>();
        sShader.enabled = true;//make shader not rendered on camera
		//shieldObj.SetActive(true);//turn mesh off, can pass through
        shieldObj2.enabled = true;
	}
	void Update () 
	{
		//turn shield's mesh & shader on/off on camera view when Spacebar is pressed
		if(Input.GetKeyUp("space"))
		{
			sShader = shieldObj.GetComponent<MeshRenderer>();
			sShader.enabled = !sShader.enabled;//false;
			//shieldObj.SetActive(false);
            shieldObj2.enabled = false;
			showShield = !showShield;

			if(showShield == false)
			{
				sShader.enabled = true;
				//shieldObj.SetActive(true);
                shieldObj2.enabled = true;
			}
			else
			{
				sShader.enabled = false;
				//shieldObj.SetActive(false);
                shieldObj2.enabled = false;
			}
		}
	}
}