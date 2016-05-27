using UnityEngine;
using System.Collections;

public class onHitParticle : MonoBehaviour {

    public ParticleSystem spark;
	public GameObject aim;

    private RaycastHit hit;
    private Vector3 sparkPos;

	void start(){

		aim = GetComponent<GameObject> ();
	
	}


	void OnTriggerEnter(Collider other){

		Ray ray = new Ray(aim.transform.position, aim.transform.forward);
		
		if(Physics.Raycast(ray, out hit)){
			Debug.Log(hit.distance);
		}
		if (other.tag != "shield")
        {
			sparkPos = new Vector3(hit.point.x, hit.point.y , hit.point.z);
			spark.transform.position = sparkPos;
            spark.enableEmission = true;
            spark.Play();
        }
    }

    void OnTriggerStay(Collider other) {
        
        sparkPos = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        spark.transform.position = sparkPos;
        
    
    }

    void OnTriggerExit(Collider other){

        spark.enableEmission = false;
        spark.Stop();
    
    }
    
}
