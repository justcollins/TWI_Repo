using UnityEngine;
using System.Collections;

public class LaserBeam2 : MonoBehaviour 
{
	//public Collider enemyCol;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hitting Enemy");
        }
    }
}
