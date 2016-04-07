using UnityEngine;
using System.Collections;

public class ShieldScriptCol : MonoBehaviour 
{
    //public Collider enemyCol;
    [Range(0, 100)] public float shieldDamage;
	private float curTime, maxTime;


	void start(){
		maxTime = 0.5f;
	}

    void OnTriggerStay(Collider taggers)
    {
        if (taggers.CompareTag ("Enemy") || taggers.CompareTag("Arbiter"))
        {
			curTime += Time.deltaTime;
			if(curTime >= maxTime){
				taggers.gameObject.GetComponent<EnemyHealth>().AddHealth(-shieldDamage);
				curTime = 0.0f;
			}
        }
    }
}
