/* PROGRAMMER	:	Kien Ngo
 * FILE			:	DyingParticles.cs
 * DATE			:	May 26, 2016
 * PURPOSE		:	This will make the object slowly fade away while particles spread out
 * 
 * NOTE:
 * We need need to access the Mesh Renderer because we are trying to access the Material of this 
 * object. Once we place the Particle System Component the Material joins the Mesh Renderer 
 * automatically
 * 
 * INSTRUCTION:
 * 1. Create a material
 *      Rendering Mode = Fade
 * 2. Apply it to the object
 * 3. Select the this object
 * 4. Go to Component --> Effects --> Particle System
 *      This will automatically add to the object
 *      Change:
 *          Start Delay = 0
 *          Start Speed = 1 to make it spread slower
 *          Shape --> Shape = Circle
 *                --> Random Direction enable
 * 5. Add this script
 * 6. Change the WaitTime = the duration to fade away
 * 7. Change the Alpha value    = how much to take away to fade out
 */

using UnityEngine;
using System.Collections;

public class DyingParticles : MonoBehaviour 
{
    [Tooltip("The amount to subtract per update during the duration")]
    [Range(0.0f, 1.0f)]
    public float alpha;

    [Tooltip("The duration it takes to fade away")]
    public float waitTime;

    private MeshRenderer meshRenderer; // This grabs the MeshRenderer component to access material
    public bool dying = false;
    private float timer;    
    private Material[] materials;
    private ParticleSystem ps;
    private Color color;

	// Use this for initialization
	void Start () 
    {
        timer = 0.0f;
        ps = GetComponent<ParticleSystem>();
        ps.Stop(); //Stop the particleSystem from playing when start

        meshRenderer = GetComponent<MeshRenderer>();
        materials = meshRenderer.materials;
        color = materials[0].color;
	} //End of Start

	
	// Update is called once per frame
	void Update () 
    {
        //Only do this when the object is dying
        if(dying)
        {
            //Start activating the particleSystem
            if (!ps.isPlaying)
                ps.Play();

            timer += Time.deltaTime;
            
	        color = materials[0].color;
            color.a -= alpha;   //This is where we are fading the object away
            materials[0].color = color;

            if (timer > waitTime)
            {
                dying = false;
                if (ps.isPlaying)
                {
                    ps.Stop();
                    //Maybe we want to destory the object here
                } 
            }
        }

        /*Debugging purpose 
        if(Input.GetKeyDown(KeyCode.E))
        {
            dying = !dying;
        }
        */
	}//End of Update
}
