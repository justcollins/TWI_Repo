using UnityEngine;
using System.Collections;

public class ShieldScriptFire : MonoBehaviour
{
    public GameObject shieldObj;

    void Start()
    {
        shieldObj.SetActive(false);
    }

    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            shieldObj.SetActive(true);
        }
        else
        {
            shieldObj.SetActive(false);
        }
    }
}