using UnityEngine;
using System.Collections;

public class ShieldScript : MonoBehaviour
{
    public GameObject shieldObj;
    private Collider shieldCol; //varible of type Collider
    private MeshRenderer shieldShader; //variable of type MeshRender
    //private bool showShield = true;
    private EngineMonitor EM;
    private KeyboardManager keyboard;

    void Start()
    {
        //beginning of game, you should NOT see the shield up
        keyboard = FindObjectOfType<KeyboardManager>();
        EM = GameObject.FindObjectOfType<EngineMonitor>();
        shieldShader = shieldObj.GetComponent<MeshRenderer>();//shader takes in GameObj's mesh render
        shieldCol = shieldObj.GetComponent<Collider>();
        shieldShader.enabled = false;//make shader not rendered on camera
        shieldCol.enabled = false;//the obj's collider is unenabled
    }
    void Update()
    {
        //turn shield's mesh & shader on/off on camera view when Spacebar is pressed
        if (EM.getLaserState() == 2 && Input.GetKey(keyboard.LeftMouse))
        {
            //shieldShader = shieldObj.GetComponent<MeshRenderer>();
            //shieldShader.enabled = !shieldShader.enabled;//it's now True
            //showShield = !showShield;//beginning was True now is False
            shieldShader.enabled = true;
            shieldCol.enabled = true;
            Debug.Log("Shield ON");
            
        }
        else
        {
                shieldShader.enabled = false;
                shieldCol.enabled = false;
               // Debug.Log("Shield OFF");
        }
        
    }

    void OnTriggerStay(Collider taggers)
    {
        if (taggers.gameObject.tag == "Enemy")
        {
            Debug.Log("Something in my shield");
        }
    }

    /*void OnTriggerEnter(Collider taggers)
    {
        if (taggers.gameObject.tag == "Enemy")
        {
            Debug.Log("Something in my shield");
            //taggers --; something that subtracts taggers' HP
        }
    }*/

   /* void OnTriggerExit(Collider taggers)
    {
        Debug.Log("It's gone now");
        //this should ALSO appear if the Enemy died in the shield
    }*/
}