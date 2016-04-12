using UnityEngine;
using System.Collections;

/// <summary>
/// Flipping Dossier is used to interact with the dossier in game, using the 'P' button right now you can switch between predetermined materials that are going to be the pages of the current missions Dossier
/// Yes I understand that the file is currently incorrectly named "docier" but deal with it, because it works
/// </summary>


public class FlippingDocier : MonoBehaviour {

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
        if (Input.GetKeyDown(keyboard.Dossier))
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
