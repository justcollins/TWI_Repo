using UnityEngine;
using System.Collections;

public class ShieldScriptCol : MonoBehaviour 
{
    //public Collider enemyCol;

    void OnTriggerStay(Collider taggers)
    {
        if (taggers.gameObject.tag == "Enemy")
        {
            Debug.Log("Something in my shield");
        }
    }
}
