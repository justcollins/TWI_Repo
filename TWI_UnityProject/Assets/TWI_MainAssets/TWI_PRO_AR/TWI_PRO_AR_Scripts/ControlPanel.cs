using UnityEngine;
using System.Collections;

public class ControlPanel : MonoBehaviour {

	public Material[] images;
    public Renderer rend;
    private KeyboardManager keyboard;
    private int index = 0;
    

	// Use this for initialization
	void Start () {
        keyboard = FindObjectOfType<KeyboardManager>();
        rend.GetComponent<Renderer>();
        rend.enabled = true;
        rend.material = images[index];
        index++;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(keyboard.ControlPanel))
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

