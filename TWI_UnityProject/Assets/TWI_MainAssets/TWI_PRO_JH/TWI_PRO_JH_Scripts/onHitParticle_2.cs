using UnityEngine;
using System.Collections;

public class onHitParticle_2 : MonoBehaviour {

    public ParticleSystem spark;

    void Start()
    {

        spark = GetComponent<ParticleSystem>();

    }

    void OnCollisionEnter(Collider col)
    {
        if (col.tag != "shield")
        {
            Vector3 otherPos = col.gameObject.transform.position;
            spark.transform.position = otherPos;
            spark.enableEmission = true;
            spark.Play();
        }
    }


    void OnCollisionExit()
    {

        spark.enableEmission = false;
        spark.Stop();

    }

}
