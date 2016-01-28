using UnityEngine;
using System.Collections;

public class FlippingDocier : MonoBehaviour {

    public Material[] images;
    public Renderer rend;
    private int index = 0;
    

	// Use this for initialization
	void Start () {
        rend.GetComponent<Renderer>();
        rend.enabled = true;
        rend.material = images[index];
        index++;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (index == images.Length)
            {
                index = 0;
                rend.material = images[index];
            }
            else
            {
                rend.material = images[index];
            }
            index++;
        }
	}
}
