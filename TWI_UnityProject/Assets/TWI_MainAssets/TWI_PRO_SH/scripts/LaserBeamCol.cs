using UnityEngine;
using System.Collections;

public class LaserBeamCol : MonoBehaviour 
{
	//public Collider enemyCol;
    [Range(0,100)]public float laserDamage;
	private float curTime, maxTime;

	void start(){
		maxTime = 0.5f;
	}

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Arbiter"))
        {
			curTime += Time.deltaTime;
			if(curTime >= maxTime){
            	other.gameObject.GetComponent<EnemyHealth>().AddHealth(-laserDamage);
				curTime = 0.0f;
			}

        }
    }
}
