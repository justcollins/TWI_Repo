using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Wormhole : MonoBehaviour {

    private Blink bl;

	public GameObject[] Wormholes;
    private bool timer= false;
	private Transform[] Warps;
	private int Warp;
   
    private bool Jump = false;

    
	// Use this for initialization
	void Start () 
    {
        bl = FindObjectOfType<Blink>();
	}
	
	// Update is called once per frame
	void Update () {

		Warp = ClosestWarp (Wormholes);

	}

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Entry" && Warp % 2 == 0 ) 
        {
            if (timer == false)
            {
                transform.position = Wormholes[Warp+1].transform.position;          
               
            }

        }
        else if (col.gameObject.tag == "Exit" && Warp % 2 != 0) 
        {
            if (timer == false)
            {
				transform.position = Wormholes[Warp-1].transform.position;            
               
            }
        }
    }
	

	int ClosestWarp (GameObject[]Wormholes)
	{
		float closestDistance = Vector3.Distance(Wormholes[0].transform.position,transform.position);

		int targetNumber = 0;


		for (int i = 0; i < Wormholes.Length; i++) 
		{
			float thisDistance = Vector3.Distance(Wormholes[i].transform.position,transform.position);
			if (thisDistance < closestDistance) 
			{
				closestDistance = thisDistance;
				targetNumber = i;

                if (closestDistance < 10)
                {
                    //Change alpha, over time ,aplha = 255; 
                }

			}
		}
		return targetNumber; 
	}
    public void SetJump(bool jmp) 
    {
        Jump = jmp;
    }
}
