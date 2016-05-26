using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Wormhole : MonoBehaviour {

    private Blink bl;

	public GameObject[] Wormholes;
    private bool timer= false;
	private Transform[] Warps;
	private int Warp;
    public Image WarpImage;

    public float alpha;


	// Use this for initialization
	void Start () 
    {
        WarpImage = GetComponent<Image>();
        bl = FindObjectOfType<Blink>();
	}
	
	// Update is called once per frame
	void Update () {

		Warp = ClosestWarp (Wormholes);

	}
    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "Entry" && Warp % 2 == 0) 
        {
            if (timer == false)
            {
                transform.position = Wormholes[Warp+1].transform.position;
                bl.blinkArea = true;
                Hello();
            }

        }
		else if (col.gameObject.tag == "Exit" && Warp % 2 != 0) 
        {
            if (timer == false)
            {
				transform.position = Wormholes[Warp-1].transform.position;
                bl.blinkArea = false;
                Hello();
            }
        }
    }
	
    IEnumerator Hello()
    {
        timer = true;
        yield return new WaitForSeconds(5);
        //WarpImage.alpha = 0; // make alpha of image //get material first before i can access the alpha
        timer = false;
    }

    void ImageAlpha
    {
        //Color alpha = image.color;
        //alpha.a = 0;
        //Image.Color = alpha;

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

}
